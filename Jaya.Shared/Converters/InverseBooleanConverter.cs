using Avalonia.Data.Converters;
using System;
using System.Globalization;

namespace Jaya.Shared.Converters
{
    public class InverseBooleanConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                throw new ArgumentNullException(nameof(value));

            if (value is bool && targetType == typeof(bool))
                return !(bool)value;

            throw new ArgumentException("Passed value is not boolean.", nameof(value));
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return !(bool)Convert(value, targetType, parameter, culture);
        }
    }
}
