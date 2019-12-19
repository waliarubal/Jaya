using Avalonia;
using Avalonia.Controls;
using Avalonia.Styling;
using System;
using System.Collections;

namespace Jaya.Shared
{
    public class RibbonTab : TabItem, IStyleable
    {
        public static readonly DirectProperty<RibbonTab, IEnumerable> ItemsProperty;
        IEnumerable _items;

        static RibbonTab()
        {
            ItemsProperty = AvaloniaProperty.RegisterDirect<RibbonTab, IEnumerable>(nameof(Items), o => o.Items, (o, v) => o.Items = v);
        }

        public IEnumerable Items
        {
            get { return _items; }
            set { SetAndRaise(ItemsProperty, ref _items, value); }
        }

        Type IStyleable.StyleKey => typeof(RibbonTab);
    }
}
