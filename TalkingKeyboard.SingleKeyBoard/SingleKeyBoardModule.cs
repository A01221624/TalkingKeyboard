// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SingleKeyBoardModule.cs" company="Numeral">
//   Copyright 2016 Fernando Ramírez Garibay
// </copyright>
// <summary>
//   Defines the SingleKeyBoardModule type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace TalkingKeyboard.Modules.SingleKeyBoard
{
    using Microsoft.Practices.Unity;

    using Prism.Modularity;
    using Prism.Regions;
    using Prism.Unity;

    using TalkingKeyboard.Infrastructure.Constants;
    using TalkingKeyboard.Modules.SingleKeyBoard.Views;

    public class SingleKeyBoardModule : IModule
    {
        private readonly IRegionManager regionManager;
        private readonly IUnityContainer unityContainer;

        /// <summary>
        ///     Initializes a new instance of the <see cref="SingleKeyBoardModule" /> class.
        /// </summary>
        /// <param name="regionManager">The prism region manager (obtained through DI).</param>
        /// <param name="unityContainer">The unity DI container (obtained through DI).</param>
        public SingleKeyBoardModule(IRegionManager regionManager, IUnityContainer unityContainer)
        {
            this.regionManager = regionManager;
            this.unityContainer = unityContainer;
        }

        /// <summary>
        ///     Registers a unique instance of each of the keyboard view-models in the dependency injection container and registers
        ///     their views with the region manager to the corresponding region of the window.
        /// </summary>
        public void Initialize()
        {
            this.unityContainer.RegisterTypeForNavigation<QwertySpanishSingleKeyboard>(
                ViewNames.QwertySpanishSingleKeyboard);
            this.regionManager.RegisterViewWithRegion(
                RegionNames.BoardViewRegion,
                () => this.unityContainer.Resolve<QwertySpanishSingleKeyboard>());
        }
    }
}