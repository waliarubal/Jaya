using Jaya.Shared.Services;
using System;
using System.Collections.Generic;
using System.Composition.Convention;
using System.Composition.Hosting;
using System.IO;
using System.Reflection;

namespace Jaya.Shared
{
    public sealed class ServiceLocator : IDisposable
    {
        static ServiceLocator _instance;
        static readonly object _syncRoot;

        CompositionHost _host;
        readonly Dictionary<string, IProviderService> _providersCache;
        bool _isProviderCacheInitialized;

        static ServiceLocator()
        {
            _syncRoot = new object();
        }

        private ServiceLocator()
        {
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
        CompositionHost RegisterServices()
        {
            var conventions = new ConventionBuilder();
            conventions.ForTypesDerivedFrom<IService>().Export<IService>();
            conventions.ForTypesDerivedFrom<IProviderService>().Export<IProviderService>();

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

            var configuration = new ContainerConfiguration().WithAssemblies(assemblies, conventions);
            return configuration.CreateContainer();
        }

        void UnregisterServices()
        {
            if (_host == null)
                return;

            _host.Dispose();
        }

        public void Dispose()
        {
            if (_isProviderCacheInitialized)
            {
                _providersCache.Clear();
                _isProviderCacheInitialized = false;
            }
                
            UnregisterServices();
        }

        void InitializeProvidersCache()
        {
            if (_isProviderCacheInitialized)
                return;

            var providers = _host.GetExports<IProviderService>();
            foreach (var provider in providers)
            {
                var providerType = provider.GetType();
                _providersCache.Add(providerType.Name, provider);
            }
            _isProviderCacheInitialized = true;
        }

        public T GetService<T>() where T : class, IService
        {
            if (_host == null)
                _host = RegisterServices();

            var type = typeof(T);
            return (T)_host.GetExport<IService>(type.Name);
        }

        public T GetProviderService<T>() where T : class, IProviderService
        {
            if (_host == null)
                _host = RegisterServices();

            InitializeProvidersCache();

            var type = typeof(T);
            return (T)_providersCache[type.Name];

        }
    }
}
