using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Avalonia.ThemeManager;
using Jaya.Shared;
using Jaya.Ui.Services;
using Jaya.Ui.Views.Windows;

namespace Jaya.Ui
{
    public class App : Application
    {
        SharedService _shared;

        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
        }

        public override void OnFrameworkInitializationCompleted()
        {
            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime)
            {
                ThemeSelector = Avalonia.ThemeManager.ThemeSelector.Create("Themes");

                _shared = ServiceLocator.Instance.GetService<SharedService>();
                _shared.LoadConfigurations();

                Lifetime.Exit += OnExit;
                Lifetime.MainWindow = new MainView();
            }

            base.OnFrameworkInitializationCompleted();
        }

        void OnExit(object sender, ControlledApplicationLifetimeExitEventArgs e)
        {
            _shared.SaveConfigurations();
            Lifetime.Exit -= OnExit;
        }

        internal static IClassicDesktopStyleApplicationLifetime Lifetime => Current.ApplicationLifetime as IClassicDesktopStyleApplicationLifetime;

        internal static IThemeSelector ThemeSelector { get; private set; }
    }
}
