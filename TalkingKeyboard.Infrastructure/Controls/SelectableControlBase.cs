// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SelectableControlBase.cs" company="Numeral">
//   Copyright 2016 Fernando Ramírez Garibay
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace TalkingKeyboard.Infrastructure.Controls
{
    using System;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Input;
    using System.Windows.Media.Animation;

    using Prism.Commands;

    using TalkingKeyboard.Infrastructure.Annotations;
    using TalkingKeyboard.Infrastructure.Enums;

    /// <summary>
    /// The selectable control base.
    /// </summary>
    public class SelectableControlBase : ISelectableControlViewModel, INotifyPropertyChanged
    {
        private Storyboard animation;
        private KeyTime animationBeginTime = KeyTime.FromTimeSpan(TimeSpan.Zero);
        private KeyTime animationEndTime = Configuration.GazeTimeSpanBeforeSelectionOccurs;
        private ICommand command;
        private object commandParameter;
        private IInputElement commandTarget;
        private TimeSpan currentGazeTimeSpan = TimeSpan.Zero;
        private TimeSpan gazeCoolDownTimeSpan = Configuration.GazeCoolDownTimeSpan;
        private TimeSpan gazeKeepAliveTimeSpan = Configuration.GazeKeepAliveTimeSpan;
        private TimeSpan gazeTimeSpanBeforeAnimationBegins = Configuration.GazeTimeSpanBeforeAnimationBegins;

        private TimeSpan gazeTimeSpanBeforeCooldown = Configuration.GazeTimeSpanBeforeCooldownOccurs;

        private TimeSpan gazeTimeSpanBeforeSelectionOccurs = Configuration.GazeTimeSpanBeforeSelectionOccurs;
        private DateTime lastSeenTime = DateTime.MinValue;
        private DateTime lastSelectedTime = DateTime.MinValue;
        private SelectableState state = SelectableState.Idle;

        /// <summary>
        /// Initializes a new instance of the <see cref="SelectableControlBase"/> class.
        /// </summary>
        /// <param name="selectableControlModel">
        /// The selectable control model.
        /// </param>
        public SelectableControlBase(ISelectableControlModel selectableControlModel)
        {
            this.SelectableControlModel = selectableControlModel;
            this.SelectableControlModel.PropertyChanged += this.SelectableButtonModelOnPropertyChanged;

            this.ToggleIsSelectableCommand = new DelegateCommand(() => this.IsSelectable = !this.IsSelectable);
            Commands.ToggleSelectionEnabledCommand.RegisterCommand(this.ToggleIsSelectableCommand);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SelectableControlBase"/> class.
        /// </summary>
        public SelectableControlBase()
        {
            this.ToggleIsSelectableCommand = new DelegateCommand(() => this.IsSelectable = !this.IsSelectable);
            Commands.ToggleSelectionEnabledCommand.RegisterCommand(this.ToggleIsSelectableCommand);
        }

        /// <summary>
        ///     The property changed.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        ///     Gets or sets the animation.
        /// </summary>
        public Storyboard Animation
        {
            get
            {
                return this.animation;
            }

            set
            {
                if (value.Equals(this.animation))
                {
                    return;
                }

                this.animation = value;
                this.OnPropertyChanged();
            }
        }

        /// <summary>
        ///     Gets or sets the animation begin time.
        /// </summary>
        public KeyTime AnimationBeginTime
        {
            get
            {
                return this.animationBeginTime;
            }

            set
            {
                if (value.Equals(this.animationBeginTime))
                {
                    return;
                }

                this.animationBeginTime = value;
                this.OnPropertyChanged();
            }
        }

        /// <summary>
        ///     Gets or sets the animation end time.
        /// </summary>
        public KeyTime AnimationEndTime
        {
            get
            {
                return this.GazeTimeSpanBeforeSelectionOccurs;
            }

            set
            {
                if (value.Equals(this.animationEndTime))
                {
                    return;
                }

                this.animationEndTime = value;
                this.OnPropertyChanged();
            }
        }

        /// <summary>
        ///     Gets or sets the command.
        /// </summary>
        public ICommand Command
        {
            get
            {
                return this.command;
            }

            set
            {
                if (value.Equals(this.command))
                {
                    return;
                }

                this.command = value;
                this.OnPropertyChanged();
            }
        }

        /// <summary>
        ///     Gets or sets the command parameter.
        /// </summary>
        public object CommandParameter
        {
            get
            {
                return this.commandParameter;
            }

            set
            {
                if (value.Equals(this.commandParameter))
                {
                    return;
                }

                this.commandParameter = value;
                this.OnPropertyChanged();
            }
        }

        /// <summary>
        ///     Gets or sets the command target.
        /// </summary>
        public IInputElement CommandTarget
        {
            get
            {
                return this.commandTarget;
            }

            set
            {
                if (value.Equals(this.commandTarget))
                {
                    return;
                }

                this.commandTarget = value;
                this.OnPropertyChanged();
            }
        }

        /// <summary>
        ///     Gets or sets the current gaze time span.
        /// </summary>
        public TimeSpan CurrentGazeTimeSpan
        {
            get
            {
                return this.currentGazeTimeSpan;
            }

            set
            {
                if (value.Equals(this.currentGazeTimeSpan))
                {
                    return;
                }

                this.currentGazeTimeSpan = value;
                this.OnPropertyChanged();
            }
        }

        /// <summary>
        ///     Gets or sets the gaze cool down time span.
        /// </summary>
        public TimeSpan GazeCoolDownTimeSpan
        {
            get
            {
                return this.gazeCoolDownTimeSpan;
            }

            set
            {
                if (value.Equals(this.gazeCoolDownTimeSpan))
                {
                    return;
                }

                this.gazeCoolDownTimeSpan = value;
                this.OnPropertyChanged();
            }
        }

        /// <summary>
        ///     Gets or sets the gaze keep alive time span.
        /// </summary>
        public TimeSpan GazeKeepAliveTimeSpan
        {
            get
            {
                return this.gazeKeepAliveTimeSpan;
            }

            set
            {
                if (value.Equals(this.gazeKeepAliveTimeSpan))
                {
                    return;
                }

                this.gazeKeepAliveTimeSpan = value;
                this.OnPropertyChanged();
            }
        }

        /// <summary>
        ///     Gets or sets the gaze time span before animation begins.
        /// </summary>
        public TimeSpan GazeTimeSpanBeforeAnimationBegins
        {
            get
            {
                return this.gazeTimeSpanBeforeAnimationBegins;
            }

            set
            {
                if (value.Equals(this.gazeTimeSpanBeforeAnimationBegins))
                {
                    return;
                }

                this.gazeTimeSpanBeforeAnimationBegins = value;
                this.OnPropertyChanged();
            }
        }

        /// <summary>
        ///     Gets or sets the gaze time span before cool-down.
        /// </summary>
        public TimeSpan GazeTimeSpanBeforeCooldown
        {
            get
            {
                return this.GazeTimeSpanBeforeSelectionOccurs + this.GazeCoolDownTimeSpan;
            }

            set
            {
                if (value.Equals(this.gazeTimeSpanBeforeCooldown))
                {
                    return;
                }

                this.gazeTimeSpanBeforeCooldown = value;
                this.OnPropertyChanged();
            }
        }

        /// <summary>
        ///     Gets or sets the gaze time span before selection occurs.
        /// </summary>
        public TimeSpan GazeTimeSpanBeforeSelectionOccurs
        {
            get
            {
                return this.SelectableControlModel.RequiredGazeTimeSpan + this.GazeTimeSpanBeforeAnimationBegins;
            }

            set
            {
                if (value.Equals(this.gazeTimeSpanBeforeSelectionOccurs))
                {
                    return;
                }

                this.gazeTimeSpanBeforeSelectionOccurs = value;
                this.OnPropertyChanged();
            }
        }

        /// <summary>
        ///     Gets or sets a value indicating whether this instance is selectable.
        /// </summary>
        /// <value>
        ///     <c>true</c> if this instance is selectable; otherwise, <c>false</c>.
        /// </value>
        public bool IsSelectable { get; set; } = true;

        /// <summary>
        ///     Gets or sets the last seen time.
        /// </summary>
        public DateTime LastSeenTime
        {
            get
            {
                return this.lastSeenTime;
            }

            set
            {
                if (value.Equals(this.lastSeenTime))
                {
                    return;
                }

                this.lastSeenTime = value;
                this.OnPropertyChanged();
            }
        }

        /// <summary>
        ///     Gets or sets the last selected time.
        /// </summary>
        public DateTime LastSelectedTime
        {
            get
            {
                return this.lastSelectedTime;
            }

            set
            {
                if (value.Equals(this.lastSelectedTime))
                {
                    return;
                }

                this.lastSelectedTime = value;
                this.OnPropertyChanged();
            }
        }

        /// <summary>
        ///     Gets the selectable button model.
        /// </summary>
        /// <value>
        ///     The selectable button model.
        /// </value>
        public ISelectableControlModel SelectableControlModel { get; }

        /// <summary>
        ///     Gets or sets the state.
        /// </summary>
        public SelectableState State
        {
            get
            {
                return this.state;
            }

            set
            {
                if (value == this.state)
                {
                    return;
                }

                this.state = value;
                this.OnPropertyChanged();
            }
        }

        /// <summary>
        ///     Gets the toggle is selectable command.
        /// </summary>
        /// <value>
        ///     The toggle is selectable command.
        /// </value>
        public ICommand ToggleIsSelectableCommand { get; }

        /// <summary>
        ///     <inheritdoc />
        /// </summary>
        /// <exception cref="NotImplementedException">
        ///     TODO: Consider changing the selection to the ViewModel.
        /// </exception>
        public void Select()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Call on property changed.
        /// </summary>
        /// <param name="propertyName">
        /// Name of property which changed.
        /// </param>
        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// The selectable button model on property changed.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="propertyChangedEventArgs">
        /// The property changed event args.
        /// </param>
        private void SelectableButtonModelOnPropertyChanged(
            object sender,
            PropertyChangedEventArgs propertyChangedEventArgs)
        {
            if (propertyChangedEventArgs.PropertyName == "RequiredGazeTimeSpan")
            {
                this.OnPropertyChanged(nameof(this.AnimationEndTime));
            }
        }
    }
}