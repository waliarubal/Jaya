using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace Jaya.Ui.Views
{
    public class AddressbarView : UserControl
    {
        public AddressbarView()
        {
            this.InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
