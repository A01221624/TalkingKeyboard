using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Microsoft.Practices.Unity;
using Prism.Events;
using TalkingKeyboard.Infrastructure;
using TalkingKeyboard.Infrastructure.Controls;
using TalkingKeyboard.Infrastructure.ServiceInterfaces;
using TalkingKeyboard.Modules.SuggestionBoard.Model;

namespace TalkingKeyboard.Modules.SuggestionBoard.ViewModels
{
    public class FourSuggestionsBoardViewModel : BindableBase, ISuggestionsViewModel
    {
        public FourSuggestionsBoardViewModel(IUnityContainer unityContainer, ISuggestionService suggestionService, ITextModel textModel, IEventAggregator eventAggregator)
        {
            Suggestions = new ObservableCollection<string>();
            eventAggregator.GetEvent<TextUpdatedEvent>()
                .Subscribe(() =>
                {
                    Suggestions = suggestionService.ProvideSuggestions(textModel.CurrentText);
                    
                }, ThreadOption.BackgroundThread, true);
        }

        private ObservableCollection<string> _suggestions;

        public ObservableCollection<string> Suggestions
        {
            get { return _suggestions; }
            set { SetProperty(ref _suggestions, value); }
        }
    }
}
