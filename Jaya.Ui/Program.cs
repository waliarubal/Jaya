using Avalonia;
using Avalonia.Logging.Serilog;
using Jaya.Ui.Views.Windows;

namespace Jaya.Ui
{
    public class Program
    {
        // This method is needed for IDE previewer infrastructure
        static AppBuilder BuildAvaloniaApp()
        {
            return AppBuilder.Configure<App>()
                           .UsePlatformDetect()
                           .UseReactiveUI()
                           .UseDataGrid()
                           .LogToDebug();
        }

        // The entry point. Things aren't ready yet, so at this point
        // you shouldn't use any Avalonia types or anything that expects
        // a SynchronizationContext to be ready
        static void Main(string[] args)
        {
            BuildAvaloniaApp().Start<MainView>();
        }
    }
}
