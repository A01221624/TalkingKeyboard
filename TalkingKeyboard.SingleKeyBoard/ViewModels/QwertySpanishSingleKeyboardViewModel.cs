// --------------------------------------------------------------------------------------------------------------------
// <copyright file="QwertySpanishSingleKeyboardViewModel.cs" company="Numeral">
//   Copyright 2016 Fernando Ramírez Garibay
// </copyright>
// <summary>
//   Defines the QwertySpanishSingleKeyboardViewModel type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace TalkingKeyboard.Modules.SingleKeyBoard.ViewModels
{
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Windows.Input;

    using Prism.Commands;

    using TalkingKeyboard.Infrastructure;
    using TalkingKeyboard.Infrastructure.Annotations;

    public class QwertySpanishSingleKeyboardViewModel : INotifyPropertyChanged
    {
        private bool _isShiftDown;

        /// <summary>
        ///     Initializes a new instance of the <see cref="QwertySpanishSingleKeyboardViewModel" /> class.
        /// </summary>
        public QwertySpanishSingleKeyboardViewModel()
        {
            this.SetShiftDownCommand = new DelegateCommand<bool?>(b => this.IsShiftDown = b ?? false);
            this.ToggleShiftDownCommand = new DelegateCommand(() => this.IsShiftDown = !this.IsShiftDown);
            Commands.ToggleShiftDownCommand.RegisterCommand(this.ToggleShiftDownCommand);
        }

        /// <summary>
        ///     Occurs when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        ///     Gets or sets a value indicating whether this instance has shift down.
        /// </summary>
        /// <value>
        ///     <c>true</c> if this instance has shift down; otherwise, <c>false</c>.
        /// </value>
        public bool IsShiftDown
        {
            get
            {
                return this._isShiftDown;
            }

            set
            {
                if (value == this._isShiftDown)
                {
                    return;
                }

                this._isShiftDown = value;
                this.OnPropertyChanged();
            }
        }

        /// <summary>
        ///     Gets or sets the command for setting shift down.
        /// </summary>
        /// <value>
        ///     The command for setting shift down.
        /// </value>
        public ICommand SetShiftDownCommand { get; set; }

        /// <summary>
        ///     Gets or sets the command for toggling shift down.
        /// </summary>
        /// <value>
        ///     The command for toggling shift down.
        /// </value>
        public ICommand ToggleShiftDownCommand { get; set; }

        /// <summary>
        ///     Called when [property changed].
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}