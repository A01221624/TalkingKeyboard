using System;
using System.Globalization;
using System.Windows.Data;

namespace TalkingKeyboard.Infrastructure.MarkupExtensions
{
    public class NullableVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value == null ? "Hidden" : "Visible";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new InvalidOperationException("NullableVisibilityConverter can only be used OneWay.");
        }
    }
}