using Prism.Commands;

namespace TalkingKeyboard.Infrastructure
{
    public static class Commands
    {
        public static CompositeCommand SetTextCommand = new CompositeCommand();
        public static CompositeCommand SetShiftDownCommand = new CompositeCommand();
        public static CompositeCommand ToggleShiftDownCommand = new CompositeCommand();
        public static CompositeCommand RemoveLastCharacterCommand = new CompositeCommand();
    }
}