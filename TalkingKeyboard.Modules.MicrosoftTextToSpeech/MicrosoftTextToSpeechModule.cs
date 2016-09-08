// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MicrosoftTextToSpeechModule.cs" company="Numeral">
//   Copyright 2016 Fernando Ramírez Garibay
// </copyright>
// <summary>
//   Defines the MicrosoftTextToSpeechModule type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace TalkingKeyboard.Modules.MicrosoftTextToSpeech
{
    using Microsoft.Practices.Unity;

    using Prism.Modularity;

    using TalkingKeyboard.Infrastructure.ServiceInterfaces;

    public class MicrosoftTextToSpeechModule : IModule
    {
        private readonly IUnityContainer _unityContainer;

        /// <summary>
        ///     Initializes a new instance of the <see cref="MicrosoftTextToSpeechModule" /> class.
        /// </summary>
        /// <param name="unityContainer">The unity dependency injection container (obtained through DI).</param>
        public MicrosoftTextToSpeechModule(IUnityContainer unityContainer)
        {
            this._unityContainer = unityContainer;
        }

        /// <summary>
        ///     Notifies the module that it has been initialized.
        /// </summary>
        public void Initialize()
        {
            this._unityContainer.RegisterType<ITextToSpeechService, TextToSpeechService>(
                new ContainerControlledLifetimeManager());
            this._unityContainer.Resolve<ITextToSpeechService>();
        }
    }
}