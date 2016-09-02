using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using TalkingKeyboard.Infrastructure.Annotations;

namespace TalkingKeyboard.Modules.SingleKeyBoard.ViewModels
{
    public class QwertySpanishSingleKeyboardViewModel : INotifyPropertyChanged
    {
        public QwertySpanishSingleKeyboardViewModel()
        {
            _setShiftDownCommand = new DelegateCommand<bool?>(b => IsShiftDown = b ?? false);
            _toggleShiftDownCommand = new DelegateCommand(() => IsShiftDown = !IsShiftDown);
            Infrastructure.Commands.ToggleShiftDownCommand.RegisterCommand(_toggleShiftDownCommand);
        }

        private bool _isShiftDown = false;
        private ICommand _setShiftDownCommand;
        private ICommand _toggleShiftDownCommand;

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

        public ICommand SetShiftDownCommand
        {
            get { return _setShiftDownCommand; }
            set { _setShiftDownCommand = value; }
        }

        public ICommand ToggleShiftDownCommand
        {
            get { return _toggleShiftDownCommand; }
            set { _toggleShiftDownCommand = value; }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
