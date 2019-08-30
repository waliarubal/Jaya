using Avalonia.Data.Converters;
using System;
using System.Globalization;

namespace Jaya.Ui.Converters
{
    // logic taken from https://stackoverflow.com/questions/14488796/does-net-provide-an-easy-way-convert-bytes-to-kb-mb-gb-etc
    public class FileSizeToStringConverter : IValueConverter
    {
        readonly string[] _sizeSuffixes =
        {
            "bytes",
            "KB",
            "MB",
            "GB",
            "TB",
            "PB",
            "EB",
            "ZB",
            "YB"
        };

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var sizeInBytes = value as long?;
            return SizeSuffix(sizeInBytes ?? 0L, parameter == null ? 1 : int.Parse(parameter.ToString()));
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        string SizeSuffix(long value, int decimalPlaces = 1)
        {
            if (decimalPlaces < 0)
                throw new ArgumentOutOfRangeException(nameof(decimalPlaces));
            if (value < 0)
                return "-" + SizeSuffix(-value);
            if (value == 0)
                return string.Format("{0:n" + decimalPlaces + "} bytes", 0);

            // mag is 0 for bytes, 1 for KB, 2, for MB, etc.
            int mag = (int)Math.Log(value, 1024);

            // 1L << (mag * 10) == 2 ^ (10 * mag) 
            // [i.e. the number of bytes in the unit corresponding to mag]
            decimal adjustedSize = (decimal)value / (1L << (mag * 10));

            // make adjustment when the value is large enough that
            // it would round up to 1000 or more
            if (Math.Round(adjustedSize, decimalPlaces) >= 1000)
            {
                mag += 1;
                adjustedSize /= 1024;
            }

            return string.Format("{0:n" + decimalPlaces + "} {1}",
                adjustedSize,
                _sizeSuffixes[mag]);
        }
    }
}
