// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ISuggestionSource.cs" company="Numeral">
//   Copyright 2016 Fernando Ramírez Garibay
// </copyright>
// <summary>
//   Defines the ISuggestionSource type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace TalkingKeyboard.Infrastructure.ServiceInterfaces
{
    using System.Collections.ObjectModel;

    /// <summary>
    ///     Interface for suggestion sources.
    /// </summary>
    public interface ISuggestionSource
    {
        /// <summary>
        ///     Gets the suggestions.
        /// </summary>
        /// <param name="basedOn">Possible object on which the suggestions are based (may be null).</param>
        /// <returns>Returns an <see cref="ObservableCollection{T}" /> of strings containing the suggestions.</returns>
        /// <remarks>
        ///     The suggestions may be sorted or not. It is up to the implementing type to decide and for the suggestion board view
        ///     model to take into account.
        /// </remarks>
        ObservableCollection<string> GetSuggestions(object basedOn = null);
    }
}