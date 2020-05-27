//
// Copyright (c) Rubal Walia. All rights reserved.
// Licensed under the 3-Clause BSD license. See LICENSE file in the project root for full license information.
//
using Avalonia.Data.Converters;
using Jaya.Shared;
using Jaya.Ui.Services;
using System;
using System.Globalization;

namespace Jaya.Ui.Converters
{
    public class BooleanToTreeNodeVisibilityConverter : IValueConverter
    {
        readonly SharedService _shared;

        public BooleanToTreeNodeVisibilityConverter()
        {
            _shared = ServiceLocator.Instance.GetService<SharedService>();
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
