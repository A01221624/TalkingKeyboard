// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Commands.cs" company="Numeral">
//   Copyright 2016 Fernando Ramírez Garibay
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace TalkingKeyboard.Infrastructure
{
    using Prism.Commands;

    /// <summary>
    ///     The commands for static, solution-wide access.
    /// </summary>
    public static class Commands
    {
        /// <summary>
        ///     Gets or sets the add multi-character text command.
        /// </summary>
        public static CompositeCommand AddMultiCharacterTextCommand { get; set; } = new CompositeCommand();

        /// <summary>
        ///     Gets or sets the set text command.
        /// </summary>
        public static CompositeCommand AppendTextCommand { get; set; } = new CompositeCommand();

        /// <summary>
        ///     Gets or sets the append text or navigate images command.
        /// </summary>
        public static CompositeCommand AppendTextOrNavigateImagesCommand { get; set; } = new CompositeCommand();

        /// <summary>
        ///     Gets or sets the clear text command.
        /// </summary>
        public static CompositeCommand ClearTextCommand { get; set; } = new CompositeCommand();

        /// <summary>
        ///     Gets or sets the decrease selection speed command.
        /// </summary>
        public static CompositeCommand DecreaseSelectionSpeedCommand { get; set; } = new CompositeCommand();

        /// <summary>
        ///     Gets or sets the increase selection speed command.
        /// </summary>
        public static CompositeCommand IncreaseSelectionSpeedCommand { get; set; } = new CompositeCommand();

        /// <summary>
        ///     Gets or sets the remove last character command.
        /// </summary>
        public static CompositeCommand RemoveLastCharacterCommand { get; set; } = new CompositeCommand();

        /// <summary>
        ///     Gets or sets the remove last multi character command.
        /// </summary>
        public static CompositeCommand RemoveLastMultiCharacterCommand { get; set; } = new CompositeCommand();

        /// <summary>
        ///     Gets or sets the remove last word command.
        /// </summary>
        public static CompositeCommand RemoveLastWordCommand { get; set; } = new CompositeCommand();

        /// <summary>
        ///     Gets or sets the remove last word without trim command.
        /// </summary>
        public static CompositeCommand RemoveLastWordWithoutTrimCommand { get; set; } = new CompositeCommand();

        /// <summary>
        ///     Gets or sets the return image menu command.
        /// </summary>
        public static CompositeCommand ReturnImageMenuCommand { get; set; } = new CompositeCommand();

        /// <summary>
        ///     Gets or sets the set shift down command.
        /// </summary>
        public static CompositeCommand SetShiftDownCommand { get; set; } = new CompositeCommand();

        /// <summary>
        ///     Gets or sets the speech synthesis command.
        /// </summary>
        public static CompositeCommand SpeechSynthesisCommand { get; set; } = new CompositeCommand();

        /// <summary>
        ///     Gets or sets the toggle selection enabled command.
        /// </summary>
        public static CompositeCommand ToggleSelectionEnabledCommand { get; set; } = new CompositeCommand();

        /// <summary>
        ///     Gets or sets the toggle theme command.
        /// </summary>
        public static CompositeCommand ToggleThemeCommand { get; set; } = new CompositeCommand();

        /// <summary>
        ///     Gets or sets the toggle shift down command.
        /// </summary>
        public static CompositeCommand ToggleShiftDownCommand { get; set; } = new CompositeCommand();

        /// <summary>
        ///     Gets or sets the add suggestion command.
        /// </summary>
        public static CompositeCommand WriteSuggestionCommand { get; set; } = new CompositeCommand();

        /// <summary>
        ///     Gets or sets the add save text command.
        /// </summary>
        public static CompositeCommand SaveTextCommand { get; set; } = new CompositeCommand();
    }
}