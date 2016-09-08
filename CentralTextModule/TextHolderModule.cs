using Microsoft.Practices.Unity;
using Prism.Modularity;
using Prism.Regions;

using TalkingKeyboard.Infrastructure.Controls;
using TalkingKeyboard.Modules.CentralTextModule.ViewModels;
using TalkingKeyboard.Modules.CentralTextModule.Views;

namespace TalkingKeyboard.Modules.CentralTextModule
{
    using TalkingKeyboard.Infrastructure.Constants;

    public class TextHolderModule : IModule
    {
        private readonly IRegionManager _regionManager;
        private readonly IUnityContainer _unityContainer;

        /// <summary>
        /// Initializes a new instance of the <see cref="TextHolderModule"/> class.
        /// </summary>
        /// <param name="regionManager">The prism region manager (obtained through DI).</param>
        /// <param name="unityContainer">The unity DI container (obtained through DI).</param>
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