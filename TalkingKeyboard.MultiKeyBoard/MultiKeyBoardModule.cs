using Microsoft.Practices.Unity;
using Prism.Modularity;
using Prism.Regions;
using Prism.Unity;

using TalkingKeyboard.Infrastructure.Models;
using TalkingKeyboard.Modules.MultiKeyBoard.Model;
using TalkingKeyboard.Modules.MultiKeyBoard.Views;

namespace TalkingKeyboard.Modules.MultiKeyBoard
{
    using TalkingKeyboard.Infrastructure.Constants;

    public class MultiKeyBoardModule : IModule
    {
        private readonly IUnityContainer _unityContainer;
        private readonly IRegionManager _regionManager;

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
                ViewNames.QwertySpanishMultiKeyboard);
            _regionManager.RegisterViewWithRegion(RegionNames.BoardViewRegion,
                () => _unityContainer.Resolve<QwertySpanishMultiKeyboard>());
        }
    }
}