// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MultikeySuggestionSource.cs" company="Numeral">
//   Copyright 2016 Fernando Ramírez Garibay
// </copyright>
// <summary>
//   Defines the MultikeySuggestionSource type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace TalkingKeyboard.Modules.SuggestionsProvider.SuggestionSources
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.IO;

    using TalkingKeyboard.Infrastructure.Constants;
    using TalkingKeyboard.Infrastructure.ServiceInterfaces;

    public class MultikeySuggestionSource : ISuggestionSource
    {
        private readonly List<List<string>> _filteredDictionary = new List<List<string>>();
        private int _caretPosition;

        /// <summary>
        ///     Initializes a new instance of the <see cref="MultikeySuggestionSource" /> class.
        /// </summary>
        public MultikeySuggestionSource()
        {
            this.LoadDictionarySortedByFrequency(ResourceLocations.DefaultDictionaryLocation);
        }

        /// <summary>
        ///     Adds the specified multi-character.
        /// </summary>
        /// <param name="s">The multi-character.</param>
        public void Add(string s)
        {
            this.UpdateFilteredDictionary(s);
        }

        /// <summary>
        ///     Clears the multi-character buffer.
        /// </summary>
        public void ClearMultiCharacterBuffer()
        {
            this._caretPosition = 0;
        }

        /// <summary>
        ///     Filters and returns suggestions according to the multi-key buffer.
        /// </summary>
        /// <param name="basedOn">Nothing is required. May be used later on.</param>
        /// <returns>
        ///     Returns disambiguation suggestions first and the rest are auto-complete suggestions.
        /// </returns>
        /// <remarks>
        ///     Disambiguation are those which match the number of keys selected and auto-complete are those which include keys
        ///     which have not been selected yet.
        /// </remarks>
        public ObservableCollection<string> GetSuggestions(object basedOn = null)
        {
            var disambiguationList = this.FilterDictionaryToWordLength(this._caretPosition);
            var autocompleteList = this.FilterDictionaryToHigherThanWordLength(this._caretPosition);
            disambiguationList.AddRange(autocompleteList);
            return new ObservableCollection<string>(disambiguationList);
        }

        /// <summary>
        ///     Loads the dictionary sorted by frequency.
        /// </summary>
        /// <param name="filePath">The file path where the dictionary is located.</param>
        public void LoadDictionarySortedByFrequency(string filePath)
        {
            var dictionary = new List<string>();
            using (var reader = File.OpenText(filePath))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    if (!string.IsNullOrWhiteSpace(line) && (line.Trim().Length > 0))
                    {
                        dictionary.Add(line);
                    }
                }
            }

            this._filteredDictionary.Add(dictionary);
        }

        /// <summary>
        ///     Removes the last multi-character.
        /// </summary>
        public void RemoveLastMultiCharacter()
        {
            if (this._caretPosition == 0)
            {
                return;
            }

            this._caretPosition--;
        }

        /// <summary>
        ///     Filters the dictionary to only leave words longer than <see cref="wordLength" />.
        /// </summary>
        /// <param name="wordLength">Max inadmissible length of word.</param>
        /// <returns>The filtered dictionary.</returns>
        /// <remarks>Here "dictionary" is not meant as a type. It is a list of words.</remarks>
        private List<string> FilterDictionaryToHigherThanWordLength(int wordLength)
        {
            return this._filteredDictionary[this._caretPosition].FindAll(s => s.Length > wordLength);
        }

        /// <summary>
        ///     Filters the dictionary to only leave words of length <see cref="wordLength" />..
        /// </summary>
        /// <param name="wordLength">Max inadmissible length of word.</param>
        /// <returns>The filtered dictionary.</returns>
        /// <remarks>Here "dictionary" is not meant as a type. It is a list of words.</remarks>
        private List<string> FilterDictionaryToWordLength(int wordLength)
        {
            return this._filteredDictionary[this._caretPosition].FindAll(s => s.Length == wordLength);
        }

        /// <summary>
        ///     Updates the dictionary list (multi-character buffer) given a new multi-character.
        /// </summary>
        /// <param name="addedMultiCharacter">The added multi-character.</param>
        /// <remarks>
        ///     The multi-character buffer is essentially a list of word lists (dictionaries), where the first one is the original
        ///     dictionary and each one following it is a filtered version of the previous where the filtering is based on the new
        ///     multi-character received.
        /// </remarks>
        private void UpdateFilteredDictionary(string addedMultiCharacter)
        {
            var possibleCharacters = addedMultiCharacter.ToCharArray();
            if (this._filteredDictionary.Count <= this._caretPosition + 1)
            {
                this._filteredDictionary.Add(null);
            }

            this._filteredDictionary[this._caretPosition + 1] =
                this._filteredDictionary[this._caretPosition].FindAll(
                    s =>
                        {
                            foreach (var c in possibleCharacters)
                            {
                                if ((c == ' ') || (c == '\r') || (c == '\n'))
                                {
                                    continue;
                                }

                                if ((s.Length > this._caretPosition) && (s[this._caretPosition] == c))
                                {
                                    return true;
                                }
                            }

                            return false;
                        });
            this._caretPosition++;
        }
    }
}