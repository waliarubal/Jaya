using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Composition;
using System.Composition.Hosting;
using System.IO;
using System.Reflection;

namespace Jaya.Shared.Services
{
    public sealed class ServiceLocator : IDisposable
    {
        static ServiceLocator _instance;
        static readonly object _syncRoot;

        static ServiceLocator()
        {
            _syncRoot = new object();
        }

        private ServiceLocator()
        {
            Plugins = RegisterServices();
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
        public IEnumerable<IService> Plugins { get; private set; }

        #endregion

        IEnumerable<IService> RegisterServices()
        {
            var configuration = new ContainerConfiguration()
                .WithAssemblies(GetAssemblies());

            using(var container = configuration.CreateContainer())
            {
                return container.GetExports<IService>();
            }

            //var collection = new ServiceCollection();

            //var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            //foreach (var assembly in assemblies)
            //{
            //    if (!assembly.FullName.StartsWith("Jaya", StringComparison.InvariantCultureIgnoreCase))
            //        continue;

            //    var types = assembly.DefinedTypes;
            //    foreach (TypeInfo typeInfo in types)
            //        if (typeInfo.IsClass && typeInfo.Name.EndsWith("Service", StringComparison.InvariantCulture))
            //            collection.AddScoped(typeInfo);
            //}

            //var container = collection.BuildServiceProvider();
            //var scopeFactory = container.GetRequiredService<IServiceScopeFactory>();
            //return scopeFactory.CreateScope();
        }

        IEnumerable<Assembly> GetAssemblies()
        {
            var assemblies = new List<Assembly>();

            var files = Directory.GetFiles(Environment.CurrentDirectory, "Jaya.*.dll");
            foreach(var file in files)
                assemblies.Add(Assembly.LoadFile(file));

            return assemblies;
        }

        void UnregisterServices(IServiceScope scope)
        {
            if (scope == null)
                return;

            scope.Dispose();
        }

        public void Dispose()
        {
            Plugins = null;
        }

        public T GetService<T>()
        {
            if (Plugins == null)
                Plugins = RegisterServices();

            return default;
        }

        public object CreateInstance(Type type)
        {
            return null;
        }

        public T CreateInstance<T>()
        {
            return default;
        }
    }
}
