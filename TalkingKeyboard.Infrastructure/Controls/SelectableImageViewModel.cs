// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SelectableImageViewModel.cs" company="Numeral">
//   Copyright 2016 Fernando Ramírez Garibay
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace TalkingKeyboard.Infrastructure.Controls
{
    /// <summary>
    ///     The selectable image view model.
    /// </summary>
    public class SelectableImageViewModel : SelectableControlBase
    {
        private string source;

        /// <summary>
        ///     Initializes a new instance of the <see cref="SelectableImageViewModel" /> class.
        /// </summary>
        /// <param name="selectableControlModel">
        ///     The selectable control model.
        /// </param>
        public SelectableImageViewModel(ISelectableControlModel selectableControlModel)
            : base(selectableControlModel)
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="SelectableImageViewModel" /> class.
        /// </summary>
        public SelectableImageViewModel()
        {
        }

        /// <summary>
        ///     Gets or sets the image path.
        /// </summary>
        public string Source
        {
            get
            {
                return this.source;
            }

            set
            {
                if (value == this.source)
                {
                    return;
                }

                this.source = value;
                this.OnPropertyChanged();
            }
        }
    }
}