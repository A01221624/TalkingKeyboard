// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CharacterClasses.cs" company="Numeral">
//   Copyright 2016 Fernando Ramírez Garibay
// </copyright>
// <summary>
//   Defines the CharacterClasses type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace TalkingKeyboard.Infrastructure.Constants
{
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// The character classes.
    /// </summary>
    public static class CharacterClasses
    {
        /// <summary>
        /// Characters followed by uppercase.
        /// </summary>
        public static char[] FollowedByUppercase => 
            new[]
                {
                    '.', '?', '!'
                };

        /// <summary>
        /// Whitespace characters.
        /// </summary>
        public static char[] Whitespace => 
            new[]
                {
                    ' ', '\r', '\n', '\t'
                };

        /// <summary>
        /// Colons and commas characters.
        /// </summary>
        public static char[] ColonsAndCommas => 
            new[]
                {
                    '.', ';', ',', ':'
                };

        /// <summary>
        /// Closing characters.
        /// </summary>
        public static char[] ClosingCharacters => 
            new[]
                {
                    '?', '!', ')', ']', '}', '>'
                };

        /// <summary>
        /// Accent characters.
        /// </summary>
        public static char[] Accents => 
            new[]
                {
                    '´', '¨'
                };

        /// <summary>
        /// The characters preceded by non-whitespace and followed by whitespace (e.g. ',').
        /// </summary>
        public static char[] PrecededByNonwhitespaceFollowedByWhitespace =>
            ColonsAndCommas.ToList().Concat(ClosingCharacters.ToList()).ToArray();

        /// <summary>
        /// Word separating characters.
        /// </summary>
        public static char[] WordSeparators =>
            Whitespace.ToList().Concat(PrecededByNonwhitespaceFollowedByWhitespace.ToList()).ToArray();

        /// <summary>
        /// Dictionary for joining characters with accents.
        /// </summary>
        public static Dictionary<char, Dictionary<char, char>> AccentLookup
            => new Dictionary<char, Dictionary<char, char>>
                   {
                           { 'a', new Dictionary<char, char> { { '´', 'á' } } },
                           { 'e', new Dictionary<char, char> { { '´', 'é' } } },
                           { 'i', new Dictionary<char, char> { { '´', 'í' } } },
                           { 'o', new Dictionary<char, char> { { '´', 'ó' } } },
                           { 'u', new Dictionary<char, char> { { '´', 'ú' }, { '¨', 'ü' } } },
                           { 'A', new Dictionary<char, char> { { '´', 'Á' } } },
                           { 'E', new Dictionary<char, char> { { '´', 'É' } } },
                           { 'I', new Dictionary<char, char> { { '´', 'Í' } } },
                           { 'O', new Dictionary<char, char> { { '´', 'Ó' } } },
                           { 'U', new Dictionary<char, char> { { '´', 'Ú' }, { '¨', 'Ü' } } }
                   };
    }
}