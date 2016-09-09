// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TextHolderModule.cs" company="Numeral">
//   Copyright 2016 Fernando Ramírez Garibay
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace TalkingKeyboard.Modules.CentralTextModule
{
    using Microsoft.Practices.Unity;

    using Prism.Modularity;
    using Prism.Regions;

    using TalkingKeyboard.Infrastructure.Constants;
    using TalkingKeyboard.Infrastructure.Controls;
    using TalkingKeyboard.Modules.CentralTextModule.ViewModels;
    using TalkingKeyboard.Modules.CentralTextModule.Views;

    /// <summary>
    ///     Class for initializing the module for holding, displaying and handling changes (through commands) to the current
    ///     text.
    /// </summary>
    /// <seealso cref="Prism.Modularity.IModule" />
    public class TextHolderModule : IModule
    {
        private readonly IRegionManager regionManager;
        private readonly IUnityContainer unityContainer;

        /// <summary>
        ///     Initializes a new instance of the <see cref="TextHolderModule" /> class.
        /// </summary>
        /// <param name="regionManager">The prism region manager (obtained through DI).</param>
        /// <param name="unityContainer">The unity DI container (obtained through DI).</param>
        public TextHolderModule(IRegionManager regionManager, IUnityContainer unityContainer)
        {
            this.regionManager = regionManager;
            this.unityContainer = unityContainer;
        }

        /// <summary>
        ///     Registers a unique instance of the text view-model in the dependency injection container and registers the text
        ///     view with the region manager to the corresponding region of the window.
        /// </summary>
        public void Initialize()
        {
            this.unityContainer.RegisterType<ITextModel, TextViewModel>(new ContainerControlledLifetimeManager());
            this.regionManager.RegisterViewWithRegion(
                RegionNames.TextViewRegion,
                () => this.unityContainer.Resolve<TextView>());
        }
    }
}