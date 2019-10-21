using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;

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
            Services = RegisterServices();
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

        public IEnumerable<IService> Services { get; private set; }

        public IEnumerable<IPorviderService> Providers { get; private set; }

        #endregion

        IEnumerable<IService> RegisterServices()
        {
            return null;

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

        void UnregisterServices(IServiceScope scope)
        {
            if (scope == null)
                return;

            scope.Dispose();
        }

        public void Dispose()
        {
            Services = null;
        }

        public T GetService<T>()
        {
            if (Services == null)
                Services = RegisterServices();

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
