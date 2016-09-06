using System;

namespace TalkingKeyboard.Infrastructure.Helpers
{
    public static class StringEditHelper
    {
        public static void SplitStringPrefixAndLastWord(string s, out string prefix, out string lastWord)
        {
            prefix = String.Empty;
            lastWord = String.Empty;
            if (s == null || s.Equals(String.Empty)) return;
            var indexLastWord = s.LastIndexOfAny(Constants.CharacterClasses.WordSeparators) + 1;
            lastWord = s.Length > indexLastWord ? s.Substring(indexLastWord) : string.Empty;
            prefix = s.Length > indexLastWord ? s.Remove(indexLastWord) : s;
        }

        public static string GetLastWord(string s)
        {
            string prefix, lastWord;
            SplitStringPrefixAndLastWord(s, out prefix, out lastWord);
            return lastWord;
        }

        public static string RemoveLastWord(string s)
        {
            string prefix, lastWord;
            SplitStringPrefixAndLastWord(s, out prefix, out lastWord);
            return prefix;
        }
    }
}