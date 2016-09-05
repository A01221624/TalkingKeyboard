using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Navigation;
using Prism.Events;
using TalkingKeyboard.Infrastructure;
using TalkingKeyboard.Infrastructure.ServiceInterfaces;
using TalkingKeyboard.Modules.MultiKeyBoard.Model;

namespace TalkingKeyboard.Modules.MultiKeyBoard.ViewModels
{
    public class QwertySpanishMultiKeyboardViewModel : BindableBase
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly ISuggestionService _suggestionService;
        private ICommand _addMultikeyTextCommand;
        private ICommand _removeLastMultiCharacterCommand;

        public QwertySpanishMultiKeyboardViewModel(IEventAggregator eventAggregator, ISuggestionService suggestionService)
        {
            _eventAggregator = eventAggregator;
            _suggestionService = suggestionService;
            var model = new MultikeyTextModel();
            _addMultikeyTextCommand = new DelegateCommand<string>((s =>
            {
                _suggestionService.AddMultiKeyText(s);
                _eventAggregator.GetEvent<MultiTextUpdatedEvent>().Publish();
            }));
            _removeLastMultiCharacterCommand = new DelegateCommand(() =>
            {
                _suggestionService.RemoveLastMultiCharacter();
                _eventAggregator.GetEvent<MultiTextUpdatedEvent>().Publish();
            });
            Infrastructure.Commands.AddMultikeyTextCommand.RegisterCommand(_addMultikeyTextCommand);
            Infrastructure.Commands.RemoveLastMultiCharacterCommand.RegisterCommand(_removeLastMultiCharacterCommand);
        }


        public ICommand AddMultikeyTextCommand
        {
            get { return _addMultikeyTextCommand; }
            set { _addMultikeyTextCommand = value; }
        }

        public ICommand RemoveLastMultiCharacterCommand
        {
            get { return _removeLastMultiCharacterCommand; }
            set { _removeLastMultiCharacterCommand = value; }
        }
    }
}
