using Avalonia;
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
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
