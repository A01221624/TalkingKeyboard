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

    using Prism.Modularity;

    using TalkingKeyboard.Modules.CoordinateProvider.Services;

    public class CoordinateProviderModule : IModule
    {
        private readonly IUnityContainer unityContainer;

        /// <summary>
        ///     Initializes a new instance of the <see cref="CoordinateProviderModule" /> class.
        /// </summary>
        /// <param name="unityContainer">The unity dependency injection container (obtained through DI).</param>
        public CoordinateProviderModule(IUnityContainer unityContainer)
        {
            this.unityContainer = unityContainer;
        }

        /// <summary>
        ///     Registers a unique instance of each of the coordinate providing services in the dependency injection container.
        /// </summary>
        public void Initialize()
        {
            this.unityContainer.RegisterType<EyeXGazeCoordinateService>(new ContainerControlledLifetimeManager());
            this.unityContainer.Resolve<EyeXGazeCoordinateService>();
        }
    }
}