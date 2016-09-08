// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PresageSuggestionSource.cs" company="Numeral">
//   Copyright 2016 Fernando Ramírez Garibay
// </copyright>
// <summary>
//   Defines the PresageSuggestionSource type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace TalkingKeyboard.Modules.SuggestionsProvider.SuggestionSources
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ServiceModel;
    using System.Text;

    using TalkingKeyboard.Infrastructure.Helpers;
    using TalkingKeyboard.Infrastructure.ServiceInterfaces;
    using TalkingKeyboard.Modules.SuggestionsProvider.PresageService;

    public class PresageSuggestionSource : ISuggestionSource
    {
        private readonly PresageClient _client;
        private readonly PresageChannel channel;

        public PresageSuggestionSource()
        {
            var binding = new NetNamedPipeBinding();
            var address = new EndpointAddress("net.pipe://localhost/PresageService/v1/presage");
            var channelFactory = new ChannelFactory<PresageChannel>(binding, address);
            this.channel = channelFactory.CreateChannel();
        }

        public string GetDummySuggestions()
        {
            var test = this.channel.context();
            var t = this.channel.predict("hello", "my");
            var testo = t[0];
            return testo;
        }

        /// <summary>
        /// </summary>
        /// <param name="basedOn"></param>
        /// <returns></returns>
        public ObservableCollection<string> GetSuggestions(object basedOn)
        {
            var currentText = basedOn as string;
            if (currentText == null)
            {
                return new ObservableCollection<string>();
            }

            string prefix, lastWord;
            StringEditHelper.SplitStringPrefixAndLastWord(currentText, out prefix, out lastWord);
            var result = new List<string>();
            foreach (var s in this.channel.predict(prefix, lastWord))
            {
                var bytes = Encoding.Default.GetBytes(s);
                result.Add(Encoding.UTF8.GetString(bytes));
            }

            return new ObservableCollection<string>(result);
        }
    }
}