using System;
using Microsoft.Practices.Unity;
using Prism.Modularity;
using Prism.Regions;
using Prism.Unity;
using TalkingKeyboard.Infrastructure.Constants;
using TalkingKeyboard.Modules.SingleKeyBoard.Views;

namespace TalkingKeyboard.Modules.SingleKeyBoard
{
    public class SingleKeyBoardModule : IModule
    {
        private readonly IRegionManager _regionManager;
        private readonly IUnityContainer _unityContainer;

        public SingleKeyBoardModule(IRegionManager regionManager, IUnityContainer unityContainer)
        {
            _regionManager = regionManager;
            _unityContainer = unityContainer;
        }

        public void Initialize()
        {
            _unityContainer.RegisterTypeForNavigation<QwertySpanishSingleKeyboard>(
                Infrastructure.Constants.ViewNames.QwertySpanishSingleKeyboard);
            _regionManager.RegisterViewWithRegion(RegionNames.BoardViewRegion, () => _unityContainer.Resolve<QwertySpanishSingleKeyboard>());
        }
    }
}