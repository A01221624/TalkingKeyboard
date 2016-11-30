// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IMultiKeyboardViewModel.cs" company="Numeral">
//   Copyright 2016 Fernando Ramírez Garibay
// </copyright>
// <summary>
//   Defines the IMultiKeyboardViewModel interface.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace TalkingKeyboard.Infrastructure.Models
{
    using System.Windows.Input;

    /// <summary>
    ///  Defines the interface for the multi-character text view-model. This is used for inter-module communication.
    /// </summary>
    public interface IMultiKeyboardViewModel
    {

        /// <summary>
        ///     Gets or sets the add multi-character command.
        /// </summary>
        /// <value>
        ///     The add multi-character command.
        /// </value>
        ICommand AddMultiCharacterCommand { get; set; }

        /// <summary>
        ///     Gets or sets the command for removing the last multi-character.
        /// </summary>
        /// <value>
        ///     The command for removing the last multi-character.
        /// </value>
        ICommand RemoveLastMultiCharacterCommand { get; set; }
    }
}