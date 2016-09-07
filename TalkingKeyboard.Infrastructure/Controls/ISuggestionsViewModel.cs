// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ISuggestionsViewModel.cs" company="Numeral">
//   Copyright 2016 Fernando Ramírez Garibay
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace TalkingKeyboard.Infrastructure.Controls
{
    using System.Collections.ObjectModel;

    /// <summary>
    /// The SuggestionsViewModel interface.
    /// </summary>
    public interface ISuggestionsViewModel
    {
        /// <summary>
        /// Gets or sets the suggestions.
        /// </summary>
        ObservableCollection<string> Suggestions { get; set; }
    }
}