// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ByGazeTimePointProcessorModule.cs" company="Numeral">
//   Copyright 2016 Fernando Ramírez Garibay
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace TalkingKeyboard.Modules.ByGazeTimePointProcessor
{
    using Microsoft.Practices.Unity;

    using Prism.Modularity;

    using TalkingKeyboard.Infrastructure.ServiceInterfaces;

    /// <summary>
    ///     Class for initializing the module for gaze processing.
    /// </summary>
    /// <seealso cref="Prism.Modularity.IModule" />
    public class ByGazeTimePointProcessorModule : IModule
    {
        private readonly IUnityContainer unityContainer;

        /// <summary>
        ///     Initializes a new instance of the <see cref="ByGazeTimePointProcessorModule" /> class.
        /// </summary>
        /// <param name="unityContainer">The unity dependency injection container (obtained through DI).</param>
        public ByGazeTimePointProcessorModule(IUnityContainer unityContainer)
        {
            this.unityContainer = unityContainer;
        }

        /// <summary>
        ///     Registers a unique instance of the selection-by-gaze service in the dependency injection container.
        /// </summary>
        public void Initialize()
        {
            this.unityContainer.RegisterType<IControlActivationService, GazeSelectionService>(
                new ContainerControlledLifetimeManager());
            this.unityContainer.Resolve<IControlActivationService>();
        }
    }
}