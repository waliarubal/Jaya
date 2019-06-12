using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace Jaya.Ui.Views
{
    public class MainView : Window
    {
        public MainView()
        {
            InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif
        }

        void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
