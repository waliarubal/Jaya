using Avalonia;
using Avalonia.Controls;
using Avalonia.Styling;
using System;

namespace Jaya.Shared.Controls
{
    public class GroupBox: ContentControl, IStyleable
    {
        public static readonly StyledProperty<string> HeaderProperty;

        static GroupBox()
        {
            HeaderProperty = AvaloniaProperty.Register<GroupBox, string>(nameof(Header));
        }

        public string Header
        {
            get => GetValue(HeaderProperty);
            set => SetValue(HeaderProperty, value);
        }

        Type IStyleable.StyleKey => typeof(GroupBox);
    }
}
