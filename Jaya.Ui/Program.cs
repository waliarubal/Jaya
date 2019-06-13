using Avalonia;
using Jaya.Ui.ViewModels;
using Jaya.Ui.Views;

namespace Jaya.Ui
{
    class Program
    {
        // Initialization code. Don't use any Avalonia, third-party APIs or any
        // SynchronizationContext-reliant code before AppMain is called: things aren't initialized
        // yet and stuff might break.
        public static void Main(string[] arguments)
        {
            BuildAvaloniaApp().Start(AppMain, arguments);
        }

        // Avalonia configuration, don't remove; also used by visual designer.
        static AppBuilder BuildAvaloniaApp()
        {
            return AppBuilder.Configure<App>()
                .UsePlatformDetect()
                .UseDataGrid()
                .UseReactiveUI();
        }

        // Your application's entry point. Here you can initialize your MVVM framework, DI
        // container, etc.
        private static void AppMain(Application app, string[] arguments)
        {
            var window = new MainView
            {
                DataContext = new MainViewModel(),
            };

            app.Run(window);
        }
    }
}
