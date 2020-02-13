using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace Jaya.Ui.Views
{
    public class DetailsView : UserControl
    {
        public DetailsView()
        {
            this.InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
