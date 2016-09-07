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
                _suggestionService.AddMultiCharacterText(s);
                _eventAggregator.GetEvent<Events.MultiTextUpdatedEvent>().Publish();
            });
            RemoveLastMultiCharacterCommand = new DelegateCommand(() =>
            {
                _suggestionService.RemoveLastMultiCharacter();
                _eventAggregator.GetEvent<Events.MultiTextUpdatedEvent>().Publish();
            });
            Commands.AddMultiCharacterTextCommand.RegisterCommand(AddMultikeyTextCommand);
            Commands.RemoveLastMultiCharacterCommand.RegisterCommand(RemoveLastMultiCharacterCommand);
        }


        public ICommand AddMultikeyTextCommand { get; set; }

        public ICommand RemoveLastMultiCharacterCommand { get; set; }
    }
}