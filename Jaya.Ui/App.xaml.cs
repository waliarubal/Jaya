using Avalonia;
using Avalonia.Markup.Xaml;
using Jaya.Shared.Contracts;
using Jaya.Shared.Services;
using Jaya.Shared.Services.Contracts;
using Jaya.Ui.Services;
using Microsoft.Extensions.DependencyInjection;
using Prise.Infrastructure.NetCore;
using System;
using System.IO;

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

            var serviceCollection = new ServiceCollection();

            // All services are singletons in a desktop application
            var configurationService = new ConfigurationService();
            var commandService = new CommandService();
            var memoryCacheService = new MemoryCacheService();
            var navigationService = new NavigationService(commandService);
            var sharedService = new SharedService(commandService, configurationService);

            ConfigureServices(serviceCollection, configurationService, commandService, memoryCacheService, navigationService, sharedService);

            // Adds the plugin system
            serviceCollection.AddPrise<IJayaPlugin>(options =>
                options
                    .WithPluginAssemblyName("Jaya.Provider.FileSystem.dll")
                    .WithRootPath(GetRootPath())
                    .ConfigureSharedServices(services =>
                    {
                        ConfigureServices(services, configurationService, commandService, memoryCacheService, navigationService, sharedService);
                    })
            );

            // Adds all the registered services to the ServiceLocator
            ServiceLocator.Create(serviceCollection.BuildServiceProvider());

            // Now, we have access to all services like before
            ServiceLocator.Instance.GetService<ISharedService>().LoadConfigurations();
        }

        private static void ConfigureServices(IServiceCollection serviceCollection, ConfigurationService configurationService, CommandService commandService, MemoryCacheService memoryCacheService, NavigationService navigationService, SharedService sharedService)
        {
            serviceCollection.AddSingleton<IConfigurationService>(configurationService);
            serviceCollection.AddSingleton<ICommandService>(commandService);
            serviceCollection.AddSingleton<IMemoryCacheService>(memoryCacheService);
            serviceCollection.AddSingleton<INavigationService>(navigationService);
            serviceCollection.AddSingleton<ISharedService>(sharedService);
            serviceCollection.AddSingleton<IPluginProvider, PluginProvider>();
        }

        private string GetRootPath()
        {
            string codeBase = typeof(App).Assembly.Location;
            UriBuilder uri = new UriBuilder(codeBase);
            string path = Uri.UnescapeDataString(uri.Path);
            return Path.GetDirectoryName(path);
        }

        protected override void OnExiting(object sender, EventArgs e)
        {
            ServiceLocator.Instance.GetService<ISharedService>().SaveConfigurations();
            base.OnExiting(sender, e);
        }
    }
}
