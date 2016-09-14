// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SuggestionBoardModule.cs" company="Numeral">
//   Copyright 2016 Fernando Ramírez Garibay
// </copyright>
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

    /// <summary>
    ///     Class for initializing the module for displaying suggestions as SelectableControls.
    /// </summary>
    /// <seealso cref="Prism.Modularity.IModule" />
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
        ///     Registers the suggestion board view with the region manager to the corresponding region of the window.
        /// </summary>
        public void Initialize()
        {
            this.regionManager.RegisterViewWithRegion(
                RegionNames.SuggestionRegion,
                () => this.unityContainer.Resolve<FourSuggestionsBoard>());
        }
    }
}