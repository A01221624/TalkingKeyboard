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
        public static char[] FollowedByUppercase = {'.', '?', '!'};
        public static char[] Whitespace = {' ', '\r', '\n', '\t'};
        public static char[] ColonsAndCommas = {'.', ';', ',', ':'};
        public static char[] ClosingCharacters = {'?', '!', ')', ']', '}', '>'};

        public static char[] PreceededByNonwhitespaceFollowedByWhitespace =
            ColonsAndCommas.ToList().Concat(ClosingCharacters.ToList()).ToArray();

        public static char[] WordSeparators =
            Whitespace.ToList().Concat(PreceededByNonwhitespaceFollowedByWhitespace.ToList()).ToArray();
    }
}