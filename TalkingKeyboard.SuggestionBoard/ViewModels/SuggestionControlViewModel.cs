using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using TalkingKeyboard.Modules.SuggestionBoard.Model;

namespace TalkingKeyboard.Modules.SuggestionBoard.ViewModels
{
    public class SuggestionControlViewModel : BindableBase
    {
        public SuggestionControlViewModel(string suggestion)
        {
            Suggestion = new Suggestion() {Text = suggestion};
        }

        private Suggestion _suggestion;

        public Suggestion Suggestion
        {
            get { return _suggestion; }
            set { SetProperty(ref _suggestion, value); }
        }
    }
}
