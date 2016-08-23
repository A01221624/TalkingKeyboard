using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;

namespace TalkingKeyboard.Modules.CentralTextModule.ViewModels
{
	public class TextViewModel : BindableBase
	{
        public TextViewModel()
        {

        }

	    private string _currentText;

	    public string CurrentText
	    {
	        get { return _currentText; }
	        set { SetProperty(ref _currentText, value); }
	    }
	}
}
