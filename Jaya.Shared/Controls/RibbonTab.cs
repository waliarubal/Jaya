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
