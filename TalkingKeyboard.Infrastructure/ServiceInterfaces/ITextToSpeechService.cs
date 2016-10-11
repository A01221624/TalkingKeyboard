// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ITextToSpeechService.cs" company="Numeral">
//   Copyright 2016 Fernando Ramírez Garibay
// </copyright>
// <summary>
//   Defines the ITextToSpeechService interface.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace TalkingKeyboard.Infrastructure.ServiceInterfaces
{
    /// <summary>
    ///     Interface for text-to-speech services.
    /// </summary>
    public interface ITextToSpeechService
    {
        /// <summary>
        ///     Speaks the specified string.
        /// </summary>
        /// <param name="s">The string to speech-synthesize</param>
        void Say(string s);

        /// <summary>
        ///     Speaks the current text.
        /// </summary>
        void SayCurrentText();
    }
}