// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ITextModel.cs" company="Numeral">
//   Copyright 2016 Fernando Ramírez Garibay
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace TalkingKeyboard.Infrastructure.Controls
{
    /// <summary>
    /// The TextModel interface.
    /// </summary>
    public interface ITextModel
    {
        /// <summary>
        /// Gets or sets the current text.
        /// </summary>
        string CurrentText { get; set; }
    }
}