// --------------------------------------------------------------------------------------------------------------------
// <copyright file="QwertySpanishMultiKeyboardViewModel.cs" company="Numeral">
//   Copyright 2016 Fernando Ramírez Garibay
// </copyright>
// <summary>
//   Defines the QwertySpanishMultiKeyboardViewModel type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace TalkingKeyboard.Modules.MultiKeyBoard.ViewModels
{
    using System.Windows.Input;

    using Prism.Commands;
    using Prism.Events;
    using Prism.Mvvm;

    using TalkingKeyboard.Infrastructure;
    using TalkingKeyboard.Infrastructure.ServiceInterfaces;

    /// <summary>
    ///     This class contains the logic related to edition of the multi-character buffer. Note that the view-model currently
    ///     communicates directly with the suggestion service, this is a design decision for the current version of the
    ///     application; an alternative would be to modify the model instance and expose an interface for the suggestion
    ///     service to consume the necessary information.
    /// </summary>
    /// <seealso cref="Prism.Mvvm.BindableBase" />
    public class QwertySpanishMultiKeyboardViewModel : BindableBase
    {
        private readonly IEventAggregator eventAggregator;
        private readonly ISuggestionService suggestionService;

        /// <summary>
        ///     Initializes a new instance of the <see cref="QwertySpanishMultiKeyboardViewModel" /> class.
        /// </summary>
        /// <param name="eventAggregator">Provides pub/sub events (obtained through DI).</param>
        /// <param name="suggestionService">The suggestion service (obtained through DI).</param>
        public QwertySpanishMultiKeyboardViewModel(
            IEventAggregator eventAggregator,
            ISuggestionService suggestionService)
        {
            this.eventAggregator = eventAggregator;
            this.suggestionService = suggestionService;
            this.AddMultiCharacterCommand = new DelegateCommand<string>(this.AddMultiCharacter);
            this.RemoveLastMultiCharacterCommand = new DelegateCommand(this.RemoveLastMultiCharacter);
            Commands.AddMultiCharacterTextCommand.RegisterCommand(this.AddMultiCharacterCommand);
            Commands.RemoveLastMultiCharacterCommand.RegisterCommand(this.RemoveLastMultiCharacterCommand);
        }

        /// <summary>
        ///     Gets or sets the add multi-character command.
        /// </summary>
        /// <value>
        ///     The add multi-character command.
        /// </value>
        public ICommand AddMultiCharacterCommand { get; set; }

        /// <summary>
        ///     Gets or sets the command for removing the last multi-character.
        /// </summary>
        /// <value>
        ///     The command for removing the last multi-character.
        /// </value>
        public ICommand RemoveLastMultiCharacterCommand { get; set; }

        /// <summary>
        ///     Adds a multi-character.
        /// </summary>
        /// <param name="s">The s.</param>
        private void AddMultiCharacter(string s)
        {
            this.suggestionService.AddMultiCharacterText(s);
            this.eventAggregator.GetEvent<Events.MultiTextUpdatedEvent>().Publish();
        }

        /// <summary>
        ///     Removes the last multi-character.
        /// </summary>
        private void RemoveLastMultiCharacter()
        {
            this.suggestionService.RemoveLastMultiCharacter();
            this.eventAggregator.GetEvent<Events.MultiTextUpdatedEvent>().Publish();
        }
    }
}