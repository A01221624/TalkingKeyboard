using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Prism.Commands;
using TalkingKeyboard.Infrastructure;
using TalkingKeyboard.Infrastructure.Annotations;

namespace TalkingKeyboard.Modules.SingleKeyBoard.ViewModels
{
    public class QwertySpanishSingleKeyboardViewModel : INotifyPropertyChanged
    {
        private bool _isShiftDown;

        public QwertySpanishSingleKeyboardViewModel()
        {
            SetShiftDownCommand = new DelegateCommand<bool?>(b => IsShiftDown = b ?? false);
            ToggleShiftDownCommand = new DelegateCommand(() => IsShiftDown = !IsShiftDown);
            Commands.ToggleShiftDownCommand.RegisterCommand(ToggleShiftDownCommand);
        }

        public bool IsShiftDown
        {
            get { return _isShiftDown; }
            set
            {
                if (value == _isShiftDown) return;
                _isShiftDown = value;
                OnPropertyChanged();
            }
        }

        public ICommand SetShiftDownCommand { get; set; }

        public ICommand ToggleShiftDownCommand { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}