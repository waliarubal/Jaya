using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace Jaya.Provider.Ftp.Views
{
    public class ConfigurationView : UserControl
    {
        public ConfigurationView()
        {
            this.InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
