using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using TalkingKeyboard.Infrastructure.Constants;
using TalkingKeyboard.Infrastructure.ServiceInterfaces;

namespace TalkingKeyboard.Modules.SuggestionsProvider.SuggestionSources
{
    public class MultikeySuggestionSource : ISuggestionSource
    {
        private readonly List<List<string>> _filteredDictionary = new List<List<string>>();
        private readonly List<string> _receivedMultikeys = new List<string>();
        private int caretPosition;

        public MultikeySuggestionSource()
        {
            LoadDictionarySortedByFrequency(ResourceLocations.DefaultDictionaryLocation);
        }

        /// <summary>
        ///     Filters and returns suggestions according to the multi-key buffer.
        /// </summary>
        /// <param name="basedOn"></param>
        /// <returns>
        ///     Returns disambiguation suggestions first and the rest are auto-complete suggestions.
        /// </returns>
        /// <remarks>
        ///     Disambiguation are those which match the number of keys selected and auto-complete are those which include keys
        ///     which have not been selected yet.
        /// </remarks>
        public ObservableCollection<string> GetSuggestions(object basedOn = null)
        {
            var disambiguationList = FilterDictionaryToWordLength(caretPosition);
            var autocompleteList = FilterDictionaryToHigherThanWordLength(caretPosition);
            disambiguationList.AddRange(autocompleteList);
            return new ObservableCollection<string>(disambiguationList);
        }

        private List<string> FilterDictionaryToWordLength(int wordLength)
        {
            return _filteredDictionary[caretPosition].FindAll(s => s.Length == wordLength);
        }

        private List<string> FilterDictionaryToHigherThanWordLength(int wordLength)
        {
            return _filteredDictionary[caretPosition].FindAll(s => s.Length > wordLength);
        }

        public void Add(string s)
        {
            _receivedMultikeys.Add(s);
            UpdateFilteredDictionary(s);
        }

        private void UpdateFilteredDictionary(string addedKey)
        {
            var possibleCharacters = addedKey.ToCharArray();
            if (_filteredDictionary.Count <= caretPosition + 1) _filteredDictionary.Add(null);
            _filteredDictionary[caretPosition + 1] = _filteredDictionary[caretPosition].FindAll(s =>
            {
                foreach (var c in possibleCharacters)
                {
                    if (c == ' ' || c == '\r' || c == '\n') continue;
                    if (s.Length > caretPosition
                        && s[caretPosition] == c) return true;
                }
                return false;
            });
            caretPosition++;
        }

        public void LoadDictionarySortedByFrequency(string filePath)
        {
            var dictionary = new List<string>();
            using (var reader = File.OpenText(filePath))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    if (!string.IsNullOrWhiteSpace(line)
                        && line.Trim().Length > 0)
                    {
                        dictionary.Add(line);
                    }
                }
            }
            _filteredDictionary.Add(dictionary);
        }

        public void RemoveLastMultiCharacter()
        {
            if (caretPosition == 0) return;
            caretPosition--;
        }

        public void ClearMultiCharacterBuffer()
        {
            caretPosition = 0;
        }
    }
}