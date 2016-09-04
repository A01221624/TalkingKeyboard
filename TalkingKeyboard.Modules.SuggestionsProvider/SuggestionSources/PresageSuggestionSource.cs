using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ServiceModel;
using System.ServiceModel.MsmqIntegration;
using TalkingKeyboard.Infrastructure.Helpers;
using TalkingKeyboard.Modules.SuggestionsProvider.PresageService;

namespace TalkingKeyboard.Modules.SuggestionsProvider.SuggestionSources
{
    public class PresageSuggestionSource
    {
        private PresageService.PresageChannel channel;

        public PresageSuggestionSource()
        {
            NetNamedPipeBinding binding = new NetNamedPipeBinding();
            EndpointAddress address = new EndpointAddress("net.pipe://localhost/PresageService/v1/presage");
            ChannelFactory<PresageService.PresageChannel> channelFactory = new ChannelFactory<PresageChannel>(binding, address);
            channel = channelFactory.CreateChannel();
        }

        private readonly PresageService.PresageClient _client;

        public string GetDummySuggestions()
        {
            var test = channel.context();
            var t = channel.predict("hello", "my");
            var testo = t[0];
            return testo;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="currentText"></param>
        /// <returns></returns>
        public ObservableCollection<string> GetSuggestions(string currentText)
        {
            string prefix, lastWord;
            StringEditHelper.SplitStringPrefixAndLastWord(currentText, out prefix, out lastWord);
            return new ObservableCollection<string>(channel.predict(prefix, lastWord));
        }
    }
}