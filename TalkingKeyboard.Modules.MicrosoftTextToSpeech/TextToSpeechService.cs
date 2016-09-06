using System.Speech.Synthesis;
using System.Windows.Input;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using TalkingKeyboard.Infrastructure;
using TalkingKeyboard.Infrastructure.Controls;
using TalkingKeyboard.Infrastructure.ServiceInterfaces;

namespace TalkingKeyboard.Modules.MicrosoftTextToSpeech
{
    public class TextToSpeechService : BindableBase, ITextToSpeechService
    {
        private readonly SpeechSynthesizer _synthesizer = new SpeechSynthesizer();

        private string _currentText;

        public TextToSpeechService(ITextModel textModel, IEventAggregator eventAggregator)
        {
            TextModel = textModel;
            SpeechSynthesisCommand =
                new DelegateCommand(SayCurrentText, SpeechSynthesisCanExecute).ObservesProperty(() => CurrentText);
            Commands.SpeechSynthesisCommand.RegisterCommand(SpeechSynthesisCommand);
            eventAggregator.GetEvent<TextUpdatedEvent>().Subscribe(() => CurrentText = TextModel.CurrentText);
        }

        private ITextModel TextModel { get; }

        public string CurrentText
        {
            get { return _currentText; }
            set { SetProperty(ref _currentText, value); }
        }

        public ICommand SpeechSynthesisCommand { get; set; }

        public void Say(string s)
        {
            _synthesizer.SpeakAsync(s);
        }

        public void SayCurrentText()
        {
            _synthesizer.SpeakAsync(TextModel.CurrentText);
        }

        private bool SpeechSynthesisCanExecute()
        {
            return !string.IsNullOrEmpty(CurrentText);
        }
    }
}