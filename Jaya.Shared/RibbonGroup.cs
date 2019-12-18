using Avalonia;
using Avalonia.Controls;
using Avalonia.Styling;
using System;

namespace Jaya.Shared
{
    public class RibbonGroup: ItemsControl, IStyleable
    {
        public static readonly StyledProperty<string> HeaderProperty;

        static RibbonGroup()
        {
            HeaderProperty = AvaloniaProperty.Register<RibbonGroup, string>(nameof(Header));
        }

        public string Header
        {
            get => GetValue(HeaderProperty);
            set => SetValue(HeaderProperty, value);
        }

        Type IStyleable.StyleKey => typeof(RibbonGroup);
    }
}
