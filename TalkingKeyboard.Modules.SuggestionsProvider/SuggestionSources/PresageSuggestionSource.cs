using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ServiceModel;
using System.ServiceModel.MsmqIntegration;
using System.Text;
using TalkingKeyboard.Infrastructure.Helpers;
using TalkingKeyboard.Infrastructure.ServiceInterfaces;
using TalkingKeyboard.Modules.SuggestionsProvider.PresageService;

namespace TalkingKeyboard.Modules.SuggestionsProvider.SuggestionSources
{
    public class PresageSuggestionSource : ISuggestionSource
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
        /// <param name="basedOn"></param>
        /// <returns></returns>
        public ObservableCollection<string> GetSuggestions(object basedOn)
        {
            var currentText = basedOn as string;
            if (currentText == null) return new ObservableCollection<string>();
            string prefix, lastWord;
            StringEditHelper.SplitStringPrefixAndLastWord(currentText, out prefix, out lastWord);
            List<string> result = new List<string>();
            foreach (var s in channel.predict(prefix, lastWord))
            {
                var bytes = Encoding.Default.GetBytes(s);
                result.Add(Encoding.UTF8.GetString(bytes));
            }
            return new ObservableCollection<string>(result);
        }
    }
}