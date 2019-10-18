using Avalonia;
using Avalonia.Markup.Xaml;
using Jaya.Shared;

namespace Jaya.Ui.Views.Windows
{
    public class MainView : StyledWindow
    {

        public MainView()
        {
            InitializeComponent();
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
