using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace TalkingKeyboard.Infrastructure.Constants
{
    public static class RegionNames
    {
        public static string TextViewRegion = "TextViewRegion";
        public static string BoardViewRegion = "BoardViewRegion";
        public static string SuggestionRegion = "SuggestionRegion";
    }

    public static class ViewNames
    {
        public static string QwertySpanishMultiKeyboard = "QwertySpanishMultiKeyboard";
        public static string QwertySpanishSingleKeyboard = "QwertySpanishSingleKeyboard";
    }

    public static class ResourceLocations
    {
        public static string SpanishDictionaryLocation = Path.GetFullPath(@"Resources\SpanishSpain.dic");
        public static string DefaultDictionaryLocation = SpanishDictionaryLocation;
    }

    public static class CharacterClasses
    {
        public static char[] FollowedByUppercase = new[] {'.', '?', '!'};
        public static char[] Whitespace = new[] {' ', '\r', '\n', '\t'};
        public static char[] ColonsAndCommas = new[] {'.', ';', ',', ':'};
        public static char[] ClosingCharacters = new []{'?', '!', ')', ']', '}', '>'};

        public static char[] PreceededByNonwhitespaceFollowedByWhitespace =
            ColonsAndCommas.ToList().Concat(ClosingCharacters.ToList()).ToArray();
        public static char[] WordSeparators =
            Whitespace.ToList().Concat(PreceededByNonwhitespaceFollowedByWhitespace.ToList()).ToArray();
    }
}