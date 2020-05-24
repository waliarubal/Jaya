using Jaya.Shared.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Jaya.Shared
{
    public sealed class ServiceLocator : IDisposable
    {
        static ServiceLocator _instance;
        static readonly object _syncRoot;

        ServiceProvider _container;
        readonly Dictionary<string, IProviderService> _providersCache;
        readonly Dictionary<string, IService> _serviceCache;
        bool _isCacheInitialized;

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

        #endregion

        ServiceProvider RegisterServices()
        {
            var container = new ServiceCollection();

            var assemblies = new List<Assembly>(); 
           
            for (var index = 0; index < AppDomain.CurrentDomain.GetAssemblies().Length; index++)
            {
                var assembly = AppDomain.CurrentDomain.GetAssemblies()[index];
                if (assembly.FullName != null &&
                    assembly.FullName.StartsWith("Jaya.", StringComparison.InvariantCultureIgnoreCase))
                    if (!assembly.FullName.StartsWith("Jaya.Shared"))
                    {
                        assemblies.Add(assembly);
                    }
            }

            foreach (var fileName in Directory.GetFiles(Environment.CurrentDirectory, "Jaya.Provider.*.dll",
                SearchOption.TopDirectoryOnly))
            {
                var assembly = Assembly.LoadFrom(fileName);
                assemblies.Add(assembly);
            }
           
            // forces before the shared
            container.AddTransient<ICommandService, CommandService>();
            container.AddTransient<IMemoryCacheService, MemoryCacheService>();
            container.AddTransient<IConfigurationService, ConfigurationService>();
            container.AddTransient<IPlatformService, PlatformService>();
           
            
            foreach (var assembly in assemblies)
            {
                AddToContainer(container, assembly);
            }

            return container.BuildServiceProvider();
        }
        
        void UnregisterServices()
        {
            _container?.Dispose();
        }

        public void Dispose()
        {
            if (_isCacheInitialized)
            {
                _serviceCache.Clear();
                _providersCache.Clear();
                _isCacheInitialized = false;
            }

            UnregisterServices();
        }

        void InitializeCache()
        {
            if (_isCacheInitialized)
                return;

            var services = _container.GetServices<IService>();
            foreach (var service in services)
            {
                var serviceType = service.GetType();
                _serviceCache.Add(serviceType.Name, service);
            }

            var providers = _container.GetServices<IProviderService>();
            foreach (var provider in providers)
            {
                var providerType = provider.GetType();
                _providersCache.Add(providerType.Name, provider);
            }

            _isCacheInitialized = true;
        }

        public T GetService<T>() where T : class, IService
        {
            if (_container == null)
                _container = RegisterServices();
            InitializeCache();
            if (_serviceCache.TryGetValue(typeof(T).Name, out var service))
            {
                return (T) service;
            }
            return _container.GetService<T>();
        }

        public T GetProviderService<T>() where T : class, IProviderService
        {
            if (_container == null)
                _container = RegisterServices();
            InitializeCache();
            if (_providersCache.TryGetValue(typeof(T).Name, out var service))
            {
                return (T) service;
            }
            return _container.GetService<T>();

        }
        private void AddToContainer(ServiceCollection collection, Assembly assembly)
        {
            var serviceType = typeof(IService);
            var serviceProviderType = typeof(IServiceProvider);
            var serviceProviderContainer = typeof(IServiceProviderContainer);
            
            foreach (var type in assembly.GetExportedTypes())
            {
                if (!type.IsClass)
                    continue; 
                //collection.AddTransient(type);
               
                  if (serviceType.IsAssignableFrom(type))
                      collection.AddSingleton(serviceType, type);
                  else if (serviceProviderType.IsAssignableFrom(type))
                      collection.AddSingleton(serviceProviderType, type);
                  if (serviceProviderContainer.IsAssignableFrom(type))
                  {
                      collection.AddTransient(serviceProviderContainer, type);
                  }
                  else
                  {
                      collection.AddTransient(type);
                  }       
            }   
        }

    }
}
