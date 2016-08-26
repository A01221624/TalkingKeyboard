using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;

namespace TalkingKeyboard.Modules.CentralTextModule.ViewModels
{
	public class TextViewModel : BindableBase
	{
        public TextViewModel()
        {
            _addTextCommand = new DelegateCommand<string>(s => CurrentText = s);
            Infrastructure.Commands.SetTextCommand.RegisterCommand(_addTextCommand);
        }

	    private string _currentText;
	    private ICommand _addTextCommand;

	    public string CurrentText
	    {
	        get { return _currentText; }
	        set { SetProperty(ref _currentText, value); }
	    }

	    public ICommand AddTextCommand => _addTextCommand;
	}
}
