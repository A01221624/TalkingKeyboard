// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SelectableButtonModel.cs" company="Numeral">
//   Copyright 2016 Fernando Ramírez Garibay
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace TalkingKeyboard.Infrastructure.Controls
{
    using System;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Windows.Input;

    using Prism.Commands;

    using TalkingKeyboard.Infrastructure.Annotations;

    /// <summary>
    ///     The selectable button model.
    /// </summary>
    public class SelectableButtonModel : ISelectableButtonModel
    {
        private TimeSpan requiredGazeTimeSpan = Configuration.InitialRequiredGazeTime;

        /// <summary>
        ///     Initializes a new instance of the <see cref="SelectableButtonModel" /> class.
        /// </summary>
        public SelectableButtonModel()
        {
            this.IncreaseSelectionSpeedCommand =
                new DelegateCommand(() => { this.RequiredGazeTimeSpan -= TimeSpan.FromMilliseconds(100); });
            Commands.IncreaseSelectionSpeedCommand.RegisterCommand(this.IncreaseSelectionSpeedCommand);
            this.DecreaseSelectionSpeedCommand =
                new DelegateCommand(() => { this.RequiredGazeTimeSpan += TimeSpan.FromMilliseconds(100); });
            Commands.DecreaseSelectionSpeedCommand.RegisterCommand(this.DecreaseSelectionSpeedCommand);
        }

        /// <summary>
        ///     The property changed.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        ///     Gets the command for decreasing the selection speed.
        /// </summary>
        /// <value>
        ///     The command for decreasing the selection speed.
        /// </value>
        public ICommand DecreaseSelectionSpeedCommand { get; }

        /// <summary>
        ///     Gets the command for increasing the selection speed.
        /// </summary>
        /// <value>
        ///     The command for increasing the selection speed.
        /// </value>
        public ICommand IncreaseSelectionSpeedCommand { get; }

        /// <summary>
        ///     Gets or sets the required gaze time span.
        /// </summary>
        public TimeSpan RequiredGazeTimeSpan
        {
            get
            {
                return this.requiredGazeTimeSpan;
            }

            set
            {
                if ((value == this.requiredGazeTimeSpan) || (value < TimeSpan.FromMilliseconds(150))
                    || (value > TimeSpan.FromSeconds(1.5)))
                {
                    return;
                }

                this.requiredGazeTimeSpan = value;
                this.OnPropertyChanged();
            }
        }

        /// <summary>
        ///     Call on property changed.
        /// </summary>
        /// <param name="propertyName">
        ///     Name of property which changed.
        /// </param>
        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}