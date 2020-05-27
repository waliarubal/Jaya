//
// Copyright (c) Rubal Walia. All rights reserved.
// Licensed under the 3-Clause BSD license. See LICENSE file in the project root for full license information.
//
using Avalonia.Controls;
using Avalonia.Styling;
using System;

namespace Jaya.Shared.Controls
{
    public class RibbonTab : TabItem, IStyleable
    {
        Type IStyleable.StyleKey => typeof(RibbonTab);
    }
}
