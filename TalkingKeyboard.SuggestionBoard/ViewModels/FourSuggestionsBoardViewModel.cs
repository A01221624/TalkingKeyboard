// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FourSuggestionsBoardViewModel.cs" company="Numeral">
//   Copyright 2016 Fernando Ramírez Garibay
// </copyright>
// <summary>
//   Defines the FourSuggestionsBoardViewModel type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace TalkingKeyboard.Modules.SuggestionBoard.ViewModels
{
    using System.Collections.ObjectModel;

    using Microsoft.Practices.Unity;

    using Prism.Events;
    using Prism.Mvvm;

    using TalkingKeyboard.Infrastructure;
    using TalkingKeyboard.Infrastructure.Controls;
    using TalkingKeyboard.Infrastructure.ServiceInterfaces;

    public class FourSuggestionsBoardViewModel : BindableBase, ISuggestionsViewModel
    {
        private readonly ISuggestionService _suggestionService;
        private readonly ITextModel _textModel;

        private ObservableCollection<string> _suggestions;

        /// <summary>
        ///     Initializes a new instance of the <see cref="FourSuggestionsBoardViewModel" /> class.
        /// </summary>
        /// <param name="unityContainer">The unity DI container (obtained through DI).</param>
        /// <param name="suggestionService">The suggestion service (obtained through DI).</param>
        /// <param name="textModel">The text model (obtained through DI).</param>
        /// <param name="eventAggregator">Provides pub/sub events (obtained through DI).</param>
        public FourSuggestionsBoardViewModel(
            IUnityContainer unityContainer,
            ISuggestionService suggestionService,
            ITextModel textModel,
            IEventAggregator eventAggregator)
        {
            this._suggestionService = suggestionService;
            this._textModel = textModel;
            this.Suggestions = new ObservableCollection<string>();
            eventAggregator.GetEvent<Events.TextUpdatedEvent>().Subscribe(
                () =>
                    {
                        this.Suggestions = suggestionService.ProvideSuggestions(this._textModel.CurrentText);
                        suggestionService.ClearMultiCharacterBuffer();
                    },
                ThreadOption.BackgroundThread,
                true);
            eventAggregator.GetEvent<Events.MultiTextUpdatedEvent>()
                .Subscribe(
                    () => { this.Suggestions = this._suggestionService.ProvideMultiCharacterSuggestions(); },
                    ThreadOption.BackgroundThread,
                    true);
        }

        /// <summary>
        ///     Gets or sets the suggestions.
        /// </summary>
        public ObservableCollection<string> Suggestions
        {
            get
            {
                return this._suggestions;
            }

            set
            {
                this.SetProperty(ref this._suggestions, value);
            }
        }
    }
}