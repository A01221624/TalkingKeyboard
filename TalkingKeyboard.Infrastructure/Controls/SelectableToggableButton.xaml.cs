// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SelectableButton.xaml.cs" company="Numeral">
//   Copyright 2016 Fernando Ramírez Garibay
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace TalkingKeyboard.Infrastructure.Controls
{
    using System.Diagnostics.CodeAnalysis;
    using System.Windows;
    using System.Windows.Media.Animation;

    /// <summary>
    ///     Interaction logic for SelectableButton.xaml
    /// </summary>
    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1650:ElementDocumentationMustBeSpelledCorrectly",
         Justification = "XAML is a Microsoft term highly relevant to the project.")]
    public partial class SelectableToggableButton
    {
        /// <summary>
        ///     The font size property.
        /// </summary>
        public static new readonly DependencyProperty FontSizeProperty = DependencyProperty.Register(
            "FontSize",
            typeof(int),
            typeof(SelectableToggableButton),
            new PropertyMetadata(default(int)));

        /// <summary>
        ///     The text property.
        /// </summary>
        public static readonly DependencyProperty TextProperty = DependencyProperty.Register(
            "Text",
            typeof(string),
            typeof(SelectableToggableButton),
            new PropertyMetadata(default(string)));

        /// <summary>
        ///     Initializes a new instance of the <see cref="SelectableToggableButton" /> class.
        /// </summary>
        public SelectableToggableButton()
        {
            this.InitializeComponent();
            this.Animation = (Storyboard)this.Resources["ArcSelectionAnimation"];
        }

        /// <summary>
        ///     Gets or sets the font size of the text on the button.
        /// </summary>
        public new int FontSize
        {
            get
            {
                return (int)this.GetValue(FontSizeProperty);
            }

            set
            {
                this.SetValue(FontSizeProperty, value);
            }
        }

        /// <summary>
        ///     Gets or sets the text on the button.
        /// </summary>
        /// <value>
        ///     The text on the button.
        /// </value>
        public string Text
        {
            get
            {
                return (string)this.GetValue(TextProperty);
            }

            set
            {
                this.SetValue(TextProperty, value);
            }
        }
    }
}