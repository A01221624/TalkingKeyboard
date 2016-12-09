// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IImageBoardViewModel.cs" company="Numeral">
//   Copyright 2016 Fernando Ramírez Garibay
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace TalkingKeyboard.Infrastructure.Models
{
    using System.Windows.Input;

    /// <summary>
    ///     Defines the interface for the image view-model. This is used for inter-module communication.
    /// </summary>
    public interface IImageBoardViewModel
    {
        /// <summary>
        ///     Gets or sets the add image text command.
        /// </summary>
        /// <value>
        ///     The add image text command.
        /// </value>
        ICommand AddImageTextCommand { get; set; }
    }
}