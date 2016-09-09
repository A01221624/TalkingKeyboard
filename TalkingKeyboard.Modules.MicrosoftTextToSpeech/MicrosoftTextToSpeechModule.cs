// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MicrosoftTextToSpeechModule.cs" company="Numeral">
//   Copyright 2016 Fernando Ramírez Garibay
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace TalkingKeyboard.Modules.MicrosoftTextToSpeech
{
    using Microsoft.Practices.Unity;

    using Prism.Modularity;

    using TalkingKeyboard.Infrastructure.ServiceInterfaces;

    /// <summary>
    ///     Class for initializing the module for providing text to speech services, particularly obtained by the Microsoft TTS
    ///     (which uses the voices installed for Windows).
    /// </summary>
    /// <seealso cref="Prism.Modularity.IModule" />
    public class MicrosoftTextToSpeechModule : IModule
    {
        private readonly IUnityContainer unityContainer;

        /// <summary>
        ///     Initializes a new instance of the <see cref="MicrosoftTextToSpeechModule" /> class.
        /// </summary>
        /// <param name="unityContainer">The unity dependency injection container (obtained through DI).</param>
        public MicrosoftTextToSpeechModule(IUnityContainer unityContainer)
        {
            this.unityContainer = unityContainer;
        }

        /// <summary>
        ///     Registers a unique instance of the text to speech service in the dependency injection container.
        /// </summary>
        public void Initialize()
        {
            this.unityContainer.RegisterType<ITextToSpeechService, TextToSpeechService>(
                new ContainerControlledLifetimeManager());
            this.unityContainer.Resolve<ITextToSpeechService>();
        }
    }
}