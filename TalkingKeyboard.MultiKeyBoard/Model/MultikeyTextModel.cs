// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MultikeyTextModel.cs" company="Numeral">
//   Copyright 2016 Fernando Ramírez Garibay
// </copyright>
// <summary>
//   Defines the MultikeyTextModel type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace TalkingKeyboard.Modules.MultiKeyBoard.Model
{
    using TalkingKeyboard.Infrastructure.Models;

    /// <summary>
    ///     This class contains the model of currently held multi-character text. It is used by the suggestion service module
    ///     to disambiguate between the different possible words which can be formed by using the previously selected
    ///     characters and to suggest auto-completion candidates. The current version of the application holds an alternative
    ///     model directly in the suggestion service; both approaches have benefits and thus non-operational class is included.
    /// </summary>
    /// <seealso cref="TalkingKeyboard.Infrastructure.Models.IMultiKeyTextModel" />
    public class MultikeyTextModel : IMultiKeyTextModel
    {
        /// <summary>
        ///     Gets or sets the current multi-character text.
        /// </summary>
        /// <value>
        ///     The current multi-character text.
        /// </value>
        public string CurrentMultiCharacterText { get; set; }
    }
}