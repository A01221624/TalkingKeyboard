using System;
using System.Windows;
using Microsoft.Practices.Unity;
using Prism.Events;
using Prism.Modularity;
using Prism.Regions;
using TalkingKeyboard.Modules.CoordinateProvider.Services;

namespace TalkingKeyboard.Modules.CoordinateProvider
{
    public class CoordinateProviderModule : IModule
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly IUnityContainer _unityContainer;

        public CoordinateProviderModule(IEventAggregator eventAggregator, IUnityContainer unityContainer)
        {
            _eventAggregator = eventAggregator;
            _unityContainer = unityContainer;
        }

        public void Initialize()
        {
            _unityContainer.RegisterType<EyeXGazeCoordinateService>(new ContainerControlledLifetimeManager());
            _unityContainer.Resolve<EyeXGazeCoordinateService>();
        }
    }
}
