// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SuggestionsProviderModule.cs" company="Numeral">
//   Copyright 2016 Fernando Ramírez Garibay
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace TalkingKeyboard.Modules.SuggestionsProvider
{
    using Microsoft.Practices.Unity;

    using Prism.Modularity;

    using TalkingKeyboard.Infrastructure.ServiceInterfaces;

    /// <summary>
    ///     Class for initializing the module for providing suggestions based on the context (current text, current multi-text,
    ///     etc.) given certain algorithms or services known as sources.
    /// </summary>
    /// <seealso cref="Prism.Modularity.IModule" />
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