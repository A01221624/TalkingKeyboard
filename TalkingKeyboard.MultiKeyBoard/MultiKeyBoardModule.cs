﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MultiKeyBoardModule.cs" company="Numeral">
//   Copyright 2016 Fernando Ramírez Garibay
// </copyright>
// <summary>
//   Defines the MultiKeyBoardModule type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace TalkingKeyboard.Modules.MultiKeyBoard
{
    using Microsoft.Practices.Unity;

    using Prism.Modularity;
    using Prism.Regions;
    using Prism.Unity;

    using TalkingKeyboard.Infrastructure.Constants;
    using TalkingKeyboard.Infrastructure.Models;
    using TalkingKeyboard.Modules.MultiKeyBoard.Model;
    using TalkingKeyboard.Modules.MultiKeyBoard.Views;

    public class MultiKeyBoardModule : IModule
    {
        private readonly IRegionManager regionManager;
        private readonly IUnityContainer unityContainer;

        /// <summary>
        ///     Initializes a new instance of the <see cref="MultiKeyBoardModule" /> class.
        /// </summary>
        /// <param name="regionManager">The prism region manager (obtained through DI).</param>
        /// <param name="unityContainer">The unity DI container (obtained through DI).</param>
        public MultiKeyBoardModule(IRegionManager regionManager, IUnityContainer unityContainer)
        {
            this.regionManager = regionManager;
            this.unityContainer = unityContainer;
        }

        /// <summary>
        ///     Registers a unique instance of the multi-key text model and  of each of the keyboard view-models in the dependency
        ///     injection container and registers their views with the region manager to the corresponding region of the window.
        /// </summary>
        public void Initialize()
        {
            this.unityContainer.RegisterType<IMultiKeyTextModel, MultikeyTextModel>(
                new ContainerControlledLifetimeManager());
            this.unityContainer.Resolve<IMultiKeyTextModel>();
            this.unityContainer.RegisterTypeForNavigation<QwertySpanishMultiKeyboard>(
                ViewNames.QwertySpanishMultiKeyboard);
            this.regionManager.RegisterViewWithRegion(
                RegionNames.BoardViewRegion,
                () => this.unityContainer.Resolve<QwertySpanishMultiKeyboard>());
        }
    }
}