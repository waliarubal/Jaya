using Avalonia;
using Avalonia.Controls;
using Avalonia.Styling;
using System;

namespace Jaya.Shared.Controls
{
    public class RibbonGroup: ItemsControl, IStyleable
    {
        public static readonly DirectProperty<RibbonGroup, string> HeaderProperty;
        string _header;

        static RibbonGroup()
        {
            HeaderProperty = AvaloniaProperty.RegisterDirect<RibbonGroup, string>(nameof(Header), o => o.Header, (o, v) => o.Header = v);
        }

        public string Header
        {
            get => _header;
            set => SetAndRaise(HeaderProperty, ref _header, value);
        }

        Type IStyleable.StyleKey => typeof(RibbonGroup);
    }
}
