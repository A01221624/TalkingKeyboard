using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;

namespace TalkingKeyboard.Infrastructure.MarkupExtensions
{
    /// <summary>
    ///     Markup extension which binds two possible strings and selects one based on a boolean.
    ///     Author: Thomas Levesque.
    /// </summary>
    /// <remarks>
    ///     Useful for Keyboards which change state based on other keys. Such as Shift ? Uppercase : Lowercase.
    ///     More information on: https://stackoverflow.com/questions/841808/wpf-display-a-bool-value-as-yes-no
    /// </remarks>
    public class SwitchBindingExtension : Binding
    {
        public SwitchBindingExtension()
        {
            Initialize();
        }

        public SwitchBindingExtension(string path)
            : base(path)
        {
            Initialize();
        }

        public SwitchBindingExtension(string path, object valueIfTrue, object valueIfFalse)
            : base(path)
        {
            Initialize();
            ValueIfTrue = valueIfTrue;
            ValueIfFalse = valueIfFalse;
        }

        public SwitchBindingExtension(RelativeSource relativeSource, string path, object valueIfTrue,
            object valueIfFalse)
            : base(path)
        {
            Initialize();
            RelativeSource = relativeSource;
            ValueIfTrue = valueIfTrue;
            ValueIfFalse = valueIfFalse;
        }

        [ConstructorArgument("valueIfTrue")]
        public object ValueIfTrue { get; set; }

        [ConstructorArgument("valueIfFalse")]
        public object ValueIfFalse { get; set; }

        private void Initialize()
        {
            ValueIfTrue = DoNothing;
            ValueIfFalse = DoNothing;
            Converter = new SwitchConverter(this);
        }

        private class SwitchConverter : IValueConverter
        {
            private readonly SwitchBindingExtension _switch;

            public SwitchConverter(SwitchBindingExtension switchExtension)
            {
                _switch = switchExtension;
            }

            #region IValueConverter Members

            public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
            {
                try
                {
                    var b = System.Convert.ToBoolean(value);
                    return b ? _switch.ValueIfTrue : _switch.ValueIfFalse;
                }
                catch
                {
                    return DependencyProperty.UnsetValue;
                }
            }

            public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            {
                return DoNothing;
            }

            #endregion
        }
    }
}