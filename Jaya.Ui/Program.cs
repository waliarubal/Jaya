using Avalonia;
using Avalonia.Logging.Serilog;
using Jaya.Ui.ViewModels;
using Jaya.Ui.Views;

namespace Jaya.Ui
{
    class Program
    {
        // Initialization code. Don't use any Avalonia, third-party APIs or any
        // SynchronizationContext-reliant code before AppMain is called: things aren't initialized
        // yet and stuff might break.
        static void Main(string[] args)
        {
            BuildAvaloniaApp().Start(AppMain, args);
        }

        // Avalonia configuration, don't remove; also used by visual designer.
        static AppBuilder BuildAvaloniaApp()
        {
            return AppBuilder.Configure<App>()
                           .UsePlatformDetect()
                           .LogToDebug()
                           .UseReactiveUI();
        }

        // Your application's entry point. Here you can initialize your MVVM framework, DI
        // container, etc.
        static void AppMain(Application app, string[] args)
        {
            var window = new MainWindowView
            {
                DataContext = new MainWindowViewModel(),
            };

            app.Run(window);
        }
    }
}
