using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using Prism.Commands;
using Prism.Events;
using TalkingKeyboard.Infrastructure;
using TalkingKeyboard.Infrastructure.Controls;
using TalkingKeyboard.Infrastructure.Helpers;
using TalkingKeyboard.Infrastructure.ServiceInterfaces;
using TalkingKeyboard.Modules.SuggestionsProvider.SuggestionSources;

namespace TalkingKeyboard.Modules.SuggestionsProvider
{
    public class SuggestionService : ISuggestionService, IDisposable
    {
        private readonly ITextModel _textModel;
        private int _count = 0;
        private PresageSuggestionSource _presageSource = new PresageSuggestionSource();
        private MultikeySuggestionSource _multikeySource = new MultikeySuggestionSource();
        private ICommand _addSuggestionCommand;

        public ICommand AddSuggestionCommand
        {
            get { return _addSuggestionCommand; }
            set { _addSuggestionCommand = value; }
        }

        public SuggestionService(ITextModel textModel)
        {
            _textModel = textModel;
            _addSuggestionCommand = new DelegateCommand<string>((s) =>
            {
                var currentText = textModel.CurrentText;
                var lastWord = StringEditHelper.GetLastWord(currentText);
                if (s.StartsWith(lastWord))
                    Infrastructure.Commands.RemoveLastWordCommand.Execute(null);
                Infrastructure.Commands.SetTextCommand.Execute(" " + s + " ");
            });
            Infrastructure.Commands.AddSuggestionCommand.RegisterCommand(_addSuggestionCommand);
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public ObservableCollection<string> ProvideSuggestions(string basedOn)
        {
            return _presageSource.GetSuggestions(basedOn);
        }

        public ObservableCollection<string> ProvideMultikeySuggestions()
        {
            return _multikeySource.GetSuggestions();
        }

        public void AddMultiKeyText(string s)
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
    }
}