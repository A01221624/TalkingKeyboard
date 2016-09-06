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

        private string _currentText = "";

        public TextViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            AddTextCommand = new DelegateCommand<string>(s =>
            {
                if (s.Length == 0) return;
                if (CurrentText.Length > 0
                    && CharacterClasses.PreceededByNonwhitespaceFollowedByWhitespace.Contains(s[0])
                    && CharacterClasses.Whitespace.Contains(CurrentText[CurrentText.Length - 1]))
                    CurrentText = CurrentText.Remove(CurrentText.Length - 1);
                CurrentText += s;
            });
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
    }
}