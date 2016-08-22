using System;
using Microsoft.Practices.Unity;
using Prism.Modularity;
using Prism.Regions;
using TalkingKeyboard.Infrastructure.Constants;
using TalkingKeyboard.Modules.SingleKeyBoard.Views;

namespace TalkingKeyboard.Modules.SingleKeyBoard
{
    public class SingleKeyBoardModule : IModule
    {
        private readonly IRegionManager _regionManager;
        private readonly IUnityContainer _unityContainer;

        public SingleKeyBoardModule(RegionManager regionManager, UnityContainer unityContainer)
        {
            _regionManager = regionManager;
            _unityContainer = unityContainer;
        }

        public void Initialize()
        {
            _regionManager.RegisterViewWithRegion(RegionNames.BoardViewRegion, () => _unityContainer.Resolve<QwertySpanishSingleKeyboard>());
        }
    }
}