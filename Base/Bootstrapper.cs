using System.Windows;
using Microsoft.Practices.Unity;
using Prism.Logging;
using Prism.Modularity;
using Prism.Unity;
using TalkingKeyboard.Infrastructure.ServiceInterfaces;
using TalkingKeyboard.Modules.ByGazeTimePointProcessor;
using TalkingKeyboard.Modules.CentralTextModule;
using TalkingKeyboard.Modules.CommandBoard;
using TalkingKeyboard.Modules.CoordinateProvider;
using TalkingKeyboard.Modules.MicrosoftTextToSpeech;
using TalkingKeyboard.Modules.MultiKeyBoard;
using TalkingKeyboard.Modules.SingleKeyBoard;
using TalkingKeyboard.Modules.SuggestionBoard;
using TalkingKeyboard.Modules.SuggestionsProvider;
using TalkingKeyboard.Shell.Views;

namespace TalkingKeyboard.Shell
{
    class Bootstrapper : UnityBootstrapper
    {
        protected override DependencyObject CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

        protected override void InitializeShell()
        {
            Application.Current.MainWindow.Show();
        }

        protected override void ConfigureContainer()
        {
            base.ConfigureContainer();
            //ViewModelLocationProvider.SetDefaultViewModelFactory((view, viewModelType) =>
            //{
            //    if (viewModelType == typeof(SelectableButtonViewModel))
            //    {
            //        Container.RegisterInstance<ISelectableControlViewModel>(view.GetHashCode().ToString(), new SelectableButtonViewModel());
            //        return Container.Resolve<ISelectableControlViewModel>(view.GetHashCode().ToString());
            //    }
            //    return Container.Resolve(viewModelType);
            //});

            //Container.RegisterTypeForNavigation<>("ViewA");
            //Container.RegisterTypeForNavigation<ViewB>("ViewB");
        }

        protected override void ConfigureModuleCatalog()
        {
            //TODO: Sort based on dependencies (least to most, sort of)
            ModuleCatalog moduleCatalog = (ModuleCatalog)this.ModuleCatalog;
            moduleCatalog.AddModule(typeof(SuggestionsProviderModule), InitializationMode.WhenAvailable);
            moduleCatalog.AddModule(typeof(CommandBoardModule), InitializationMode.OnDemand);
            moduleCatalog.AddModule(typeof(CoordinateProviderModule), InitializationMode.WhenAvailable);
            moduleCatalog.AddModule(typeof(MultiKeyBoardModule), InitializationMode.WhenAvailable);
            moduleCatalog.AddModule(typeof(SingleKeyBoardModule), InitializationMode.WhenAvailable);
            moduleCatalog.AddModule(typeof(TextHolderModule), InitializationMode.WhenAvailable);
            moduleCatalog.AddModule(typeof(SuggestionBoardModule), InitializationMode.WhenAvailable);
            moduleCatalog.AddModule(typeof(ByGazeTimePointProcessorModule), InitializationMode.WhenAvailable);
            moduleCatalog.AddModule(typeof(MicrosoftTextToSpeechModule), InitializationMode.WhenAvailable);
        }

        /// <summary>
        /// Create logger
        /// </summary>
        /// <returns></returns>
        protected override ILoggerFacade CreateLogger()
        {
            //return base.CreateLogger();
            return new Logging.NLogLogger();
        }
    }
}
