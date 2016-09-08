// --------------------------------------------------------------------------------------------------------------------
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
        private readonly IRegionManager _regionManager;
        private readonly IUnityContainer _unityContainer;

        /// <summary>
        ///     Initializes a new instance of the <see cref="MultiKeyBoardModule" /> class.
        /// </summary>
        /// <param name="regionManager">The prism region manager (obtained through DI).</param>
        /// <param name="unityContainer">The unity DI container (obtained through DI).</param>
        public MultiKeyBoardModule(IRegionManager regionManager, IUnityContainer unityContainer)
        {
            this._regionManager = regionManager;
            this._unityContainer = unityContainer;
        }

        /// <summary>
        ///     Notifies the module that it has be initialized.
        /// </summary>
        public void Initialize()
        {
            this._unityContainer.RegisterType<IMultiKeyTextModel, MultikeyTextModel>(
                new ContainerControlledLifetimeManager());
            this._unityContainer.Resolve<IMultiKeyTextModel>();
            this._unityContainer.RegisterTypeForNavigation<QwertySpanishMultiKeyboard>(
                ViewNames.QwertySpanishMultiKeyboard);
            this._regionManager.RegisterViewWithRegion(
                RegionNames.BoardViewRegion,
                () => this._unityContainer.Resolve<QwertySpanishMultiKeyboard>());
        }
    }
}