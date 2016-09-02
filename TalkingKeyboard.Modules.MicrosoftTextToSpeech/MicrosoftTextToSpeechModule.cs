﻿using Prism.Modularity;
using Prism.Regions;
using System;
using Microsoft.Practices.Unity;
using TalkingKeyboard.Infrastructure.ServiceInterfaces;

namespace TalkingKeyboard.Modules.MicrosoftTextToSpeech
{
    public class MicrosoftTextToSpeechModule : IModule
    {
        private readonly IUnityContainer _unityContainer;

        public MicrosoftTextToSpeechModule(IUnityContainer unityContainer)
        {
            _unityContainer = unityContainer;
        }

        public void Initialize()
        {
            _unityContainer.RegisterType<ITextToSpeechService, TextToSpeechService>(
                new ContainerControlledLifetimeManager());
            _unityContainer.Resolve<ITextToSpeechService>();
        }
    }
}