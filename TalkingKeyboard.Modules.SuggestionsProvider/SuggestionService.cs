// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SuggestionService.cs" company="Numeral">
//   Copyright 2016 Fernando Ramírez Garibay
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace TalkingKeyboard.Modules.SuggestionsProvider
{
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Windows.Input;

    using Prism.Commands;

    using TalkingKeyboard.Infrastructure;
    using TalkingKeyboard.Infrastructure.Constants;
    using TalkingKeyboard.Infrastructure.Controls;
    using TalkingKeyboard.Infrastructure.Helpers;
    using TalkingKeyboard.Infrastructure.ServiceInterfaces;
    using TalkingKeyboard.Modules.SuggestionsProvider.SuggestionSources;

    /// <summary>
    ///     Defines the SuggestionService class which provides suggestion services such as auto-complete.
    /// </summary>
    public class SuggestionService : ISuggestionService
    {
        private readonly MultikeySuggestionSource multikeySource = new MultikeySuggestionSource();
        private readonly PresageSuggestionSource presageSource = new PresageSuggestionSource();
        private readonly ITextModel textModel;

        /// <summary>
        ///     Initializes a new instance of the <see cref="SuggestionService" /> class.
        /// </summary>
        /// <param name="textModel">The text model (obtained through DI).</param>
        public SuggestionService(ITextModel textModel)
        {
            this.textModel = textModel;
            this.AddSuggestionCommand = new DelegateCommand<string>(this.AddSuggestion);
            Commands.AddSuggestionCommand.RegisterCommand(this.AddSuggestionCommand);
        }

        /// <summary>
        ///     Gets or sets the command for adding a suggestion.
        /// </summary>
        /// <value>
        ///     The command for adding a suggestion.
        /// </value>
        public ICommand AddSuggestionCommand { get; set; }

        /// <summary>
        ///     Adds a multi-character to the buffer.
        /// </summary>
        /// <param name="s">The multi-character to add.</param>
        /// <remarks>
        ///     The multi-character is a string with several characters, each of which will be considered as a potential
        ///     character for the current position in the text. These characters may be separated by any whitespace character found
        ///     in <see cref="CharacterClasses.Whitespace" />
        /// </remarks>
        public void AddMultiCharacterText(string s)
        {
            this.multikeySource.Add(s);
        }

        /// <summary>
        ///     Clears the multi character buffer.
        /// </summary>
        public void ClearMultiCharacterBuffer()
        {
            this.multikeySource.ClearMultiCharacterBuffer();
        }

        /// <summary>
        ///     Provides suggestions according to the multi-key buffer.
        /// </summary>
        /// <returns>
        ///     Returns an <see cref="T:System.Collections.ObjectModel.ObservableCollection`1" /> collection with disambiguation
        ///     suggestions first and then auto-complete suggestions.
        /// </returns>
        /// <remarks>
        ///     <para>
        ///         Disambiguation are those which match the number of keys selected and auto-complete are those which include keys
        ///         which have not been selected yet.
        ///     </para>
        ///     <para>Adds space and first-uppercases the suggestions if necessary.</para>
        /// </remarks>
        public ObservableCollection<string> ProvideMultiCharacterSuggestions()
        {
            var suggestions = this.multikeySource.GetSuggestions();
            return this.AddSpaceAndUppercaseIfNecessary(suggestions);
        }

        /// <summary>
        ///     Gets the suggestions based on the current text.
        /// </summary>
        /// <returns>
        ///     Returns an <see cref="T:System.Collections.ObjectModel.ObservableCollection`1" /> of strings containing the
        ///     suggestions.
        /// </returns>
        /// <remarks>
        ///     <para>Adds space and first-uppercases the suggestions if necessary.</para>
        /// </remarks>
        public ObservableCollection<string> ProvideSuggestions()
        {
            var suggestions = this.presageSource.GetSuggestions(this.textModel.CurrentText);
            return this.AddSpaceAndUppercaseIfNecessary(suggestions);
        }

        /// <summary>
        ///     Removes the last multi-character from the buffer.
        /// </summary>
        public void RemoveLastMultiCharacter()
        {
            this.multikeySource.RemoveLastMultiCharacter();
        }

        /// <summary>
        ///     Adds space and first-uppercase if necessary to each string in a suggestion collection (based on the current text).
        /// </summary>
        /// <param name="collection">The suggestion collection.</param>
        /// <returns>
        ///     A new collection where the strings are the same as in the original but with a space at the beginning if it is
        ///     needed and where the first character is uppercase if necessary.
        /// </returns>
        private ObservableCollection<string> AddSpaceAndUppercaseIfNecessary(ObservableCollection<string> collection)
        {
            var currentPrefix = StringEditHelper.RemoveLastWord(this.textModel.CurrentText);
            var trimmedPrefix = currentPrefix.Trim();
            var needsSpace = (currentPrefix.Length > 0)
                             && !CharacterClasses.Whitespace.Contains(currentPrefix[currentPrefix.Length - 1]);
            var needsUppercase = (this.textModel.CurrentText.Trim().Length == 0) || (trimmedPrefix.Length == 0)
                                 || CharacterClasses.FollowedByUppercase.Contains(
                                     trimmedPrefix[trimmedPrefix.Length - 1]);
            var result = collection.Select(
                s =>
                    {
                        if (needsUppercase)
                        {
                            s = s.Length > 1 ? char.ToUpper(s[0]) + s.Substring(1) : s.ToUpper();
                        }

                        if (needsSpace)
                        {
                            s = " " + s;
                        }

                        return s;
                    });
            return new ObservableCollection<string>(result);
        }

        /// <summary>
        ///     Adds a suggestion.
        /// </summary>
        /// <param name="s">The suggestion.</param>
        private void AddSuggestion(string s)
        {
            var currentText = this.textModel.CurrentText;
            var lastWord = StringEditHelper.GetLastWord(currentText);
            if (s.ToLower().StartsWith(lastWord.ToLower()))
            {
                Commands.RemoveLastWordCommand.Execute(null);
            }

            Commands.AppendTextCommand.Execute(s + " ");
        }
    }
}