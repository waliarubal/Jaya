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
        readonly SharedService _shared;

        public App()
        {
            ThemeSelector = Avalonia.ThemeManager.ThemeSelector.Create("Themes");

            _shared = ServiceLocator.Instance.GetService<SharedService>();
        }

        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
   
            _shared.LoadConfigurations();
        }

        public override void OnFrameworkInitializationCompleted()
        {
            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime)
            {
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
