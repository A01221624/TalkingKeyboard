// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MultiCharacterSuggestionSource.cs" company="Numeral">
//   Copyright 2016 Fernando Ramírez Garibay
// </copyright>
// <summary>
//   Defines the MultiCharacterSuggestionSource type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace TalkingKeyboard.Modules.SuggestionsProvider.SuggestionSources
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.IO;
    using System.Linq;

    using TalkingKeyboard.Infrastructure.Constants;
    using TalkingKeyboard.Infrastructure.ServiceInterfaces;

    /// <summary>
    ///     This class implements a disambiguation algorithm for multi-character text entry.
    /// </summary>
    /// <seealso cref="TalkingKeyboard.Infrastructure.ServiceInterfaces.ISuggestionSource" />
    public class MultiCharacterSuggestionSource : ISuggestionSource
    {
        private readonly List<List<string>> filteredDictionary = new List<List<string>>();
        private int caretPosition;

        /// <summary>
        ///     Initializes a new instance of the <see cref="MultiCharacterSuggestionSource" /> class.
        /// </summary>
        public MultiCharacterSuggestionSource()
        {
            this.LoadDictionarySortedByFrequency(ResourceLocations.DefaultDictionaryLocation);
        }

        /// <summary>
        ///     Adds a multi-character to the buffer.
        /// </summary>
        /// <param name="s">The multi-character to add.</param>
        /// <remarks>
        ///     The multi-character is a string with several characters, each of which will be considered as a potential
        ///     character for the current position in the text. These characters may be separated by any whitespace character found
        ///     in <see cref="CharacterClasses.Whitespace" />
        /// </remarks>
        public void Add(string s)
        {
            this.UpdateFilteredDictionary(s);
        }

        /// <summary>
        ///     Clears the multi character buffer.
        /// </summary>
        /// <remarks>
        ///     This only virtually clears the buffer by returning to the original dictionary (position zero in the list of
        ///     filtered dictionaries). The rest of the dictionaries are not unassigned but will be overwritten as multi-characters
        ///     are added.
        /// </remarks>
        public void ClearMultiCharacterBuffer()
        {
            this.caretPosition = 0;
        }

        /// <summary>
        ///     Filters and returns suggestions according to the multi-key buffer.
        /// </summary>
        /// <param name="basedOn">Nothing is required. May be used later on.</param>
        /// <returns>
        ///     Returns an <see cref="T:System.Collections.ObjectModel.ObservableCollection`1" /> with disambiguation suggestions
        ///     first and then auto-complete suggestions.
        /// </returns>
        /// <remarks>
        ///     Disambiguation are those which match the number of keys selected and auto-complete are those which include keys
        ///     which have not been selected yet.
        /// </remarks>
        public ObservableCollection<string> GetSuggestions(object basedOn = null)
        {
            var disambiguationList = this.FilterDictionaryToWordLength(this.caretPosition);
            var autocompleteList = this.FilterDictionaryToHigherThanWordLength(this.caretPosition);
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

            this.filteredDictionary.Add(dictionary);
        }

        /// <summary>
        ///     Removes the last multi-character.
        /// </summary>
        public void RemoveLastMultiCharacter()
        {
            if (this.caretPosition == 0)
            {
                return;
            }

            this.caretPosition--;
        }

        /// <summary>
        ///     Filters the dictionary to only leave words longer than <see cref="wordLength" />.
        /// </summary>
        /// <param name="wordLength">Max inadmissible length of word.</param>
        /// <returns>The filtered dictionary.</returns>
        /// <remarks>Here "dictionary" is not meant as a type. It is a list of words.</remarks>
        private List<string> FilterDictionaryToHigherThanWordLength(int wordLength)
        {
            return this.filteredDictionary[this.caretPosition].FindAll(s => s.Length > wordLength);
        }

        /// <summary>
        ///     Filters the dictionary to only leave words of length <see cref="wordLength" />..
        /// </summary>
        /// <param name="wordLength">Max inadmissible length of word.</param>
        /// <returns>The filtered dictionary.</returns>
        /// <remarks>Here "dictionary" is not meant as a type. It is a list of words.</remarks>
        private List<string> FilterDictionaryToWordLength(int wordLength)
        {
            return this.filteredDictionary[this.caretPosition].FindAll(s => s.Length == wordLength);
        }

        /// <summary>
        ///     Updates the dictionary list (multi-character buffer) given a new multi-character.
        /// </summary>
        /// <param name="addedMultiCharacter">The added multi-character.</param>
        /// <remarks>
        ///     <para>
        ///         The multi-character buffer is essentially a list of word lists (dictionaries), where the first one is the
        ///         original
        ///         dictionary and each one following it is a filtered version of the previous where the filtering is based on the
        ///         new
        ///         multi-character received.
        ///     </para>
        ///     <para>
        ///         Algorithm:
        ///         <list type="number">
        ///             <item>
        ///                 <description>Add new entry to buffer if necessary</description>
        ///             </item>
        ///             <item>
        ///                 <description>Filter the previous dictionary as follows</description>
        ///             </item>
        ///             <list type="bullet">
        ///                 <item>
        ///                     <description>Check whether the word is at least of adequate size (number of characters entered).</description>
        ///                 </item>
        ///                 <item>
        ///                     <description>
        ///                         Check whether any of the characters (ignore whitespace) from the multi-character
        ///                         string fall on the expected position.
        ///                     </description>
        ///                 </item>
        ///             </list>
        ///             <item>
        ///                 <description>Increase the current buffer position (to the newly filtered dictionary).</description>
        ///             </item>
        ///         </list>
        ///     </para>
        /// </remarks>
        private void UpdateFilteredDictionary(string addedMultiCharacter)
        {
            var possibleCharacters = addedMultiCharacter.ToCharArray();
            if (this.filteredDictionary.Count <= this.caretPosition + 1)
            {
                this.filteredDictionary.Add(null);
            }

            this.filteredDictionary[this.caretPosition + 1] = this.filteredDictionary[this.caretPosition].FindAll(
                s =>
                    {
                        foreach (var c in possibleCharacters)
                        {
                            if (CharacterClasses.Whitespace.Contains(c))
                            {
                                continue;
                            }

                            if ((s.Length > this.caretPosition) && (s[this.caretPosition] == c))
                            {
                                return true;
                            }
                        }

                        return false;
                    });
            this.caretPosition++;
        }
    }
}