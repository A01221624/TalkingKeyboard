using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using TalkingKeyboard.Infrastructure;
using TalkingKeyboard.Infrastructure.Constants;
using TalkingKeyboard.Infrastructure.Controls;
using TalkingKeyboard.Infrastructure.Helpers;

namespace TalkingKeyboard.Modules.CentralTextModule.ViewModels
{
    public class TextViewModel : BindableBase, ITextModel
    {
        private readonly IEventAggregator _eventAggregator;
        private string _accentBuffer = string.Empty;

        private string _currentText = string.Empty;

        public TextViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            AddTextCommand = new DelegateCommand<string>(AddText);
            RemoveLastCharacterCommand =
                new DelegateCommand(() => CurrentText = CurrentText.Remove(CurrentText.Length - 1),
                    () => CurrentText.Length > 0)
                    .ObservesProperty(() => CurrentText);
            RemoveLasWordCommand =
                new DelegateCommand(() => { CurrentText = StringEditHelper.RemoveLastWord(CurrentText); });
            Commands.SetTextCommand.RegisterCommand(AddTextCommand);
            Commands.RemoveLastCharacterCommand.RegisterCommand(RemoveLastCharacterCommand);
            Commands.RemoveLastWordCommand.RegisterCommand(RemoveLasWordCommand);
        }

        public ICommand AddTextCommand { get; }

        public ICommand RemoveLastCharacterCommand { get; set; }

        public ICommand RemoveLasWordCommand { get; set; }

        public string CurrentText
        {
            get { return _currentText; }
            set
            {
                SetProperty(ref _currentText, value);
                _eventAggregator.GetEvent<TextUpdatedEvent>().Publish();
            }
        }

        private void AddText(string s)
        {
            if (s.Length == 0) return;
            var newIsAccent = CharacterClasses.Accents.Contains(s[0]);
            if (!newIsAccent)
            {
                s = CombineWithAccentBuffer(s);
                RemoveWhitespaceIfNecessaryBasedOn(s);
                CurrentText += s;
                _accentBuffer = string.Empty;
            }
            else
            {
                _accentBuffer = s;
            }
        }

        private void RemoveWhitespaceIfNecessaryBasedOn(string s)
        {
            if (CurrentText.Length > 0 && CharacterClasses.PreceededByNonwhitespaceFollowedByWhitespace.Contains(s[0]) &&
                CharacterClasses.Whitespace.Contains(CurrentText[CurrentText.Length - 1]))
                CurrentText = CurrentText.Remove(CurrentText.Length - 1);
        }

        private string CombineWithAccentBuffer(string s)
        {
            Dictionary<char, char> possibleAccentuations;
            char accentedCharacter;
            if (_accentBuffer.Length > 0 && CharacterClasses.AccentLookup.TryGetValue(s[0], out possibleAccentuations) &&
                possibleAccentuations.TryGetValue(_accentBuffer[0], out accentedCharacter))
            {
                s = accentedCharacter.ToString();
            }
            return s;
        }
    }
}