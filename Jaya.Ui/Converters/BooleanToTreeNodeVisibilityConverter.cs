using Avalonia.Data.Converters;
using Jaya.Shared.Services;
using Jaya.Ui.Services;
using System;
using System.Globalization;

namespace Jaya.Ui.Converters
{
    public class BooleanToTreeNodeVisibilityConverter : IValueConverter
    {
        readonly ISharedService _shared;

        public BooleanToTreeNodeVisibilityConverter()
        {
            _shared = ServiceLocator.Instance.GetService<ISharedService>();
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (_shared.ApplicationConfiguration.IsHiddenItemVisible)
                return true;

            return !(bool)value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
