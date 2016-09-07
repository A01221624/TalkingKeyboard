﻿using System.Collections.ObjectModel;
using Microsoft.Practices.Unity;
using Prism.Events;
using Prism.Mvvm;
using TalkingKeyboard.Infrastructure;
using TalkingKeyboard.Infrastructure.Controls;
using TalkingKeyboard.Infrastructure.ServiceInterfaces;

namespace TalkingKeyboard.Modules.SuggestionBoard.ViewModels
{
    public class FourSuggestionsBoardViewModel : BindableBase, ISuggestionsViewModel
    {
        private readonly ISuggestionService _suggestionService;
        private readonly ITextModel _textModel;

        private ObservableCollection<string> _suggestions;

        public FourSuggestionsBoardViewModel(IUnityContainer unityContainer, ISuggestionService suggestionService,
            ITextModel textModel, IEventAggregator eventAggregator)
        {
            _suggestionService = suggestionService;
            _textModel = textModel;
            Suggestions = new ObservableCollection<string>();
            eventAggregator.GetEvent<Events.TextUpdatedEvent>()
                .Subscribe(() =>
                {
                    Suggestions = suggestionService.ProvideSuggestions(_textModel.CurrentText);
                    suggestionService.ClearMultiCharacterBuffer();
                }, ThreadOption.BackgroundThread, true);
            eventAggregator.GetEvent<Events.MultiTextUpdatedEvent>()
                .Subscribe(() => { Suggestions = _suggestionService.ProvideMultiCharacterSuggestions(); },
                    ThreadOption.BackgroundThread, true);
        }

        public ObservableCollection<string> Suggestions
        {
            get { return _suggestions; }
            set { SetProperty(ref _suggestions, value); }
        }
    }
}