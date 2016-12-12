// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Bootstrapper.cs" company="Numeral">
//   Copyright 2016 Fernando Ramírez Garibay
// </copyright>
// <summary>
//   Defines the Bootstrapper type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace TalkingKeyboard.Shell
{
    using System.Diagnostics.CodeAnalysis;
    using System.Windows;

    using Microsoft.Practices.Unity;
    
    using Prism.Logging;
    using Prism.Modularity;
    using Prism.Unity;
    
    using TalkingKeyboard.Infrastructure.Controls;
    using TalkingKeyboard.Modules.ByGazeTimePointProcessor;
    using TalkingKeyboard.Modules.CentralTextModule;
    using TalkingKeyboard.Modules.CoordinateProvider;
    using TalkingKeyboard.Modules.ImageBoard;
    using TalkingKeyboard.Modules.MicrosoftTextToSpeech;
    using TalkingKeyboard.Modules.MultiKeyBoard;
    using TalkingKeyboard.Modules.SingleKeyBoard;
    using TalkingKeyboard.Modules.SuggestionBoard;
    using TalkingKeyboard.Modules.SuggestionsProvider;
    using TalkingKeyboard.Shell.Logging;
    using TalkingKeyboard.Shell.Views;

    internal class Bootstrapper : UnityBootstrapper
    {
        protected override void ConfigureModuleCatalog()
        {
            // TODO: Sort based on dependencies (least to most, sort of)
            var moduleCatalog = (ModuleCatalog)this.ModuleCatalog;
            moduleCatalog.AddModule(typeof(TextHolderModule), InitializationMode.WhenAvailable);
            moduleCatalog.AddModule(typeof(SuggestionsProviderModule), InitializationMode.WhenAvailable);
            moduleCatalog.AddModule(typeof(CoordinateProviderModule), InitializationMode.WhenAvailable);
            moduleCatalog.AddModule(typeof(ImageBoardModule), InitializationMode.WhenAvailable);
            moduleCatalog.AddModule(typeof(MultiKeyBoardModule), InitializationMode.WhenAvailable);
            moduleCatalog.AddModule(typeof(SingleKeyBoardModule), InitializationMode.WhenAvailable);
            moduleCatalog.AddModule(typeof(SuggestionBoardModule), InitializationMode.WhenAvailable);
            moduleCatalog.AddModule(typeof(ByGazeTimePointProcessorModule), InitializationMode.WhenAvailable);
            moduleCatalog.AddModule(typeof(MicrosoftTextToSpeechModule), InitializationMode.WhenAvailable);
        }

        /// <summary>
        ///     Create the <see cref="T:Prism.Logging.ILoggerFacade" /> used by the bootstrapper.
        /// </summary>
        /// <returns>Returns an NLog Logger.</returns>
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1650:ElementDocumentationMustBeSpelledCorrectly",
             Justification = "That's the actual name.")]
        protected override ILoggerFacade CreateLogger()
        {
            // return base.CreateLogger();
            return new NLogLogger();
        }

        /// <summary>
        ///     Creates the shell or main window of the application.
        /// </summary>
        /// <returns>
        ///     The shell of the application.
        /// </returns>
        /// <remarks>
        ///     If the returned instance is a <see cref="T:System.Windows.DependencyObject" />, the
        ///     <see cref="T:Prism.Bootstrapper" /> will attach the default <see cref="T:Prism.Regions.IRegionManager" /> of
        ///     the application in its <see cref="F:Prism.Regions.RegionManager.RegionManagerProperty" /> attached property
        ///     in order to be able to add regions by using the <see cref="F:Prism.Regions.RegionManager.RegionNameProperty" />
        ///     attached property from XAML.
        /// </remarks>
        protected override DependencyObject CreateShell()
        {
            this.Container.RegisterType<ISelectableControlModel, SelectableControlModel>(new ContainerControlledLifetimeManager());
            this.Container.Resolve<ISelectableControlModel>();
            return this.Container.Resolve<MainWindow>();
        }

        /// <summary>
        ///     Initializes the shell.
        /// </summary>
        protected override void InitializeShell()
        {
            Application.Current.MainWindow.Show();
        }
    }
}