using System;
using Microsoft.Practices.Unity;
using Prism.Modularity;
using Prism.Regions;
using Prism.Unity;
using TalkingKeyboard.Infrastructure.Constants;
using TalkingKeyboard.Infrastructure.Models;
using TalkingKeyboard.Modules.MultiKeyBoard.Model;
using TalkingKeyboard.Modules.MultiKeyBoard.Views;

namespace TalkingKeyboard.Modules.MultiKeyBoard
{
    public class MultiKeyBoardModule : IModule
    {
        IRegionManager _regionManager;
        private readonly IUnityContainer _unityContainer;

        public MultiKeyBoardModule(IRegionManager regionManager, IUnityContainer unityContainer)
        {
            _regionManager = regionManager;
            _unityContainer = unityContainer;
        }

        public void Initialize()
        {
            _unityContainer.RegisterType<IMultiKeyTextModel, MultikeyTextModel>(new ContainerControlledLifetimeManager());
            _unityContainer.Resolve<IMultiKeyTextModel>();
            _unityContainer.RegisterTypeForNavigation<QwertySpanishMultiKeyboard>(
                Infrastructure.Constants.ViewNames.QwertySpanishMultiKeyboard);
            _regionManager.RegisterViewWithRegion(RegionNames.BoardViewRegion, () => _unityContainer.Resolve<QwertySpanishMultiKeyboard>());
        }
    }
}