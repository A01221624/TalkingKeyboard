using Microsoft.Practices.Unity;
using Prism.Modularity;
using Prism.Regions;
using TalkingKeyboard.Infrastructure.Constants;
using TalkingKeyboard.Infrastructure.Controls;
using TalkingKeyboard.Modules.CentralTextModule.ViewModels;
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
            _unityContainer.RegisterType<ITextModel, TextViewModel>(new ContainerControlledLifetimeManager());
            _regionManager.RegisterViewWithRegion(RegionNames.TextViewRegion, () => _unityContainer.Resolve<TextView>());
        }
    }
}