// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Commands.cs" company="Numeral">
//   Copyright 2016 Fernando Ramírez Garibay
// </copyright>
// <summary>
//   Provides static solution-wide access to commands.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace TalkingKeyboard.Infrastructure
{
    using Prism.Commands;

    /// <summary>
    /// The commands for static, solution-wide access.
    /// </summary>
    public static class Commands
    {
        /// <summary>
        /// Gets or sets the set text command.
        /// </summary>
        public static CompositeCommand SetTextCommand { get; set; } = new CompositeCommand();

        /// <summary>
        /// Gets or sets the set shift down command.
        /// </summary>
        public static CompositeCommand SetShiftDownCommand { get; set; } = new CompositeCommand();

        /// <summary>
        /// Gets or sets the speech synthesis command.
        /// </summary>
        public static CompositeCommand SpeechSynthesisCommand { get; set; } = new CompositeCommand();

        /// <summary>
        /// Gets or sets the toggle shift down command.
        /// </summary>
        public static CompositeCommand ToggleShiftDownCommand { get; set; } = new CompositeCommand();

        /// <summary>
        /// Gets or sets the remove last character command.
        /// </summary>
        public static CompositeCommand RemoveLastCharacterCommand { get; set; } = new CompositeCommand();

        /// <summary>
        /// Gets or sets the remove last word command.
        /// </summary>
        public static CompositeCommand RemoveLastWordCommand { get; set; } = new CompositeCommand();

        /// <summary>
        /// Gets or sets the add suggestion command.
        /// </summary>
        public static CompositeCommand AddSuggestionCommand { get; set; } = new CompositeCommand();

        /// <summary>
        /// Gets or sets the add multi-key text command.
        /// </summary>
        public static CompositeCommand AddMultikeyTextCommand { get; set; } = new CompositeCommand();

        /// <summary>
        /// Gets or sets the remove last multi character command.
        /// </summary>
        public static CompositeCommand RemoveLastMultiCharacterCommand { get; set; } = new CompositeCommand();

    }
}