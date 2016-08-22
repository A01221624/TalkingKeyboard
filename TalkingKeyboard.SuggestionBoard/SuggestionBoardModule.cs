using System;
using Microsoft.Practices.Unity;
using Prism.Modularity;
using Prism.Regions;
using TalkingKeyboard.Infrastructure.Constants;
using TalkingKeyboard.Modules.SuggestionBoard.Views;

namespace TalkingKeyboard.Modules.SuggestionBoard
{
    public class SuggestionBoardModule : IModule
    {
        private readonly IRegionManager _regionManager;
        private readonly IUnityContainer _unityContainer;

        public SuggestionBoardModule(RegionManager regionManager, UnityContainer unityContainer)
        {
            _regionManager = regionManager;
            _unityContainer = unityContainer;
        }

        public void Initialize()
        {
            _regionManager.RegisterViewWithRegion(RegionNames.SuggestionRegion, () => _unityContainer.Resolve<FourSuggestionsBoard>());
        }
    }
}