// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NumericKeyboardViewModel.cs" company="Numeral">
//   Copyright 2016 Fernando Ramírez Garibay
// </copyright>
// <summary>
//   Defines the NumericKeyboardViewModel type.
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

    /// <summary>
    ///     This class contains the state and logic relevant to the Spanish QWERTY single-character keyboard.This logic
    ///     currently pertains the state of the shift button which enables toggling keys between one character and another,
    ///     such as lowercase and uppercase.
    /// </summary>
    /// <seealso cref="System.ComponentModel.INotifyPropertyChanged" />
    public class NumericKeyboardViewModel : INotifyPropertyChanged
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="NumericKeyboardViewModel" /> class.
        /// </summary>
        public NumericKeyboardViewModel()
        {
        }

        /// <summary>
        ///     Occurs when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

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