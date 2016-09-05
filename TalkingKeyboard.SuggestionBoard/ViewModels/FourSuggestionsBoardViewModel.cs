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
using TalkingKeyboard.Infrastructure.Models;
using TalkingKeyboard.Infrastructure.ServiceInterfaces;
using TalkingKeyboard.Modules.SuggestionBoard.Model;

namespace TalkingKeyboard.Modules.SuggestionBoard.ViewModels
{
    public class FourSuggestionsBoardViewModel : BindableBase, ISuggestionsViewModel
    {
        private readonly ISuggestionService _suggestionService;
        private readonly ITextModel _textModel;

        public FourSuggestionsBoardViewModel(IUnityContainer unityContainer, ISuggestionService suggestionService, ITextModel textModel, IEventAggregator eventAggregator)
        {
            _suggestionService = suggestionService;
            _textModel = textModel;
            Suggestions = new ObservableCollection<string>();
            eventAggregator.GetEvent<TextUpdatedEvent>()
                .Subscribe(() =>
                {
                    Suggestions = suggestionService.ProvideSuggestions(_textModel.CurrentText);
                    suggestionService.ClearMultiCharacterBuffer();
                }, ThreadOption.BackgroundThread, true);
            eventAggregator.GetEvent<MultiTextUpdatedEvent>().Subscribe(() =>
            {
                Suggestions = _suggestionService.ProvideMultikeySuggestions();
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
