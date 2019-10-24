using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Jaya.Shared;
using Jaya.Ui.Services;
using Jaya.Ui.Views.Windows;

namespace Jaya.Ui
{
    public class App : Application
    {

        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
        }

        public override void OnFrameworkInitializationCompleted()
        {
            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime)
            {
                Lifetime.Exit += OnExit;
                ServiceLocator.Instance.GetService<SharedService>().LoadConfigurations();
                Lifetime.MainWindow = new MainView();
            }

            base.OnFrameworkInitializationCompleted();
        }

        void OnExit(object sender, ControlledApplicationLifetimeExitEventArgs e)
        {
            ServiceLocator.Instance.GetService<SharedService>().SaveConfigurations();
            Lifetime.Exit -= OnExit;
        }

        internal static IClassicDesktopStyleApplicationLifetime Lifetime => Current.ApplicationLifetime as IClassicDesktopStyleApplicationLifetime;
    }
}
