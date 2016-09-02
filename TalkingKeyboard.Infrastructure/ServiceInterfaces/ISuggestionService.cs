using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace TalkingKeyboard.Infrastructure.ServiceInterfaces
{
    public interface ISuggestionService
    {
        ObservableCollection<string> ProvideSuggestions(string basedOn);
    }
}