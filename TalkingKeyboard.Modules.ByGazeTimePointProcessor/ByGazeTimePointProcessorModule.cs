// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ByGazeTimePointProcessorModule.cs" company="Numeral">
//   Copyright 2016 Fernando Ramírez Garibay
// </copyright>
// <summary>
//   Defines the ByGazeTimePointProcessorModule type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace TalkingKeyboard.Modules.ByGazeTimePointProcessor
{
    using Microsoft.Practices.Unity;

    using Prism.Modularity;

    using TalkingKeyboard.Infrastructure.ServiceInterfaces;

    public class ByGazeTimePointProcessorModule : IModule
    {
        private readonly IUnityContainer _unityContainer;

        /// <summary>
        ///     Initializes a new instance of the <see cref="ByGazeTimePointProcessorModule" /> class.
        /// </summary>
        /// <param name="unityContainer">The unity dependency injection container (obtained through DI).</param>
        public ByGazeTimePointProcessorModule(IUnityContainer unityContainer)
        {
            this._unityContainer = unityContainer;
        }

        /// <summary>
        ///     Notifies the module that it has been initialized.
        /// </summary>
        public void Initialize()
        {
            this._unityContainer.RegisterType<IControlActivationService, GazeSelectionService>(
                new ContainerControlledLifetimeManager());
            this._unityContainer.Resolve<IControlActivationService>();
        }
    }
}