using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using Prism.Events;
using TalkingKeyboard.Infrastructure;
using TalkingKeyboard.Infrastructure.Controls;
using TalkingKeyboard.Infrastructure.ServiceInterfaces;

namespace TalkingKeyboard.Modules.SuggestionsProvider
{
    public class SuggestionService : ISuggestionService, IDisposable
    {
        private int _count = 0;
        public SuggestionService()
        {

        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public ObservableCollection<string> ProvideSuggestions(string basedOn)
        {
            return new ObservableCollection<string>() { _count++.ToString(), _count++.ToString(), _count++.ToString(), _count++.ToString() };
        }
    }
}