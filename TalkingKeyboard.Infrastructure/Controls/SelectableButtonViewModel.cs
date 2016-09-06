using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Animation;
using TalkingKeyboard.Infrastructure.Annotations;
using TalkingKeyboard.Infrastructure.Enums;

namespace TalkingKeyboard.Infrastructure.Controls
{
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
            Configuration.GazeTimeSpanBeforeCooldownazeTimeSpanBeforeSelectionOccurs;

        private TimeSpan _gazeTimeSpanBeforeSelectionOccurs = Configuration.GazeTimeSpanBeforeSelectionOccurs;
        private DateTime _lastSeenTime = DateTime.MinValue;
        private DateTime _lastSelectedTime = DateTime.MinValue;
        private SelectableState _state = SelectableState.Idle;

        public string ButtonText
        {
            get { return _buttonText; }
            set
            {
                if (value == _buttonText) return;
                _buttonText = value;
                OnPropertyChanged();
            }
        }

        public double FontSize
        {
            get { return _fontSize; }
            set
            {
                if (value.Equals(_fontSize)) return;
                _fontSize = value;
                OnPropertyChanged();
            }
        }

        public KeyTime AnimationBeginTime
        {
            get { return _animationBeginTime; }
            set
            {
                if (value.Equals(_animationBeginTime)) return;
                _animationBeginTime = value;
                OnPropertyChanged();
            }
        }

        public KeyTime AnimationEndTime
        {
            get { return _animationEndTime; }
            set
            {
                if (value.Equals(_animationEndTime)) return;
                _animationEndTime = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public ICommand Command
        {
            get { return _command; }
            set
            {
                if (Equals(value, _command)) return;
                _command = value;
                OnPropertyChanged();
            }
        }

        public object CommandParameter
        {
            get { return _commandParameter; }
            set
            {
                if (Equals(value, _commandParameter)) return;
                _commandParameter = value;
                OnPropertyChanged();
            }
        }

        public IInputElement CommandTarget
        {
            get { return _commandTarget; }
            set
            {
                if (Equals(value, _commandTarget)) return;
                _commandTarget = value;
                OnPropertyChanged();
            }
        }

        public DateTime LastSelectedTime
        {
            get { return _lastSelectedTime; }
            set
            {
                if (value.Equals(_lastSelectedTime)) return;
                _lastSelectedTime = value;
                OnPropertyChanged();
            }
        }

        public DateTime LastSeenTime
        {
            get { return _lastSeenTime; }
            set
            {
                if (value.Equals(_lastSeenTime)) return;
                _lastSeenTime = value;
                OnPropertyChanged();
            }
        }

        public TimeSpan CurrentGazeTimeSpan
        {
            get { return _currentGazeTimeSpan; }
            set
            {
                if (value.Equals(_currentGazeTimeSpan)) return;
                _currentGazeTimeSpan = value;
                OnPropertyChanged();
            }
        }

        public SelectableState State
        {
            get { return _state; }
            set
            {
                if (value == _state) return;
                _state = value;
                OnPropertyChanged();
            }
        }

        public TimeSpan GazeKeepAliveTimeSpan
        {
            get { return _gazeKeepAliveTimeSpan; }
            set
            {
                if (value.Equals(_gazeKeepAliveTimeSpan)) return;
                _gazeKeepAliveTimeSpan = value;
                OnPropertyChanged();
            }
        }

        public TimeSpan GazeTimeSpanBeforeAnimationBegins
        {
            get { return _gazeTimeSpanBeforeAnimationBegins; }
            set
            {
                if (value.Equals(_gazeTimeSpanBeforeAnimationBegins)) return;
                _gazeTimeSpanBeforeAnimationBegins = value;
                OnPropertyChanged();
            }
        }

        public TimeSpan GazeTimeSpanBeforeSelectionOccurs
        {
            get { return _gazeTimeSpanBeforeSelectionOccurs; }
            set
            {
                if (value.Equals(_gazeTimeSpanBeforeSelectionOccurs)) return;
                _gazeTimeSpanBeforeSelectionOccurs = value;
                OnPropertyChanged();
            }
        }

        public TimeSpan GazeTimeSpanBeforeCooldown
        {
            get { return _gazeTimeSpanBeforeCooldown; }
            set
            {
                if (value.Equals(_gazeTimeSpanBeforeCooldown)) return;
                _gazeTimeSpanBeforeCooldown = value;
                OnPropertyChanged();
            }
        }

        public Storyboard Animation
        {
            get { return _animation; }
            set
            {
                if (Equals(value, _animation)) return;
                _animation = value;
                OnPropertyChanged();
            }
        }

        public void PlayAnimation()
        {
            throw new NotImplementedException();
        }

        public void PauseAnimation()
        {
            throw new NotImplementedException();
        }

        public void StopAnimation()
        {
            throw new NotImplementedException();
        }

        public void ResumeAnimation()
        {
            throw new NotImplementedException();
        }

        public void Select()
        {
            throw new NotImplementedException();
        }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}