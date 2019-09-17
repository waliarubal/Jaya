using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace Jaya.Ui.Views
{
    public class PluginsWindowView : Window
    {
        public PluginsWindowView()
        {
            this.InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
