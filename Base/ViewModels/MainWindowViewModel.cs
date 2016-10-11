// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MainWindowViewModel.cs" company="Numeral">
//   Copyright 2016 Fernando Ramírez Garibay
// </copyright>
// <summary>
//   Defines the MainWindowViewModel type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace TalkingKeyboard.Shell.ViewModels
{
    using System.Collections.Generic;
    using System.Windows.Input;

    using Prism.Commands;
    using Prism.Mvvm;
    using Prism.Regions;

    using TalkingKeyboard.Infrastructure.Constants;

    public class MainWindowViewModel : BindableBase
    {
        private readonly List<string> knownBoards;
        private readonly IRegionManager regionManager;
        private int currentViewIndex; // Must be set to index of initial board.
        private string title = "TalkingKeyboard";

        /// <summary>
        ///     Initializes a new instance of the <see cref="MainWindowViewModel" /> class.
        /// </summary>
        /// <param name="regionManager">The region manager (obtained through dependency injection).</param>
        public MainWindowViewModel(IRegionManager regionManager)
        {
            this.regionManager = regionManager;
            this.knownBoards = new List<string>
                                   {
                                       ViewNames.QwertySpanishMultiKeyboard,
                                       ViewNames.QwertySpanishSingleKeyboard
                                   };

            this.ChangeViewToLeftCommand = new DelegateCommand(
                                               () =>
                                                   {
                                                       if (--this.currentViewIndex < 0)
                                                       {
                                                           this.currentViewIndex = this.knownBoards.Count - 1;
                                                       }

                                                       this.regionManager.RequestNavigate(
                                                           RegionNames.BoardViewRegion,
                                                           this.knownBoards[this.currentViewIndex]);
                                                   });
            this.ChangeViewToRightCommand = new DelegateCommand(
                                                () =>
                                                    {
                                                        if (++this.currentViewIndex >= this.knownBoards.Count)
                                                        {
                                                            this.currentViewIndex = 0;
                                                        }

                                                        this.regionManager.RequestNavigate(
                                                            RegionNames.BoardViewRegion,
                                                            this.knownBoards[this.currentViewIndex]);
                                                    });
        }

        /// <summary>
        ///     Gets or sets the command for switching to the view on the left.
        /// </summary>
        /// <value>
        ///     The command for switching to the view on the left.
        /// </value>
        public ICommand ChangeViewToLeftCommand { get; set; }

        /// <summary>
        ///     Gets or sets the command for switching to the view on the right.
        /// </summary>
        /// <value>
        ///     The command for switching to the view on the right.
        /// </value>
        public ICommand ChangeViewToRightCommand { get; set; }

        /// <summary>
        ///     Gets or sets the title of the window.
        /// </summary>
        /// <value>
        ///     The title of the window.
        /// </value>
        public string Title
        {
            get
            {
                return this.title;
            }

            set
            {
                this.SetProperty(ref this.title, value);
            }
        }
    }
}