// --------------------------------------------------------------------------------------------------------------------
// <copyright file="QwertySpanishMultiKeyboard.xaml.cs" company="Numeral">
//   Copyright 2016 Fernando Ramírez Garibay
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using TalkingKeyboard.Infrastructure.Models;

namespace TalkingKeyboard.Modules.MultiKeyBoard.Views
{
    /// <summary>
    ///     This class describes a Spanish keyboard with multiple-character keys arranged in the QWERTY layout. No logic is
    ///     handled in this class and can be fully edited in a designer-oriented program.
    /// </summary>
    public partial class T9SpanishMultiKeyboard
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="T9SpanishMultiKeyboard"/> class.
        /// </summary>
        /// <param name="viewModel">
        /// The view-model for multi-character keyboards.
        /// </param>
        public T9SpanishMultiKeyboard(IMultiKeyboardViewModel viewModel)
        {
            this.InitializeComponent();
            this.DataContext = viewModel;
        }
    }
}