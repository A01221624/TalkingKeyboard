// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StringEditHelper.cs" company="Numeral">
//   Copyright 2016 Fernando Ramírez Garibay
// </copyright>
// <summary>
//   Defines the StringEditHelper type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace TalkingKeyboard.Infrastructure.Helpers
{
    using TalkingKeyboard.Infrastructure.Constants;

    public static class StringEditHelper
    {
        /// <summary>
        ///     Gets the last word.
        /// </summary>
        /// <param name="s">String from which to get the last word.</param>
        /// <returns>Last word from string.</returns>
        /// <remarks>The last word is anything found after the last <see cref="CharacterClasses.WordSeparators" /></remarks>
        public static string GetLastWord(string s)
        {
            string prefix, lastWord;
            SplitStringPrefixAndLastWord(s, out prefix, out lastWord);
            return lastWord;
        }

        /// <summary>
        ///     Returns a new instance of the original string where the last word is removed.
        /// </summary>
        /// <param name="s">The original string.</param>
        /// <returns>A new instance of the original string where the last word is removed.</returns>
        /// <remarks>The last word is anything found after the last <see cref="CharacterClasses.WordSeparators" /></remarks>
        public static string RemoveLastWord(string s)
        {
            string prefix, lastWord;
            SplitStringPrefixAndLastWord(s, out prefix, out lastWord);
            return prefix;
        }

        /// <summary>
        ///     Splits the string between the last word and everything before (prefix).
        /// </summary>
        /// <param name="s">The string</param>
        /// <param name="prefix">The retrieved prefix.</param>
        /// <param name="lastWord">The retrieved last word.</param>
        /// <remarks>
        ///     The last word is anything found after the last <see cref="CharacterClasses.WordSeparators" />.
        ///     The prefix is anything before the last word.
        /// </remarks>
        public static void SplitStringPrefixAndLastWord(string s, out string prefix, out string lastWord)
        {
            prefix = string.Empty;
            lastWord = string.Empty;
            if ((s == null) || s.Equals(string.Empty))
            {
                return;
            }

            var indexLastWord = s.LastIndexOfAny(CharacterClasses.WordSeparators) + 1;
            lastWord = s.Length > indexLastWord ? s.Substring(indexLastWord) : string.Empty;
            prefix = s.Length > indexLastWord ? s.Remove(indexLastWord) : s;
        }
    }
}