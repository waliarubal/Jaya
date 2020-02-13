using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace Jaya.Ui.Views
{
    public class ManagePluginsView : UserControl
    {
        public ManagePluginsView()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
