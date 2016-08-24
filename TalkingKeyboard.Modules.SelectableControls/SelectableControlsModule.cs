using Prism.Modularity;
using Prism.Regions;
using System;
using Microsoft.Practices.Unity;
using TalkingKeyboard.Modules.SelectableControls.Views;

namespace TalkingKeyboard.Modules.SelectableControls
{
    public class SelectableControlsModule : IModule
    {
        IRegionManager _regionManager;
        private IUnityContainer _unityContainer;

        public SelectableControlsModule(IRegionManager regionManager, IUnityContainer unityContainer)
        {
            _regionManager = regionManager;
            _unityContainer = unityContainer;
        }

        public void Initialize()
        {
        }
    }
}