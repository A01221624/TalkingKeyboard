using System.Collections.ObjectModel;

namespace TalkingKeyboard.Infrastructure.ServiceInterfaces
{
    public interface ISuggestionSource
    {
        ObservableCollection<string> GetSuggestions(object basedOn = null);
    }
}