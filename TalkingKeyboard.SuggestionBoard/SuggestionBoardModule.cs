// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SuggestionBoardModule.cs" company="Numeral">
//   Copyright 2016 Fernando Ramírez Garibay
// </copyright>
// <summary>
//   Defines the SuggestionBoardModule type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace TalkingKeyboard.Modules.SuggestionBoard
{
    using Microsoft.Practices.Unity;

    using Prism.Modularity;
    using Prism.Regions;

    using TalkingKeyboard.Infrastructure.Constants;
    using TalkingKeyboard.Infrastructure.Controls;
    using TalkingKeyboard.Modules.SuggestionBoard.ViewModels;
    using TalkingKeyboard.Modules.SuggestionBoard.Views;

    public class SuggestionBoardModule : IModule
    {
        private readonly IRegionManager regionManager;
        private readonly IUnityContainer unityContainer;

        /// <summary>
        ///     Initializes a new instance of the <see cref="SuggestionBoardModule" /> class.
        /// </summary>
        /// <param name="regionManager">The prism region manager (obtained through DI).</param>
        /// <param name="unityContainer">The unity DI container (obtained through DI).</param>
        public SuggestionBoardModule(IRegionManager regionManager, IUnityContainer unityContainer)
        {
            this.regionManager = regionManager;
            this.unityContainer = unityContainer;
        }

        /// <summary>
        ///     Registers a unique instance of the suggestions view-model in the dependency injection container and registers the
        ///     suggestion board view with the region manager to the corresponding region of the window.
        /// </summary>
        public void Initialize()
        {
            this.unityContainer.RegisterType<ISuggestionsViewModel, FourSuggestionsBoardViewModel>(
                new ContainerControlledLifetimeManager());
            this.unityContainer.Resolve<ISuggestionsViewModel>();
            this.regionManager.RegisterViewWithRegion(
                RegionNames.SuggestionRegion,
                () => this.unityContainer.Resolve<FourSuggestionsBoard>());
        }
    }
}