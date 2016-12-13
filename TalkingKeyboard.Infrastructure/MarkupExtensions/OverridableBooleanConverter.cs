// --------------------------------------------------------------------------------------------------------------------
// <copyright file="OverridableBooleanConverter.cs" company="Numeral">
//   Copyright 2016 Fernando Ramírez Garibay
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace TalkingKeyboard.Infrastructure.MarkupExtensions
{
    using System;
    using System.Globalization;
    using System.Windows.Data;

    /// <summary>
    ///     The override-able boolean converter.
    /// </summary>
    internal class OverridableBooleanConverter : IMultiValueConverter
    {
        /// <summary>
        ///     Takes two boolean values, override-able and override-r and returns the override-able value unless overridden as
        ///     true.
        /// </summary>
        /// <param name="values">
        ///     The array of values that the source bindings in the
        ///     <see cref="T:System.Windows.Data.MultiBinding" /> produces. The value
        ///     <see cref="F:System.Windows.DependencyProperty.UnsetValue" /> indicates that the source binding has no value to
        ///     provide for conversion.
        ///     This converter expects exactly two boolean values.
        /// </param>
        /// <param name="targetType">The type of the binding target property.</param>
        /// <param name="parameter">The converter parameter to use.</param>
        /// <param name="culture">The culture to use in the converter.</param>
        /// <returns>
        ///     The override-able value unless it has been overridden as true.
        /// </returns>
        /// <exception cref="ArgumentException">Only two booleans are expected.</exception>
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values.Length != 2)
            {
                throw new ArgumentException("Only two booleans are expected.");
            }

            var overridable = (bool)values[0];
            var overrider = (bool)values[1];

            return overridable | (!overridable && overrider);
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}