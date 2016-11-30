// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MultiKeyBoardModule.cs" company="Numeral">
//   Copyright 2016 Fernando Ramírez Garibay
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using TalkingKeyboard.Modules.MultiKeyBoard.ViewModels;

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

    /// <summary>
    ///     Class for initializing the module for displaying the multi-character keyboards and handling their input.
    /// </summary>
    /// <seealso cref="Prism.Modularity.IModule" />
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
            /* Model */
            this.unityContainer.RegisterType<IMultiKeyTextModel, MultikeyTextModel>(
                new ContainerControlledLifetimeManager());
            this.unityContainer.Resolve<IMultiKeyTextModel>();

            /* View-model */
            this.unityContainer.RegisterType<IMultiKeyboardViewModel, MultiKeyboardViewModel>(
                new ContainerControlledLifetimeManager());
            this.unityContainer.Resolve<IMultiKeyboardViewModel>();

            /* Views */
            this.unityContainer.RegisterTypeForNavigation<T9SpanishMultiKeyboard>(
                ViewNames.T9SpanishMultiKeyboard);
            this.unityContainer.RegisterTypeForNavigation<QwertySpanishMultiKeyboard>(
                ViewNames.QwertySpanishMultiKeyboard);
            this.regionManager.RegisterViewWithRegion(
                RegionNames.BoardViewRegion,
                () => this.unityContainer.Resolve<T9SpanishMultiKeyboard>());
        }
    }
}