using Avalonia;
using Avalonia.Markup.Xaml;
using Jaya.Shared.Contracts;
using Jaya.Shared.Services;
using Jaya.Ui.Services;
using Microsoft.Extensions.DependencyInjection;
using Prise.Infrastructure.NetCore;
using System;

namespace Jaya.Ui
{
    public class App : Application
    {
        IServiceProvider mainServiceProvider;

        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
        }

        public override void RegisterServices()
        {
            base.RegisterServices();

            var serviceCollection = new ServiceCollection();

            serviceCollection.AddSingleton<IConfigurationService, ConfigurationService>();
            serviceCollection.AddSingleton<ICommandService, CommandService>();
            serviceCollection.AddSingleton<INavigationService, NavigationService>();
            serviceCollection.AddSingleton<ISharedService, SharedService>();

            serviceCollection.AddPrise<IProviderService>(options =>
                options.WithPluginAssemblyName("Jaya.Provider.FileSystem.dll")
            );

            // Adds all the registered services to the ServiceLocator
            ServiceLocator.Create(serviceCollection.BuildServiceProvider());

            // Now, we have access to all services like before
            ServiceLocator.Instance.GetService<ISharedService>().LoadConfigurations();
        }

        protected override void OnExiting(object sender, EventArgs e)
        {
            ServiceLocator.Instance.GetService<ISharedService>().SaveConfigurations();
            base.OnExiting(sender, e);
        }
    }
}
