namespace TalkingKeyboard.Infrastructure.Helpers
{
    public static class StringEditHelper
    {
        public static void SplitStringPrefixAndLastWord(string s, out string prefix, out string lastWord)
        {
            var previousWords = s.Split(' ');
            lastWord = previousWords.Length > 0 ? previousWords[previousWords.Length - 1] : string.Empty;
            prefix = previousWords.Length > 1
                ? string.Join(" ", previousWords, 0, previousWords.Length - 1)
                : string.Empty;
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