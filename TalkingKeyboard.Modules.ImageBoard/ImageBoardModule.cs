namespace TalkingKeyboard.Modules.ImageBoard
{
    using System;

    using Microsoft.Practices.Unity;

    using Prism.Modularity;
    using Prism.Regions;
    using Prism.Unity;

    using TalkingKeyboard.Infrastructure.Constants;
    using TalkingKeyboard.Infrastructure.Models;
    using TalkingKeyboard.Modules.ImageBoard.ViewModels;
    using TalkingKeyboard.Modules.ImageBoard.Views;

    public class ImageBoardModule : IModule
    {
        private readonly IRegionManager regionManager;
        private readonly IUnityContainer unityContainer;

        /// <summary>
        ///     Initializes a new instance of the <see cref="ImageBoardModule" /> class.
        /// </summary>
        /// <param name="regionManager">The prism region manager (obtained through DI).</param>
        /// <param name="unityContainer">The unity DI container (obtained through DI).</param>
        public ImageBoardModule(IRegionManager regionManager, IUnityContainer unityContainer)
        {
            this.regionManager = regionManager;
            this.unityContainer = unityContainer;
        }

        public void Initialize()
        {
            this.unityContainer.RegisterTypeForNavigation<ImageBoard>(
                ViewNames.SimpleImageBoard);
            this.regionManager.RegisterViewWithRegion(
                RegionNames.BoardViewRegion,
                () => this.unityContainer.Resolve<ImageBoard>());
        }
    }
}