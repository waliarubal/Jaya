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

        public override void RegisterServices()
        {
            base.RegisterServices();
            ServiceLocator.Instance.GetService<SharedService>().LoadConfigurations();
        }

        public override void OnFrameworkInitializationCompleted()
        {
            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
                App.DesktopLifetime.MainWindow = new MainView();

            base.OnFrameworkInitializationCompleted();
        }

        internal static IClassicDesktopStyleApplicationLifetime DesktopLifetime => Current.ApplicationLifetime as IClassicDesktopStyleApplicationLifetime;

        //protected override void OnExiting(object sender, EventArgs e)
        //{
        //    ServiceLocator.Instance.GetService<SharedService>().SaveConfigurations();
        //    base.OnExiting(sender, e);
        //}
    }
}
