using Prism.Modularity;
using Prism.Regions;
using System;
using Microsoft.Practices.Unity;
using TalkingKeyboard.Infrastructure.ServiceInterfaces;

namespace TalkingKeyboard.Modules.ByGazeTimePointProcessor
{
    public class ByGazeTimePointProcessorModule : IModule
    {
        private readonly IUnityContainer _unityContainer;

        public ByGazeTimePointProcessorModule(IUnityContainer unityContainer)
        {
            _unityContainer = unityContainer;
        }

        public void Initialize()
        {
            _unityContainer.RegisterType<IControlActivationService, GazeSelectionService>(new ContainerControlledLifetimeManager());
            _unityContainer.Resolve<IControlActivationService>();
        }
    }
}