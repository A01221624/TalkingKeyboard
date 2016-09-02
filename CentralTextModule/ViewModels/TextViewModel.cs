using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using TalkingKeyboard.Infrastructure.Controls;

namespace TalkingKeyboard.Modules.CentralTextModule.ViewModels
{
	public class TextViewModel : BindableBase, ITextModel
    {
        public TextViewModel()
        {
            _addTextCommand = new DelegateCommand<string>(s => CurrentText += s);
            _removeLastCharacterCommand =
                new DelegateCommand(() => CurrentText = CurrentText.Remove(CurrentText.Length - 1), () => CurrentText.Length > 0)
                    .ObservesProperty(() => CurrentText);
            Infrastructure.Commands.SetTextCommand.RegisterCommand(_addTextCommand);
            Infrastructure.Commands.RemoveLastCharacterCommand.RegisterCommand(_removeLastCharacterCommand);
        }

	    private string _currentText = "";
	    private ICommand _addTextCommand;
	    private ICommand _removeLastCharacterCommand;

	    public string CurrentText
	    {
	        get { return _currentText; }
	        set { SetProperty(ref _currentText, value); }
	    }

	    public ICommand AddTextCommand => _addTextCommand;

	    public ICommand RemoveLastCharacterCommand
	    {
	        get { return _removeLastCharacterCommand; }
	        set { _removeLastCharacterCommand = value; }
	    }
	}
}
