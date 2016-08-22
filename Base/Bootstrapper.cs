using System.Windows;
using Microsoft.Practices.Unity;
using Prism.Logging;
using Prism.Modularity;
using Prism.Unity;
using TalkingKeyboard.Modules.CentralTextModule;
using TalkingKeyboard.Modules.CommandBoard;
using TalkingKeyboard.Modules.CoordinateProvider;
using TalkingKeyboard.Modules.MultiKeyBoard;
using TalkingKeyboard.Modules.SingleKeyBoard;
using TalkingKeyboard.Modules.SuggestionBoard;
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

            //Container.RegisterTypeForNavigation<>("ViewA");
            //Container.RegisterTypeForNavigation<ViewB>("ViewB");
        }

        protected override void ConfigureModuleCatalog()
        {
            ModuleCatalog moduleCatalog = (ModuleCatalog)this.ModuleCatalog;
            moduleCatalog.AddModule(typeof(CommandBoardModule), InitializationMode.OnDemand);
            moduleCatalog.AddModule(typeof(CoordinateProviderModule), InitializationMode.OnDemand);
            moduleCatalog.AddModule(typeof(MultiKeyBoardModule), InitializationMode.OnDemand);
            moduleCatalog.AddModule(typeof(SingleKeyBoardModule), InitializationMode.WhenAvailable);
            moduleCatalog.AddModule(typeof(SuggestionBoardModule), InitializationMode.WhenAvailable);
            moduleCatalog.AddModule(typeof(TextHolderModule), InitializationMode.WhenAvailable);
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
