using System.Collections.ObjectModel;

namespace TalkingKeyboard.Infrastructure.Controls
{
    public interface ISuggestionsViewModel
    {
        ObservableCollection<string> Suggestions { get; set; }
    }
}