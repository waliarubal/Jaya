using Avalonia.Controls;
using Avalonia.Styling;
using System;

namespace Jaya.Shared
{
    public class RibbonTab: TabItem, IStyleable
    {
        Type IStyleable.StyleKey => typeof(RibbonTab);
    }
}
