using Jaya.Shared.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace Jaya.Shared
{
    public sealed class ServiceLocator : IDisposable
    {
        static ServiceLocator _instance;
        static readonly object _syncRoot;
        readonly Dictionary<string, IProviderService> _providersCache;
        readonly Dictionary<string, IService> _serviceCache;

        static ServiceLocator()
        {
            _syncRoot = new object();
        }

        private ServiceLocator()
        {
            _serviceCache = new Dictionary<string, IService>();
            _providersCache = new Dictionary<string, IProviderService>();
        }

        ~ServiceLocator()
        {
            Dispose();
        }

        #region properties

        public static ServiceLocator Instance
        {
            get
            {
                lock (_syncRoot)
                {
                    if (_instance == null)
                        _instance = new ServiceLocator();
                }

                return _instance;
            }
        }

        internal ServiceProvider Container { get; private set; }

        internal bool IsCacheInitialized { get; private set; }

        #endregion

        ServiceProvider RegisterServices()
        {
            var container = new ServiceCollection();

            // add shared services in the beginning
            container.AddSingleton<ICommandService, CommandService>();
            container.AddSingleton<IMemoryCacheService, MemoryCacheService>();
            container.AddSingleton<IConfigurationService, ConfigurationService>();
            container.AddSingleton<IPlatformService, PlatformService>();

            var assemblies = new List<Assembly>();

            var currentDomainAssemblies = AppDomain.CurrentDomain.GetAssemblies();
            foreach (var assembly in currentDomainAssemblies)
            {
                if (assembly.FullName.StartsWith("Jaya.", StringComparison.InvariantCultureIgnoreCase) &&
                    !assembly.FullName.StartsWith("Jaya.Shared", StringComparison.InvariantCultureIgnoreCase))
                    assemblies.Add(assembly);
            }

            foreach (var fileName in Directory.GetFiles(Environment.CurrentDirectory, "Jaya.Provider.*.dll", SearchOption.TopDirectoryOnly))
            {
                var assembly = Assembly.LoadFrom(fileName);
                assemblies.Add(assembly);
            }

            foreach (var assembly in assemblies)
                AddToContainer(container, assembly);

            return container.BuildServiceProvider();
        }

        void UnregisterServices()
        {
            Container?.Dispose();
        }

        public void Dispose()
        {
            if (IsCacheInitialized)
            {
                _serviceCache.Clear();
                _providersCache.Clear();
                IsCacheInitialized = false;
            }

            UnregisterServices();
        }

        void AddToContainer(ServiceCollection collection, Assembly assembly)
        {
            var serviceType = typeof(IService);
            var serviceProviderType = typeof(IServiceProvider);

            foreach (var type in assembly.GetExportedTypes())
            {
                if (!type.IsClass)
                    continue;

                if (serviceType.IsAssignableFrom(type))
                    collection.AddSingleton(serviceType, type);
                else if (serviceProviderType.IsAssignableFrom(type))
                    collection.AddSingleton(serviceProviderType, type);
            }
        }

        bool InitializeCache()
        {
            if (IsCacheInitialized)
                return true;

            var services = Container.GetServices<IService>();
            foreach (var service in services)
            {
                var serviceType = service.GetType();
                _serviceCache.Add(serviceType.Name, service);
            }

            var providers = Container.GetServices<IProviderService>();
            foreach (var provider in providers)
            {
                var providerType = provider.GetType();
                _providersCache.Add(providerType.Name, provider);
            }

            return true;
        }

        public T GetService<T>() where T : class, IService
        {
            if (Container == null)
            {
                Container = RegisterServices();
                IsCacheInitialized = InitializeCache();
            }

            if (_serviceCache.TryGetValue(typeof(T).Name, out var service))
                return (T)service;

            return Container.GetService<T>();
        }

        public T GetProviderService<T>() where T : class, IProviderService
        {
            if (Container == null)
            {
                Container = RegisterServices();
                IsCacheInitialized = InitializeCache();
            }

            if (_providersCache.TryGetValue(typeof(T).Name, out var service))
                return (T)service;

            return Container.GetService<T>();

        }

    }
}
