namespace TalkingKeyboard.Infrastructure.DataContainers
{
    public interface IMaintainable
    {
        /// <summary>
        ///     Performs any regular maintenance tasks such as
        ///     clean-up of old elements.
        /// </summary>
        void Maintain();
    }
}