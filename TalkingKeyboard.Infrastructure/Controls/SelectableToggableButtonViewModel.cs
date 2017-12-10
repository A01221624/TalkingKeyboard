// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SelectableButtonViewModel.cs" company="Numeral">
//   Copyright 2016 Fernando Ramírez Garibay
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace TalkingKeyboard.Infrastructure.Controls
{
    /// <summary>
    ///     The selectable button view model.
    /// </summary>
    public class SelectableToggableButtonViewModel : SelectableControlBase
    {
        private string buttonText;
        private double fontSize = 40;

        /// <summary>
        ///     Initializes a new instance of the <see cref="SelectableButtonViewModel" /> class.
        /// </summary>
        /// <param name="selectableControlModel">
        ///     The selectable button model.
        /// </param>
        public SelectableToggableButtonViewModel(ISelectableControlModel selectableControlModel)
            : base(selectableControlModel)
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="SelectableButtonViewModel" /> class.
        /// </summary>
        public SelectableToggableButtonViewModel()
        {
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
    }
}