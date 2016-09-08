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
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.ServiceModel;

    using TalkingKeyboard.Infrastructure.Helpers;
    using TalkingKeyboard.Infrastructure.ServiceInterfaces;
    using TalkingKeyboard.Modules.SuggestionsProvider.PresageService;

    public class PresageSuggestionSource : ISuggestionSource
    {
        private readonly PresageChannel channel;

        /// <summary>
        ///     Initializes a new instance of the <see cref="PresageSuggestionSource" /> class.
        /// </summary>
        public PresageSuggestionSource()
        {
            var binding = new NetNamedPipeBinding();
            var address = new EndpointAddress("net.pipe://localhost/PresageService/v1/presage");
            var channelFactory = new ChannelFactory<PresageChannel>(binding, address);
            this.channel = channelFactory.CreateChannel();
        }

        /// <summary>
        ///     Gets the suggestions.
        /// </summary>
        /// <param name="basedOn">
        ///     String (as object) with the text on which the suggestions are based (i.e. the currently displayed
        ///     text).
        /// </param>
        /// <returns>
        ///     Returns an <see cref="T:System.Collections.ObjectModel.ObservableCollection`1" /> of strings containing the
        ///     suggestions.
        /// </returns>
        /// <remarks>
        ///     <para>
        ///         The suggestions are sorted by Presage. Configuration of Presage is done through an XML file located in its
        ///         installation folder.
        ///     </para>
        ///     <para>
        ///         Conversion between UTF8 (for this module) and Default encoding (for Presage) is performed when communicating
        ///         with the Presage Service.
        ///     </para>
        /// </remarks>
        public ObservableCollection<string> GetSuggestions(object basedOn)
        {
            var currentText = basedOn as string;
            if (currentText == null)
            {
                return new ObservableCollection<string>();
            }

            string prefix, lastWord;
            StringEditHelper.SplitStringPrefixAndLastWord(currentText, out prefix, out lastWord);

            prefix = StringEditHelper.ConvertToDefaultEncodingFromUtf8(prefix);
            lastWord = StringEditHelper.ConvertToDefaultEncodingFromUtf8(lastWord);

            var result =
                this.channel.predict(prefix, lastWord)
                    .Select(StringEditHelper.ConvertToUtf8FromDefaultEncoding)
                    .ToList();

            return new ObservableCollection<string>(result);
        }
    }
}