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

    using Prism.Events;
    using Prism.Mvvm;

    using TalkingKeyboard.Infrastructure;
    using TalkingKeyboard.Infrastructure.Controls;
    using TalkingKeyboard.Infrastructure.ServiceInterfaces;

    public class FourSuggestionsBoardViewModel : BindableBase
    {
        private readonly ISuggestionService suggestionService;

        private ObservableCollection<string> suggestions;

        /// <summary>
        ///     Initializes a new instance of the <see cref="FourSuggestionsBoardViewModel" /> class.
        /// </summary>
        /// <param name="suggestionService">The suggestion service (obtained through DI).</param>
        /// <param name="eventAggregator">Provides pub/sub events (obtained through DI).</param>
        public FourSuggestionsBoardViewModel(
            ISuggestionService suggestionService,
            IEventAggregator eventAggregator)
        {
            this.suggestionService = suggestionService;
            eventAggregator.GetEvent<Events.TextUpdatedEvent>().Subscribe(
                () =>
                    {
                        if (this.Suggestions == null)
                        {
                            this.Suggestions = new ObservableCollection<string>();
                        }

                        this.Suggestions = suggestionService.ProvideSuggestions();
                        suggestionService.ClearMultiCharacterBuffer();
                    },
                ThreadOption.BackgroundThread,
                true);
            eventAggregator.GetEvent<Events.MultiTextUpdatedEvent>().Subscribe(
                () =>
                    {
                        if (this.Suggestions == null)
                        {
                            this.Suggestions = new ObservableCollection<string>();
                        }

                        this.Suggestions = this.suggestionService.ProvideMultiCharacterSuggestions();
                    },
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
                return this.suggestions;
            }

            set
            {
                this.SetProperty(ref this.suggestions, value);
            }
        }
    }
}