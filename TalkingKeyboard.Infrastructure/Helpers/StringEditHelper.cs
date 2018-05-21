// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StringEditHelper.cs" company="Numeral">
//   Copyright 2016 Fernando Ramírez Garibay
// </copyright>
// <summary>
//   Defines the StringEditHelper type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System.Collections;

namespace TalkingKeyboard.Infrastructure.Helpers
{
    using System.Text;

    using TalkingKeyboard.Infrastructure.Constants;

    /// <summary>
    /// This helper class contains static methods for editing the string and retrieving useful information (e.g. last word).
    /// </summary>
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

        /// <summary>
        /// Converts to default encoding from UTF8.
        /// </summary>
        /// <param name="s">The string for which to change the encoding.</param>
        /// <returns>
        /// The string encoded with the default encoding.
        /// </returns>
        public static string ConvertToDefaultEncodingFromUtf8(string s)
        {
            var bytes = Encoding.UTF8.GetBytes(s);
            return Encoding.Default.GetString(bytes);
        }

        /// <summary>
        /// Converts to UTF8 from default encoding.
        /// </summary>
        /// <param name="s">The string for which to change the encoding.</param>
        /// <returns>
        /// The string encoded with the UTF8 encoding.
        /// </returns>
        public static string ConvertToUtf8FromDefaultEncoding(string s)
        {
            var bytes = Encoding.Default.GetBytes(s);
            return Encoding.UTF8.GetString(bytes);
        }

        /// <summary>
        ///     Checks if the string ends with a whitespace.
        /// </summary>
        /// <param name="s">String to check if it ends with a whitespace.</param>
        /// <returns>True if the string ends with a whitespace.</returns>
        public static bool EndsWithWhitespace(string s)
        {
            return ((IList) CharacterClasses.Whitespace).Contains(s[s.Length - 1]);
        }
    }
}