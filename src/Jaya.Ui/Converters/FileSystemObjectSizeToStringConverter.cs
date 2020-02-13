using Avalonia.Data.Converters;
using Jaya.Shared.Models;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace Jaya.Ui.Converters
{
    public class FileSystemObjectSizeToStringConverter : IMultiValueConverter
    {

        public object Convert(IList<object> values, Type targetType, object parameter, CultureInfo culture)
        {
            var type = (FileSystemObjectType)values[1];
            if (type == FileSystemObjectType.Directory)
                return string.Empty;

            var sizeInBytes = values[0] as long?;
            return FileSystemObjectModel.SizeSuffix(sizeInBytes ?? 0L, 2);
        }
    }
}
