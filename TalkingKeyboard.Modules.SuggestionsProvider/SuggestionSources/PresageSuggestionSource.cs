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
    using System;
    using System.Collections.ObjectModel;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.ServiceModel;

    using TalkingKeyboard.Infrastructure.Helpers;
    using TalkingKeyboard.Infrastructure.ServiceInterfaces;
    using TalkingKeyboard.Modules.SuggestionsProvider.PresageService;

    /// <summary>
    ///     This class provides auto-completion suggestions obtained from an external application called Presage which uses an
    ///     n-gram database to perform text prediction.
    /// </summary>
    /// <remarks>
    ///     For more information on Presage, see http://presage.sourceforge.net/
    /// </remarks>
    /// <seealso cref="TalkingKeyboard.Infrastructure.ServiceInterfaces.ISuggestionSource" />
    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1650:ElementDocumentationMustBeSpelledCorrectly",
         Justification = "Web addresses exempt of spell-check.")]
    public class PresageSuggestionSource : ISuggestionSource
    {
        private PresageChannel channel;

        /// <summary>
        ///     Initializes a new instance of the <see cref="PresageSuggestionSource" /> class.
        /// </summary>
        public PresageSuggestionSource()
        {
            this.InitializeChannel();
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

            if ((this.channel.State != CommunicationState.Opened) && (this.channel.State != CommunicationState.Opening))
            {
                this.InitializeChannel();
            }

            string prefix, lastWord;
            StringEditHelper.SplitStringPrefixAndLastWord(currentText, out prefix, out lastWord);

            prefix = StringEditHelper.ConvertToDefaultEncodingFromUtf8(prefix);
            lastWord = StringEditHelper.ConvertToDefaultEncodingFromUtf8(lastWord);

            try
            {
                var result =
                    this.channel.predict(prefix, lastWord)
                        .Select(StringEditHelper.ConvertToUtf8FromDefaultEncoding)
                        .ToList();

                return new ObservableCollection<string>(result);
            }
            catch (Exception)
            {
                return new ObservableCollection<string>();
            }
        }

        /// <summary>
        ///     Initializes the presage WCF channel.
        /// </summary>
        private void InitializeChannel()
        {
            var binding = new NetNamedPipeBinding();
            var address = new EndpointAddress("net.pipe://localhost/PresageService/v1/presage");
            var channelFactory = new ChannelFactory<PresageChannel>(binding, address);
            this.channel = channelFactory.CreateChannel();
        }
    }
}