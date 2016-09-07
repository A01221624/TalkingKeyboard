// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IMultiKeyTextModel.cs" company="Numeral">
//   Copyright 2016 Fernando Ramírez Garibay
// </copyright>
// <summary>
//   Defines the IMultiKeyTextModel interface.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace TalkingKeyboard.Infrastructure.Models
{
    public interface IMultiKeyTextModel
    {
        /// <summary>
        ///     Gets or sets the current multi-character text.
        /// </summary>
        /// <value>
        ///     The current multi-character text.
        /// </value>
        string CurrentMultiCharacterText { get; set; }
    }
}