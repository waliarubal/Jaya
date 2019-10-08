using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace Jaya.Ui.Views.Providers
{
    public class FileSystemConfigurationView : UserControl
    {
        public FileSystemConfigurationView()
        {
            this.InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
