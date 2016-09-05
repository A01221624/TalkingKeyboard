using Prism.Commands;

namespace TalkingKeyboard.Infrastructure
{
    public static class Commands
    {
        public static CompositeCommand SetTextCommand = new CompositeCommand();
        public static CompositeCommand SetShiftDownCommand = new CompositeCommand();
        public static CompositeCommand SpeechSynthesisCommand = new CompositeCommand();
        public static CompositeCommand ToggleShiftDownCommand = new CompositeCommand();
        public static CompositeCommand RemoveLastCharacterCommand = new CompositeCommand();
        public static CompositeCommand RemoveLastWordCommand = new CompositeCommand();
        public static CompositeCommand AddSuggestionCommand = new CompositeCommand();
        public static CompositeCommand AddMultikeyTextCommand = new CompositeCommand();
        public static CompositeCommand RemoveLastMultiCharacterCommand = new CompositeCommand();
    }
}