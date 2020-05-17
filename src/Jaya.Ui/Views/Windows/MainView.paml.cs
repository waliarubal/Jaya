using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Jaya.Shared.Controls;

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
            App.ThemeSelector.EnableThemes(this);
            DoubleTapped += MainView_DoubleTapped;
        }
        
        private void MainView_DoubleTapped(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState == WindowState.Maximized
                ? WindowState.Normal
                : WindowState.Maximized;
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
