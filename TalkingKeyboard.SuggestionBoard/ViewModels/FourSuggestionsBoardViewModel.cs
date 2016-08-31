using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Practices.Unity;
using TalkingKeyboard.Infrastructure.Controls;
using TalkingKeyboard.Modules.SuggestionBoard.Model;

namespace TalkingKeyboard.Modules.SuggestionBoard.ViewModels
{
    public class FourSuggestionsBoardViewModel : BindableBase
    {
        public FourSuggestionsBoardViewModel(IUnityContainer unityContainer)
        {
            SuggestionCollection = new List<SuggestionControlViewModel>(4)
            {
                new SuggestionControlViewModel("hola"),
                new SuggestionControlViewModel("lola"),
                new SuggestionControlViewModel("gol"),
                new SuggestionControlViewModel("ola")
            };
        }

        private List<SuggestionControlViewModel> _suggestionCollection;

        public List<SuggestionControlViewModel> SuggestionCollection
        {
            get { return _suggestionCollection; }
            set { SetProperty(ref _suggestionCollection, value); }
        }
    }
}
