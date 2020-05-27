//
// Copyright (c) Rubal Walia. All rights reserved.
// Licensed under the 3-Clause BSD license. See LICENSE file in the project root for full license information.
//
using Avalonia.Data.Converters;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace Jaya.Shared.Converters
{
    public class MultiValueConverter : IMultiValueConverter
    {
        public object Convert(IList<object> values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values == null)
                return null;

            return new List<object>(values);
        }
    }
}
