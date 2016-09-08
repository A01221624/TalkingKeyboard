// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SuggestionControlViewModel.cs" company="Numeral">
//   Copyright 2016 Fernando Ramírez Garibay
// </copyright>
// <summary>
//   Defines the SuggestionControlViewModel type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace TalkingKeyboard.Modules.SuggestionBoard.ViewModels
{
    using Prism.Mvvm;

    using TalkingKeyboard.Modules.SuggestionBoard.Model;

    public class SuggestionControlViewModel : BindableBase
    {
        private Suggestion suggestion;

        /// <summary>
        ///     Initializes a new instance of the <see cref="SuggestionControlViewModel" /> class.
        /// </summary>
        /// <param name="suggestion">The suggestion held by the control.</param>
        public SuggestionControlViewModel(string suggestion)
        {
            this.Suggestion = new Suggestion { Text = suggestion };
        }

        /// <summary>
        ///     Gets or sets the suggestion held by the control.
        /// </summary>
        /// <value>
        ///     The suggestion held by the control.
        /// </value>
        public Suggestion Suggestion
        {
            get
            {
                return this.suggestion;
            }

            set
            {
                this.SetProperty(ref this.suggestion, value);
            }
        }
    }
}