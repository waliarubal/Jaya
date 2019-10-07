using Avalonia;
using Avalonia.Markup.Xaml;

namespace Jaya.Ui.Views
{
    public class HostView : StyledWindow
    {
        public HostView()
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
