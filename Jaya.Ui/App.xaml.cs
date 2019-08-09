using Avalonia;
using Avalonia.Markup.Xaml;

namespace Jaya.Ui
{
    public class App : Application
    {
        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
