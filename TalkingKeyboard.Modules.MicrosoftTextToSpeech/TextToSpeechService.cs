// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TextToSpeechService.cs" company="Numeral">
//   Copyright 2016 Fernando Ramírez Garibay
// </copyright>
// <summary>
//   Defines the TextToSpeechService type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace TalkingKeyboard.Modules.MicrosoftTextToSpeech
{
    using System.Speech.Synthesis;
    using System.Windows.Input;

    using Prism.Commands;
    using Prism.Events;
    using Prism.Mvvm;

    using TalkingKeyboard.Infrastructure;
    using TalkingKeyboard.Infrastructure.Controls;
    using TalkingKeyboard.Infrastructure.ServiceInterfaces;

    public class TextToSpeechService : BindableBase, ITextToSpeechService
    {
        private readonly SpeechSynthesizer synthesizer = new SpeechSynthesizer();

        private string currentText;

        /// <summary>
        ///     Initializes a new instance of the <see cref="TextToSpeechService" /> class.
        /// </summary>
        /// <param name="textModel">The text model (obtained through DI).</param>
        /// <param name="eventAggregator">Provides pub/sub events (obtained through DI).</param>
        public TextToSpeechService(ITextModel textModel, IEventAggregator eventAggregator)
        {
            this.TextModel = textModel;
            this.SpeechSynthesisCommand =
                new DelegateCommand(this.SayCurrentText, this.SpeechSynthesisCanExecute).ObservesProperty(
                    () => this.CurrentText);
            Commands.SpeechSynthesisCommand.RegisterCommand(this.SpeechSynthesisCommand);
            eventAggregator.GetEvent<Events.TextUpdatedEvent>()
                .Subscribe(() => this.CurrentText = this.TextModel.CurrentText);
        }

        /// <summary>
        ///     Gets or sets the current text.
        /// </summary>
        /// <value>
        ///     The current text.
        /// </value>
        public string CurrentText
        {
            get
            {
                return this.currentText;
            }

            set
            {
                this.SetProperty(ref this.currentText, value);
            }
        }

        /// <summary>
        ///     Gets or sets the speech synthesis command.
        /// </summary>
        /// <value>
        ///     The speech synthesis command.
        /// </value>
        /// <seealso cref="SpeechSynthesisCanExecute" />
        public ICommand SpeechSynthesisCommand { get; set; }

        /// <summary>
        ///     Gets the text model.
        /// </summary>
        /// <value>
        ///     The text model.
        /// </value>
        private ITextModel TextModel { get; }

        /// <summary>
        ///     Speaks the specified string.
        /// </summary>
        /// <param name="s">The string to speech-synthesize</param>
        public void Say(string s)
        {
            this.synthesizer.SpeakAsync(s);
        }

        /// <summary>
        ///     Speaks the current text.
        /// </summary>
        public void SayCurrentText()
        {
            this.synthesizer.SpeakAsync(this.TextModel.CurrentText);
        }

        /// <summary>
        ///     Determines whether speech synthesis may be performed.
        /// </summary>
        /// <returns>True if speech synthesis can be performed (current text is not empty or null).</returns>
        /// <seealso cref="SpeechSynthesisCommand" />
        private bool SpeechSynthesisCanExecute()
        {
            return !string.IsNullOrEmpty(this.CurrentText);
        }
    }
}