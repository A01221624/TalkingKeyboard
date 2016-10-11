// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SwitchBindingExtension.cs" company="Numeral">
//   Copyright 2016 Fernando Ramírez Garibay
// </copyright>
// <summary>
//   Markup extension which binds two possible strings and selects one based on a boolean.
//   Author: Thomas Levesque.
// </summary>
// <remarks>
//   Useful for Keyboards which change state based on other keys. Such as Shift ? Uppercase : Lowercase.
//   More information on: https://stackoverflow.com/questions/841808/wpf-display-a-bool-value-as-yes-no
// </remarks>
// --------------------------------------------------------------------------------------------------------------------
namespace TalkingKeyboard.Infrastructure.MarkupExtensions
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Globalization;
    using System.Windows;
    using System.Windows.Data;
    using System.Windows.Markup;

    /// <summary>
    ///     This class contains a markup extension which binds two possible strings and selects one based on a boolean.
    ///     Author: Thomas Levesque.
    /// </summary>
    /// <seealso cref="System.Windows.Data.Binding" />
    /// <remarks>
    ///     Useful for Keyboards which change state based on other keys. Such as Shift ? Uppercase : Lowercase.
    ///     More information on: https://stackoverflow.com/questions/841808/wpf-display-a-bool-value-as-yes-no
    /// </remarks>
    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1650:ElementDocumentationMustBeSpelledCorrectly",
         Justification = "Web addresses exempt of spell-check.")]
    public class SwitchBindingExtension : Binding
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="SwitchBindingExtension" /> class.
        /// </summary>
        public SwitchBindingExtension()
        {
            this.Initialize();
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="SwitchBindingExtension" /> class.
        /// </summary>
        /// <param name="path">The initial <see cref="P:System.Windows.Data.Binding.Path" /> for the binding.</param>
        public SwitchBindingExtension(string path)
            : base(path)
        {
            this.Initialize();
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="SwitchBindingExtension" /> class.
        /// </summary>
        /// <param name="path">The binding path.</param>
        /// <param name="valueIfTrue">The value if true.</param>
        /// <param name="valueIfFalse">The value if false.</param>
        public SwitchBindingExtension(string path, object valueIfTrue, object valueIfFalse)
            : base(path)
        {
            this.Initialize();
            this.ValueIfTrue = valueIfTrue;
            this.ValueIfFalse = valueIfFalse;
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="SwitchBindingExtension" /> class.
        /// </summary>
        /// <param name="relativeSource">The relative source.</param>
        /// <param name="path">The binding path.</param>
        /// <param name="valueIfTrue">The value if true.</param>
        /// <param name="valueIfFalse">The value if false.</param>
        public SwitchBindingExtension(
            RelativeSource relativeSource,
            string path,
            object valueIfTrue,
            object valueIfFalse)
            : base(path)
        {
            this.Initialize();
            this.RelativeSource = relativeSource;
            this.ValueIfTrue = valueIfTrue;
            this.ValueIfFalse = valueIfFalse;
        }

        /// <summary>
        ///     Gets or sets the value if the bound object false.
        /// </summary>
        /// <value>
        ///     The value if false.
        /// </value>
        [ConstructorArgument("valueIfFalse")]
        public object ValueIfFalse { get; set; }

        /// <summary>
        ///     Gets or sets the value if the bound object true.
        /// </summary>
        /// <value>
        ///     The value if true.
        /// </value>
        [ConstructorArgument("valueIfTrue")]
        public object ValueIfTrue { get; set; }

        /// <summary>
        ///     Initializes this instance.
        /// </summary>
        private void Initialize()
        {
            this.ValueIfTrue = Binding.DoNothing;
            this.ValueIfFalse = Binding.DoNothing;
            this.Converter = new SwitchConverter(this);
        }

        /// <summary>
        ///     IValueConverter implementation for converting between boolean and any object.
        /// </summary>
        /// <remarks>
        ///     Does the footwork for selecting an object instance depending on a boolean value.
        /// </remarks>
        /// <seealso cref="System.Windows.Data.IValueConverter" />
        private class SwitchConverter : IValueConverter
        {
            private readonly SwitchBindingExtension _switch;

            /// <summary>
            ///     Initializes a new instance of the <see cref="SwitchConverter" /> class.
            /// </summary>
            /// <param name="switchExtension">The switch extension.</param>
            public SwitchConverter(SwitchBindingExtension switchExtension)
            {
                this._switch = switchExtension;
            }

            /// <summary>
            ///     Maps the boolean value (true/false) to one of two possible object instances.
            /// </summary>
            /// <param name="value">The value produced by the binding source.</param>
            /// <param name="targetType">The type of the binding target property.</param>
            /// <param name="parameter">The converter parameter to use.</param>
            /// <param name="culture">The culture to use in the converter.</param>
            /// <returns>
            ///     <see cref="SwitchBindingExtension.ValueIfTrue" /> if the boolean (<see cref="value" />) is true or
            ///     <see cref="SwitchBindingExtension.ValueIfFalse" /> if false.
            /// </returns>
            public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
            {
                try
                {
                    var b = System.Convert.ToBoolean(value);
                    return b ? this._switch.ValueIfTrue : this._switch.ValueIfFalse;
                }
                catch
                {
                    return DependencyProperty.UnsetValue;
                }
            }

            /// <summary>
            ///     Converts the object instance back to boolean.
            /// </summary>
            /// <param name="value">The value that is produced by the binding target.</param>
            /// <param name="targetType">The type to convert to.</param>
            /// <param name="parameter">The converter parameter to use.</param>
            /// <param name="culture">The culture to use in the converter.</param>
            /// <returns>
            ///     Current implementation does Nothing.
            /// </returns>
            /// <remarks>
            ///     Current scope has no use for this.
            /// </remarks>
            public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            {
                return DoNothing;
            }
        }
    }
}