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
    /// <summary>
    ///     Defines the interface for the multi-character text model. This is used for inter-module communication.
    /// </summary>
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