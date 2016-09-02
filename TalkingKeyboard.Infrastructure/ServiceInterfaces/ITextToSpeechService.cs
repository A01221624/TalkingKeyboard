namespace TalkingKeyboard.Infrastructure.ServiceInterfaces
{
    public interface ITextToSpeechService
    {
        void Say(string s);
        void SayCurrentText();
    }
}