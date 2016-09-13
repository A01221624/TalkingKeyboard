// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ISuggestionService.cs" company="Numeral">
//   Copyright 2016 Fernando Ramírez Garibay
// </copyright>
// <summary>
//   Defines the ISuggestionService interface.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace TalkingKeyboard.Infrastructure.ServiceInterfaces
{
    using System.Collections.ObjectModel;

    public interface ISuggestionService
    {
        /// <summary>
        ///     Adds multi-character text to the buffer.
        /// </summary>
        /// <param name="s">The multi-character text to add.</param>
        void AddMultiCharacterText(string s);

        /// <summary>
        ///     Clears the multi character buffer.
        /// </summary>
        void ClearMultiCharacterBuffer();

        /// <summary>
        ///     Provides the multi character suggestions (disambiguation and auto-complete).
        /// </summary>
        /// <returns>Returns an <see cref="ObservableCollection{T}" /> of strings containing the suggestions.</returns>
        /// <remarks>
        ///     The suggestions may be sorted or not. It is up to the implementing type to decide and for the suggestion board view
        ///     model to take into account.
        /// </remarks>
        ObservableCollection<string> ProvideMultiCharacterSuggestions();

        /// <summary>
        ///     Gets the suggestions leaving the implementer to retrieve any necessary models.
        /// </summary>
        /// <returns>Returns an <see cref="ObservableCollection{T}" /> of strings containing the suggestions.</returns>
        /// <remarks>
        ///     The suggestions may be sorted or not. It is up to the implementing type to decide and for the suggestion board view
        ///     model to take into account.
        /// </remarks>
        ObservableCollection<string> ProvideSuggestions();

        /// <summary>
        ///     Removes the last multi-character from the buffer.
        /// </summary>
        void RemoveLastMultiCharacter();
    }
}