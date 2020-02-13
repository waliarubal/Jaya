using Avalonia;
using Avalonia.Markup.Xaml;
using Jaya.Shared.Controls;

namespace Jaya.Ui.Views.Windows
{
    public class HostView : StyledWindow
    {

        public HostView()
        {
            this.InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif
            App.ThemeSelector.EnableThemes(this);
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
