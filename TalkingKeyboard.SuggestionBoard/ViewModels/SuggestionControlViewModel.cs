using Prism.Mvvm;
using TalkingKeyboard.Modules.SuggestionBoard.Model;

namespace TalkingKeyboard.Modules.SuggestionBoard.ViewModels
{
    public class SuggestionControlViewModel : BindableBase
    {
        private Suggestion _suggestion;

        public SuggestionControlViewModel(string suggestion)
        {
            Suggestion = new Suggestion {Text = suggestion};
        }

        public Suggestion Suggestion
        {
            get { return _suggestion; }
            set { SetProperty(ref _suggestion, value); }
        }
    }
}