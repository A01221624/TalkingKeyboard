// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TextViewModel.cs" company="Numeral">
//   Copyright 2016 Fernando Ramírez Garibay
// </copyright>
// <summary>
//   Defines the TextViewModel type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace TalkingKeyboard.Modules.CentralTextModule.ViewModels
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Windows.Input;

    using Prism.Commands;
    using Prism.Events;
    using Prism.Mvvm;

    using TalkingKeyboard.Infrastructure;
    using TalkingKeyboard.Infrastructure.Constants;
    using TalkingKeyboard.Infrastructure.Controls;
    using TalkingKeyboard.Infrastructure.Helpers;

    public class TextViewModel : BindableBase, ITextModel
    {
        private readonly IEventAggregator eventAggregator;
        private string accentBuffer = string.Empty;

        private string currentText = string.Empty;

        /// <summary>
        ///     Initializes a new instance of the <see cref="TextViewModel" /> class.
        /// </summary>
        /// <param name="eventAggregator">Provides pub/sub events (obtained through DI).</param>
        public TextViewModel(IEventAggregator eventAggregator)
        {
            this.eventAggregator = eventAggregator;
            this.AppendTextCommand = new DelegateCommand<string>(this.AppendText);
            this.RemoveLastCharacterCommand =
                new DelegateCommand(
                    () => this.CurrentText = this.CurrentText.Remove(this.CurrentText.Length - 1),
                    () => this.CurrentText.Length > 0).ObservesProperty(() => this.CurrentText);
            this.RemoveLastWordCommand =
                new DelegateCommand(() => { this.CurrentText = StringEditHelper.RemoveLastWord(this.CurrentText); });
            Commands.AppendTextCommand.RegisterCommand(this.AppendTextCommand);
            Commands.RemoveLastCharacterCommand.RegisterCommand(this.RemoveLastCharacterCommand);
            Commands.RemoveLastWordCommand.RegisterCommand(this.RemoveLastWordCommand);
        }

        /// <summary>
        ///     Gets the command for adding a string to the current text.
        /// </summary>
        /// <value>
        ///     The command for adding a string to the current text.
        /// </value>
        public ICommand AppendTextCommand { get; }

        /// <summary>
        ///     Gets or sets the current text.
        /// </summary>
        public string CurrentText
        {
            get
            {
                return this.currentText;
            }

            set
            {
                this.SetProperty(ref this.currentText, value);
                this.eventAggregator.GetEvent<Events.TextUpdatedEvent>().Publish();
            }
        }

        /// <summary>
        ///     Gets or sets the command for removing the last character from the current text.
        /// </summary>
        /// <value>
        ///     The command for removing the last character from the current text..
        /// </value>
        public ICommand RemoveLastCharacterCommand { get; set; }

        /// <summary>
        ///     Gets or sets the command for removing the last word from the current text.
        /// </summary>
        /// <value>
        ///     The command for removing the last word from the current text.
        /// </value>
        public ICommand RemoveLastWordCommand { get; set; }

        /// <summary>
        ///     Appends a string to the current text.
        /// </summary>
        /// <param name="s">The string to be appended.</param>
        private void AppendText(string s)
        {
            if (s.Length == 0)
            {
                return;
            }

            var newIsAccent = CharacterClasses.Accents.Contains(s[0]);
            if (!newIsAccent)
            {
                s = this.CombineWithAccentBuffer(s);
                this.RemoveWhitespaceIfNecessaryBasedOn(s);
                this.CurrentText += s;
                this.accentBuffer = string.Empty;
            }
            else
            {
                this.accentBuffer = s;
            }
        }

        /// <summary>
        ///     Combines a pending accent to a new character.
        /// </summary>
        /// <param name="s">The character to be accentuated.</param>
        /// <returns>
        ///     The accentuated character if it can be accentuated with the pending accent; otherwise returns the original
        ///     character.
        /// </returns>
        private string CombineWithAccentBuffer(string s)
        {
            Dictionary<char, char> possibleAccentuations;
            char accentedCharacter;
            if ((this.accentBuffer.Length > 0)
                && CharacterClasses.AccentLookup.TryGetValue(s[0], out possibleAccentuations)
                && possibleAccentuations.TryGetValue(this.accentBuffer[0], out accentedCharacter))
            {
                s = accentedCharacter.ToString();
            }

            return s;
        }

        /// <summary>
        ///     Removes trailing white-spaces if necessary given the new character to be appended.
        /// </summary>
        /// <param name="s">The new character to be appended.</param>
        /// <remarks>
        ///     This is necessary, for example, when there is a space and a comma is to be added: "Hello ," versus "Hello,"; this
        ///     case occurs when the suggestion module is used, which auto-adds spaces after words.
        /// </remarks>
        private void RemoveWhitespaceIfNecessaryBasedOn(string s)
        {
            if ((this.CurrentText.Length > 0)
                && CharacterClasses.PrecededByNonwhitespaceFollowedByWhitespace.Contains(s[0])
                && CharacterClasses.Whitespace.Contains(this.CurrentText[this.CurrentText.Length - 1]))
            {
                this.CurrentText = this.CurrentText.Remove(this.CurrentText.Length - 1);
            }
        }
    }
}