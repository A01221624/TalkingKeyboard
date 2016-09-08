// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CoordinateProviderModule.cs" company="Numeral">
//   Copyright 2016 Fernando Ramírez Garibay
// </copyright>
// <summary>
//   Defines the CoordinateProviderModule type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace TalkingKeyboard.Modules.CoordinateProvider
{
    using Microsoft.Practices.Unity;

    using Prism.Events;
    using Prism.Modularity;

    using TalkingKeyboard.Modules.CoordinateProvider.Services;

    public class CoordinateProviderModule : IModule
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly IUnityContainer _unityContainer;

        /// <summary>
        ///     Initializes a new instance of the <see cref="CoordinateProviderModule" /> class.
        /// </summary>
        /// <param name="eventAggregator">Provides pub/sub events (obtained through DI).</param>
        /// <param name="unityContainer">The unity dependency injection container (obtained through DI).</param>
        public CoordinateProviderModule(IEventAggregator eventAggregator, IUnityContainer unityContainer)
        {
            this._eventAggregator = eventAggregator;
            this._unityContainer = unityContainer;
        }

        /// <summary>
        ///     Notifies the module that it has been initialized.
        /// </summary>
        public void Initialize()
        {
            this._unityContainer.RegisterType<EyeXGazeCoordinateService>(new ContainerControlledLifetimeManager());
            this._unityContainer.Resolve<EyeXGazeCoordinateService>();
        }
    }
}