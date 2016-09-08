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
        private readonly IRegionManager _regionManager;
        private readonly IUnityContainer _unityContainer;

        /// <summary>
        ///     Initializes a new instance of the <see cref="SuggestionBoardModule" /> class.
        /// </summary>
        /// <param name="regionManager">The prism region manager (obtained through DI).</param>
        /// <param name="unityContainer">The unity DI container (obtained through DI).</param>
        public SuggestionBoardModule(IRegionManager regionManager, IUnityContainer unityContainer)
        {
            this._regionManager = regionManager;
            this._unityContainer = unityContainer;
        }

        /// <summary>
        ///     Notifies the module that it has been initialized.
        /// </summary>
        public void Initialize()
        {
            this._unityContainer.RegisterType<ISuggestionsViewModel, FourSuggestionsBoardViewModel>(
                new ContainerControlledLifetimeManager());
            this._unityContainer.Resolve<ISuggestionsViewModel>();
            this._regionManager.RegisterViewWithRegion(
                RegionNames.SuggestionRegion,
                () => this._unityContainer.Resolve<FourSuggestionsBoard>());
        }
    }
}