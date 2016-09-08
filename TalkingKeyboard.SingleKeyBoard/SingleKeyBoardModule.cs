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
        private readonly IRegionManager _regionManager;
        private readonly IUnityContainer _unityContainer;

        /// <summary>
        ///     Initializes a new instance of the <see cref="SingleKeyBoardModule" /> class.
        /// </summary>
        /// <param name="regionManager">The prism region manager (obtained through DI).</param>
        /// <param name="unityContainer">The unity DI container (obtained through DI).</param>
        public SingleKeyBoardModule(IRegionManager regionManager, IUnityContainer unityContainer)
        {
            this._regionManager = regionManager;
            this._unityContainer = unityContainer;
        }

        /// <summary>
        ///     Notifies the module that it has been initialized.
        /// </summary>
        public void Initialize()
        {
            this._unityContainer.RegisterTypeForNavigation<QwertySpanishSingleKeyboard>(
                ViewNames.QwertySpanishSingleKeyboard);
            this._regionManager.RegisterViewWithRegion(
                RegionNames.BoardViewRegion,
                () => this._unityContainer.Resolve<QwertySpanishSingleKeyboard>());
        }
    }
}