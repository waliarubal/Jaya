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
            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                if (assembly.FullName.StartsWith("Jaya.", StringComparison.InvariantCultureIgnoreCase))
                    assemblies.Add(assembly);
            }

            foreach (var fileName in Directory.GetFiles(Environment.CurrentDirectory, "Jaya.Provider.*.dll", SearchOption.TopDirectoryOnly))
            {
                var assembly = Assembly.LoadFrom(fileName);
                assemblies.Add(assembly);
            }

            var serviceType = typeof(IService);
            var serviceProviderType = typeof(IServiceProvider);

            foreach (var assembly in assemblies)
            {
                foreach (var type in assembly.GetExportedTypes())
                {
                    if (!type.IsClass)
                        continue;

                    if (serviceType.IsAssignableFrom(type))
                        container.AddSingleton(serviceType, type);
                    else if (serviceProviderType.IsAssignableFrom(type))
                        container.AddSingleton(serviceProviderType, type);
                }
            }

            return container.BuildServiceProvider();
        }

        void UnregisterServices()
        {
            if (_container == null)
                return;

            _container.Dispose();
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

            var type = typeof(T);
            return (T)_serviceCache[type.Name];
        }

        public T GetProviderService<T>() where T : class, IProviderService
        {
            if (_container == null)
                _container = RegisterServices();

            InitializeCache();

            var type = typeof(T);
            return (T)_providersCache[type.Name];

        }
    }
}
