using System;
using Microsoft.Practices.Unity;
using Prism.Modularity;
using Prism.Regions;
using TalkingKeyboard.Infrastructure.Constants;
using TalkingKeyboard.Modules.CentralTextModule.Views;

namespace TalkingKeyboard.Modules.CentralTextModule
{
    public class TextHolderModule : IModule
    {
        private readonly IRegionManager _regionManager;
        private readonly IUnityContainer _unityContainer;

        public TextHolderModule(IRegionManager regionManager, IUnityContainer unityContainer)
        {
            _regionManager = regionManager;
            _unityContainer = unityContainer;
        }

        public void Initialize()
        {
            _regionManager.RegisterViewWithRegion(RegionNames.TextViewRegion, () => _unityContainer.Resolve<TextView>());
        }
    }
}