using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace Jaya.Ui.Views
{
    public class RibbonView : UserControl
    {
        public static StyledProperty<bool> IsRibbonExpandedProperty;

        static RibbonView()
        {
            IsRibbonExpandedProperty = AvaloniaProperty.Register<RibbonView, bool>(nameof(IsRibbonExpanded), true);
        }

        public RibbonView()
        {
            this.InitializeComponent();
        }

        public bool IsRibbonExpanded
        {
            get => GetValue(IsRibbonExpandedProperty);
            set => SetValue(IsRibbonExpandedProperty, value);
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
