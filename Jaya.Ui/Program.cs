using Avalonia;
using Avalonia.Logging.Serilog;
using Jaya.Ui.Services;
using Jaya.Ui.Views;
using System;

namespace Jaya.Ui
{
    class Program
    {
        // This method is needed for IDE previewer infrastructure
        static AppBuilder BuildAvaloniaApp()
        {
            return AppBuilder.Configure<App>()
                           .UsePlatformDetect()
                           .UseReactiveUI()
                           .LogToDebug();
        }

        // The entry point. Things aren't ready yet, so at this point
        // you shouldn't use any Avalonia types or anything that expects
        // a SynchronizationContext to be ready
        static void Main(string[] args)
        {
            BuildAvaloniaApp().Start(AppMain, args);
        }

        // Application entry point. Avalonia is completely initialized.
        static void AppMain(Application app, string[] args)
        {
            ServiceManager.Instance.Start();

            Application.Current.OnExit += App_OnExit;
            Application.Current.RunWithMainWindow<MainWindowView>();
        }

        static void App_OnExit(object sender, EventArgs e)
        {
            Application.Current.OnExit -= App_OnExit;

            ServiceManager.Instance.Stop();
        }
    }
}
