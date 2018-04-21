// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MainWindowViewModel.cs" company="Numeral">
//   Copyright 2016 Fernando Ramírez Garibay
// </copyright>
// <summary>
//   Defines the MainWindowViewModel type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Windows;
using MahApps.Metro;

namespace TalkingKeyboard.Shell.ViewModels
{
    using System.Collections.Generic;
    using System.Windows.Input;

    using Prism.Commands;
    using Prism.Mvvm;
    using Prism.Regions;

    using TalkingKeyboard.Infrastructure;
    using TalkingKeyboard.Infrastructure.Constants;

    /// <summary>
    ///     This class describes the logic for the main window, which is currently only registering relevant objects.
    /// </summary>
    /// <seealso cref="Prism.Mvvm.BindableBase" />
    public class MainWindowViewModel : BindableBase
    {
        private readonly List<string> knownBoards;
        private readonly IRegionManager regionManager;
        private int currentViewIndex = 0; // Must be set to index of initial board.
        private int currentFriendlySpeed = 11;
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
                                       ViewNames.SimpleImageBoard,
                                       ViewNames.NumericKeyboard,
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
            this.ToggleThemeCommand = new DelegateCommand(() =>
            {
                Tuple<AppTheme, Accent> appStyle = ThemeManager.DetectAppStyle(Application.Current);

                if (appStyle.Item1.Name == "BaseLight")
                {
                    ThemeManager.ChangeAppStyle(Application.Current,
                        appStyle.Item2,
                        ThemeManager.GetAppTheme("BaseDark"));
                }
                else
                {
                    ThemeManager.ChangeAppStyle(Application.Current,
                        appStyle.Item2,
                        ThemeManager.GetAppTheme("BaseLight"));
                }
            });
            this.IncreaseSelectionSpeedCommand =
                new DelegateCommand(() => { this.UserFriendlySpeedValue += 1; });
            Commands.IncreaseSelectionSpeedCommand.RegisterCommand(this.IncreaseSelectionSpeedCommand);
            this.DecreaseSelectionSpeedCommand =
                new DelegateCommand(() => { this.UserFriendlySpeedValue -= 1; });
            Commands.DecreaseSelectionSpeedCommand.RegisterCommand(this.DecreaseSelectionSpeedCommand);
        }

        public int UserFriendlySpeedValue
        {
            get
            {
                return this.currentFriendlySpeed;
            }

            set
            {
                if (value < 1 || value > 14)
                {
                    return;
                }
                this.SetProperty(ref this.currentFriendlySpeed, value);
            }
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
        ///     Gets or sets the command to run when speed is increased.
        ///     This is just the part which updates the speed label.
        /// </summary>
        /// <value>
        ///     The command to run when speed is increased.
        /// </value>
        public ICommand IncreaseSelectionSpeedCommand { get; set; }

        /// <summary>
        ///     Gets or sets the command to run when speed is decreased.
        ///     This is just the part which updates the speed label.
        /// </summary>
        /// <value>
        ///     The command to run when speed is decreased.
        /// </value>
        public ICommand DecreaseSelectionSpeedCommand { get; set; }

        /// <summary>
        ///     Gets or sets the command for toggling the UI theme.
        /// </summary>
        /// <value>
        ///     The command for toggling the UI theme.
        /// </value>
        public ICommand ToggleThemeCommand { get; set; }

        /// <summary>
        ///     Gets or sets a dummy command used to determine if selection is enabled.
        /// </summary>
        /// <value>
        ///     The dummy command.
        /// </value>
        public ICommand DummyCommand { get; set; }

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