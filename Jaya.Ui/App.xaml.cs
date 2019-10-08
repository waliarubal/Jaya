using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Jaya.Ui.Views.Windows;

namespace Jaya.Ui
{
    public class App : Application
    {
        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
        }

        internal static IClassicDesktopStyleApplicationLifetime Lifetime { get; private set; }

        public override void OnFrameworkInitializationCompleted()
        {
            Lifetime = ApplicationLifetime as IClassicDesktopStyleApplicationLifetime;
            if (Lifetime != null)
                Lifetime.MainWindow = new MainView();

            base.OnFrameworkInitializationCompleted();
        }
    }
}
