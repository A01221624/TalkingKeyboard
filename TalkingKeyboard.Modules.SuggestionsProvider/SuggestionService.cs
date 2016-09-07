using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Prism.Commands;
using TalkingKeyboard.Infrastructure;
using TalkingKeyboard.Infrastructure.Controls;
using TalkingKeyboard.Infrastructure.Helpers;
using TalkingKeyboard.Infrastructure.ServiceInterfaces;
using TalkingKeyboard.Modules.SuggestionsProvider.SuggestionSources;

namespace TalkingKeyboard.Modules.SuggestionsProvider
{
    using TalkingKeyboard.Infrastructure.Constants;

    public class SuggestionService : ISuggestionService, IDisposable
    {
        private readonly MultikeySuggestionSource _multikeySource = new MultikeySuggestionSource();
        private readonly PresageSuggestionSource _presageSource = new PresageSuggestionSource();
        private readonly ITextModel _textModel;

        public SuggestionService(ITextModel textModel)
        {
            _textModel = textModel;
            AddSuggestionCommand = new DelegateCommand<string>(s =>
            {
                var currentText = _textModel.CurrentText;
                var lastWord = StringEditHelper.GetLastWord(currentText);
                if (s.ToLower().StartsWith(lastWord.ToLower())) Commands.RemoveLastWordCommand.Execute(null);
                Commands.SetTextCommand.Execute(s + " ");
            });
            Commands.AddSuggestionCommand.RegisterCommand(AddSuggestionCommand);
        }

        public ICommand AddSuggestionCommand { get; set; }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public ObservableCollection<string> ProvideSuggestions(string basedOn)
        {
            var suggestions = _presageSource.GetSuggestions(basedOn);
            return AddSpaceAndUppercaseIfNecessary(suggestions);
        }

        public ObservableCollection<string> ProvideMultiCharacterSuggestions()
        {
            var suggestions = _multikeySource.GetSuggestions();
            return AddSpaceAndUppercaseIfNecessary(suggestions);
        }

        public void AddMultiCharacterText(string s)
        {
            _multikeySource.Add(s);
        }

        public void RemoveLastMultiCharacter()
        {
            _multikeySource.RemoveLastMultiCharacter();
        }

        public void ClearMultiCharacterBuffer()
        {
            _multikeySource.ClearMultiCharacterBuffer();
        }

        private ObservableCollection<string> AddSpaceAndUppercaseIfNecessary(ObservableCollection<string> collection)
        {
            var currentPrefix = StringEditHelper.RemoveLastWord(_textModel.CurrentText);
            var trimmedPrefix = currentPrefix.Trim();
            var needsSpace = currentPrefix.Length > 0
                             && !CharacterClasses.Whitespace.Contains(
                                 currentPrefix[currentPrefix.Length - 1]);
            var needsUppercase = _textModel.CurrentText.Trim().Length == 0 || trimmedPrefix.Length == 0
                                 ||
                                 CharacterClasses.FollowedByUppercase.Contains(trimmedPrefix[trimmedPrefix.Length - 1]);
            var result = collection.Select(s =>
            {
                if (needsUppercase) s = s.Length > 1 ? char.ToUpper(s[0]) + s.Substring(1) : s.ToUpper();
                if (needsSpace) s = " " + s;
                return s;
            });
            return new ObservableCollection<string>(result);
        }
    }
}