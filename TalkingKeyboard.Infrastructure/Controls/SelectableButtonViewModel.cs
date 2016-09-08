// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SelectableButtonViewModel.cs" company="Numeral">
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

    using TalkingKeyboard.Infrastructure.Annotations;
    using TalkingKeyboard.Infrastructure.Enums;

    /// <summary>
    ///     The selectable button view model.
    /// </summary>
    public class SelectableButtonViewModel : ISelectableControlViewModel, INotifyPropertyChanged
    {
        private Storyboard animation;
        private KeyTime animationBeginTime = KeyTime.FromTimeSpan(TimeSpan.Zero);
        private KeyTime animationEndTime = Configuration.GazeTimeSpanBeforeSelectionOccurs;
        private string buttonText;
        private ICommand command;
        private object commandParameter;
        private IInputElement commandTarget;
        private TimeSpan currentGazeTimeSpan = TimeSpan.Zero;
        private double fontSize = 40;
        private TimeSpan gazeKeepAliveTimeSpan = Configuration.GazeKeepAliveTimeSpan;
        private TimeSpan gazeTimeSpanBeforeAnimationBegins = Configuration.GazeTimeSpanBeforeAnimationBegins;

        private TimeSpan gazeTimeSpanBeforeCooldown =
            Configuration.GazeTimeSpanBeforeCooldownOccurs;

        private TimeSpan gazeTimeSpanBeforeSelectionOccurs = Configuration.GazeTimeSpanBeforeSelectionOccurs;
        private DateTime lastSeenTime = DateTime.MinValue;
        private DateTime lastSelectedTime = DateTime.MinValue;
        private SelectableState state = SelectableState.Idle;

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
                if (object.Equals(value, this.animation))
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
                return this.animationEndTime;
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
        ///     Gets or sets the button text.
        /// </summary>
        public string ButtonText
        {
            get
            {
                return this.buttonText;
            }

            set
            {
                if (value == this.buttonText)
                {
                    return;
                }

                this.buttonText = value;
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
                if (object.Equals(value, this.command))
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
                if (object.Equals(value, this.commandParameter))
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
                if (object.Equals(value, this.commandTarget))
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
        ///     Gets or sets the font size.
        /// </summary>
        public double FontSize
        {
            get
            {
                return this.fontSize;
            }

            set
            {
                {
                    if (value.Equals(this.fontSize))
                    {
                        return;
                    }
                }

                this.fontSize = value;
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
                return this.gazeTimeSpanBeforeCooldown;
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
                return this.gazeTimeSpanBeforeSelectionOccurs;
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
        ///     <inheritdoc/>
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
    }
}