using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using Prism.Events;
using TalkingKeyboard.Infrastructure;
using TalkingKeyboard.Infrastructure.Controls;
using TalkingKeyboard.Infrastructure.Helpers;

namespace TalkingKeyboard.Modules.CentralTextModule.ViewModels
{
	public class TextViewModel : BindableBase, ITextModel
    {
	    private readonly IEventAggregator _eventAggregator;

	    public TextViewModel(IEventAggregator eventAggregator)
        {
	        _eventAggregator = eventAggregator;
	        _addTextCommand = new DelegateCommand<string>(s => CurrentText += s);
            _removeLastCharacterCommand =
                new DelegateCommand(() => CurrentText = CurrentText.Remove(CurrentText.Length - 1), () => CurrentText.Length > 0)
                    .ObservesProperty(() => CurrentText);
            _removeLasWordCommand = new DelegateCommand(() =>
            {
                CurrentText = StringEditHelper.RemoveLastWord(CurrentText);
            });
            Infrastructure.Commands.SetTextCommand.RegisterCommand(_addTextCommand);
            Infrastructure.Commands.RemoveLastCharacterCommand.RegisterCommand(_removeLastCharacterCommand);
            Infrastructure.Commands.RemoveLastWordCommand.RegisterCommand(_removeLasWordCommand);
        }

	    private string _currentText = "";
	    private ICommand _addTextCommand;
	    private ICommand _removeLastCharacterCommand;
	    private ICommand _removeLasWordCommand;

	    public string CurrentText
	    {
	        get { return _currentText; }
	        set
	        {
	            SetProperty(ref _currentText, value); 
	            _eventAggregator.GetEvent<TextUpdatedEvent>().Publish();
	        }
	    }

	    public ICommand AddTextCommand => _addTextCommand;

	    public ICommand RemoveLastCharacterCommand
	    {
	        get { return _removeLastCharacterCommand; }
	        set { _removeLastCharacterCommand = value; }
	    }

	    public ICommand RemoveLasWordCommand
	    {
	        get { return _removeLasWordCommand; }
	        set { _removeLasWordCommand = value; }
	    }
    }
}
