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