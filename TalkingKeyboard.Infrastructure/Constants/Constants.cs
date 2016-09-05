using System.Collections.Generic;
using System.IO;

namespace TalkingKeyboard.Infrastructure.Constants
{
    public static class RegionNames
    {
        public static string TextViewRegion = "TextViewRegion";
        public static string BoardViewRegion = "BoardViewRegion";
        public static string SuggestionRegion = "SuggestionRegion";
    }

    public static class ResourceLocations
    {
        public static string SpanishDictionaryLocation = Path.GetFullPath(@"Resources\SpanishSpain.dic");
        public static string DefaultDictionaryLocation = SpanishDictionaryLocation;
    }
}