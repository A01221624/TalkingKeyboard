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
        private Storyboard _animation;
        private KeyTime _animationBeginTime = KeyTime.FromTimeSpan(TimeSpan.Zero);
        private KeyTime _animationEndTime = Configuration.GazeTimeSpanBeforeSelectionOccurs;
        private string _buttonText;
        private ICommand _command;
        private object _commandParameter;
        private IInputElement _commandTarget;
        private TimeSpan _currentGazeTimeSpan = TimeSpan.Zero;
        private double _fontSize = 40;
        private TimeSpan _gazeKeepAliveTimeSpan = Configuration.GazeKeepAliveTimeSpan;
        private TimeSpan _gazeTimeSpanBeforeAnimationBegins = Configuration.GazeTimeSpanBeforeAnimationBegins;

        private TimeSpan _gazeTimeSpanBeforeCooldown =
            Configuration.GazeTimeSpanBeforeCooldownOccurs;

        private TimeSpan _gazeTimeSpanBeforeSelectionOccurs = Configuration.GazeTimeSpanBeforeSelectionOccurs;
        private DateTime _lastSeenTime = DateTime.MinValue;
        private DateTime _lastSelectedTime = DateTime.MinValue;
        private SelectableState _state = SelectableState.Idle;

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
                return this._animation;
            }

            set
            {
                if (object.Equals(value, this._animation))
                {
                    return;
                }

                this._animation = value;
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
                return this._animationBeginTime;
            }

            set
            {
                if (value.Equals(this._animationBeginTime))
                {
                    return;
                }

                this._animationBeginTime = value;
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
                return this._animationEndTime;
            }

            set
            {
                if (value.Equals(this._animationEndTime))
                {
                    return;
                }

                this._animationEndTime = value;
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
                return this._buttonText;
            }

            set
            {
                if (value == this._buttonText)
                {
                    return;
                }

                this._buttonText = value;
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
                return this._command;
            }

            set
            {
                if (object.Equals(value, this._command))
                {
                    return;
                }

                this._command = value;
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
                return this._commandParameter;
            }

            set
            {
                if (object.Equals(value, this._commandParameter))
                {
                    return;
                }

                this._commandParameter = value;
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
                return this._commandTarget;
            }

            set
            {
                if (object.Equals(value, this._commandTarget))
                {
                    return;
                }

                this._commandTarget = value;
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
                return this._currentGazeTimeSpan;
            }

            set
            {
                if (value.Equals(this._currentGazeTimeSpan))
                {
                    return;
                }

                this._currentGazeTimeSpan = value;
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
                return this._fontSize;
            }

            set
            {
                {
                    if (value.Equals(this._fontSize))
                    {
                        return;
                    }
                }

                this._fontSize = value;
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
                return this._gazeKeepAliveTimeSpan;
            }

            set
            {
                if (value.Equals(this._gazeKeepAliveTimeSpan))
                {
                    return;
                }

                this._gazeKeepAliveTimeSpan = value;
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
                return this._gazeTimeSpanBeforeAnimationBegins;
            }

            set
            {
                if (value.Equals(this._gazeTimeSpanBeforeAnimationBegins))
                {
                    return;
                }

                this._gazeTimeSpanBeforeAnimationBegins = value;
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
                return this._gazeTimeSpanBeforeCooldown;
            }

            set
            {
                if (value.Equals(this._gazeTimeSpanBeforeCooldown))
                {
                    return;
                }

                this._gazeTimeSpanBeforeCooldown = value;
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
                return this._gazeTimeSpanBeforeSelectionOccurs;
            }

            set
            {
                if (value.Equals(this._gazeTimeSpanBeforeSelectionOccurs))
                {
                    return;
                }

                this._gazeTimeSpanBeforeSelectionOccurs = value;
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
                return this._lastSeenTime;
            }

            set
            {
                if (value.Equals(this._lastSeenTime))
                {
                    return;
                }

                this._lastSeenTime = value;
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
                return this._lastSelectedTime;
            }

            set
            {
                if (value.Equals(this._lastSelectedTime))
                {
                    return;
                }

                this._lastSelectedTime = value;
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
                return this._state;
            }

            set
            {
                if (value == this._state)
                {
                    return;
                }

                this._state = value;
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