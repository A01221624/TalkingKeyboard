using Microsoft.Practices.Unity;
using Prism.Modularity;
using TalkingKeyboard.Infrastructure.ServiceInterfaces;

namespace TalkingKeyboard.Modules.SuggestionsProvider
{
    public class SuggestionsProviderModule : IModule
    {
        private readonly IUnityContainer _unityContainer;

        public SuggestionsProviderModule(IUnityContainer unityContainer)
        {
            _unityContainer = unityContainer;
        }

        public void Initialize()
        {
            _unityContainer.RegisterType<ISuggestionService, SuggestionService>(
                new ContainerControlledLifetimeManager());
            _unityContainer.Resolve<ISuggestionService>();
        }
    }
}