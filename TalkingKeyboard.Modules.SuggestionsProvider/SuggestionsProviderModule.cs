// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SuggestionsProviderModule.cs" company="Numeral">
//   Copyright 2016 Fernando Ramírez Garibay
// </copyright>
// <summary>
//   Defines the SuggestionsProviderModule type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace TalkingKeyboard.Modules.SuggestionsProvider
{
    using Microsoft.Practices.Unity;

    using Prism.Modularity;

    using TalkingKeyboard.Infrastructure.ServiceInterfaces;

    public class SuggestionsProviderModule : IModule
    {
        private readonly IUnityContainer unityContainer;

        /// <summary>
        ///     Initializes a new instance of the <see cref="SuggestionsProviderModule" /> class.
        /// </summary>
        /// <param name="unityContainer">The unity DI container (obtained through DI).</param>
        public SuggestionsProviderModule(IUnityContainer unityContainer)
        {
            this.unityContainer = unityContainer;
        }

        /// <summary>
        ///     Registers a unique instance of the suggestion service in the dependency injection container.
        /// </summary>
        public void Initialize()
        {
            this.unityContainer.RegisterType<ISuggestionService, SuggestionService>(
                new ContainerControlledLifetimeManager());
            this.unityContainer.Resolve<ISuggestionService>();
        }
    }
}