using System.Windows.Input;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using TalkingKeyboard.Infrastructure;
using TalkingKeyboard.Infrastructure.ServiceInterfaces;
using TalkingKeyboard.Modules.MultiKeyBoard.Model;

namespace TalkingKeyboard.Modules.MultiKeyBoard.ViewModels
{
    public class QwertySpanishMultiKeyboardViewModel : BindableBase
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly ISuggestionService _suggestionService;

        public QwertySpanishMultiKeyboardViewModel(IEventAggregator eventAggregator,
            ISuggestionService suggestionService)
        {
            _eventAggregator = eventAggregator;
            _suggestionService = suggestionService;
            var model = new MultikeyTextModel();
            AddMultikeyTextCommand = new DelegateCommand<string>(s =>
            {
                _suggestionService.AddMultiKeyText(s);
                _eventAggregator.GetEvent<MultiTextUpdatedEvent>().Publish();
            });
            RemoveLastMultiCharacterCommand = new DelegateCommand(() =>
            {
                _suggestionService.RemoveLastMultiCharacter();
                _eventAggregator.GetEvent<MultiTextUpdatedEvent>().Publish();
            });
            Commands.AddMultikeyTextCommand.RegisterCommand(AddMultikeyTextCommand);
            Commands.RemoveLastMultiCharacterCommand.RegisterCommand(RemoveLastMultiCharacterCommand);
        }


        public ICommand AddMultikeyTextCommand { get; set; }

        public ICommand RemoveLastMultiCharacterCommand { get; set; }
    }
}