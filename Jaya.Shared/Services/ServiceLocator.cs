using System;
using System.Collections.Generic;
using System.Composition;
using System.Composition.Convention;
using System.Composition.Hosting;
using System.IO;
using System.Reflection;

namespace Jaya.Shared.Services
{
    public sealed class ServiceLocator : IDisposable
    {
        static ServiceLocator _instance;
        static readonly object _syncRoot;
        CompositionHost _host;

        static ServiceLocator()
        {
            _syncRoot = new object();
        }

        private ServiceLocator()
        {
            
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

        [ImportMany]
        public IEnumerable<IService> Services { get; private set; }

        [ImportMany]
        public IEnumerable<IPorviderService> Providers { get; private set; }

        #endregion

        CompositionHost RegisterServices()
        {
            var conventions = new ConventionBuilder();
            conventions.ForTypesDerivedFrom<IService>().Export<IService>();
            conventions.ForTypesDerivedFrom<IServiceProvider>().Export<IServiceProvider>();

            var assemblies = new List<Assembly>();
            foreach(var fileName in Directory.GetFiles(Environment.CurrentDirectory, "Jaya.*.dll", SearchOption.TopDirectoryOnly))
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
            UnregisterServices();
        }

        public T GetService<T>() where T: IService
        {
            if (_host == null)
            {
                _host = RegisterServices();
                Services = _host.GetExports<IService>();
                Providers = _host.GetExports<IPorviderService>();
            }
                
            return _host.GetExport<T>();
        }

        public T GetProvider<T>() where T : IPorviderService
        {
            if (_host == null)
            {
                _host = RegisterServices();
                Services = _host.GetExports<IService>();
                Providers = _host.GetExports<IPorviderService>();
            }

            return _host.GetExport<T>();
        }

        /*
        public object CreateInstance(Type type)
        {
            return null;
        }

        public T CreateInstance<T>()
        {
            return default;
        }
        */
    }
}
