using Microsoft.Practices.Unity;
using Prism.Modularity;
using Prism.Regions;

using TalkingKeyboard.Infrastructure.Controls;
using TalkingKeyboard.Modules.SuggestionBoard.ViewModels;
using TalkingKeyboard.Modules.SuggestionBoard.Views;

namespace TalkingKeyboard.Modules.SuggestionBoard
{
    using TalkingKeyboard.Infrastructure.Constants;

    public class SuggestionBoardModule : IModule
    {
        private readonly IRegionManager _regionManager;
        private readonly IUnityContainer _unityContainer;

        public SuggestionBoardModule(IRegionManager regionManager, IUnityContainer unityContainer)
        {
            _regionManager = regionManager;
            _unityContainer = unityContainer;
        }

        public void Initialize()
        {
            _unityContainer.RegisterType<ISuggestionsViewModel, FourSuggestionsBoardViewModel>(
                new ContainerControlledLifetimeManager());
            _unityContainer.Resolve<ISuggestionsViewModel>();
            _regionManager.RegisterViewWithRegion(RegionNames.SuggestionRegion,
                () => _unityContainer.Resolve<FourSuggestionsBoard>());
        }
    }
}