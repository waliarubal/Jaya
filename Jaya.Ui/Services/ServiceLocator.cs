using Microsoft.Extensions.DependencyInjection;
using System;
using System.Reflection;

namespace Jaya.Ui.Services
{
    public sealed class ServiceLocator : IDisposable
    {
        static ServiceLocator _instance;
        static readonly object _syncRoot;
        IServiceScope _scope;

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

        public IServiceProvider Provider
        {
            get
            {
                if (_scope == null)
                    _scope = RegisterServices();

                return _scope.ServiceProvider;
            }
        }

        #endregion

        IServiceScope RegisterServices()
        {
            var collection = new ServiceCollection();

            var types = Assembly.GetExecutingAssembly().DefinedTypes;
            foreach (TypeInfo typeInfo in types)
                if (typeInfo.IsClass && typeInfo.Name.EndsWith("Service", StringComparison.InvariantCulture))
                    collection.AddScoped(typeInfo);

            var container = collection.BuildServiceProvider();
            var scopeFactory = container.GetRequiredService<IServiceScopeFactory>();
            return scopeFactory.CreateScope();
        }

        void UnregisterServices(IServiceScope scope)
        {
            if (scope == null)
                return;

            scope.Dispose();
        }

        public void Dispose()
        {
            UnregisterServices(_scope);
        }

        public T GetService<T>()
        {
            if (_scope == null)
                _scope = RegisterServices();

            return _scope.ServiceProvider.GetService<T>();
        }

        public object CreateInstance(Type type)
        {
            return ActivatorUtilities.CreateInstance(Provider, type);
        }

        public T CreateInstance<T>()
        {
            return ActivatorUtilities.CreateInstance<T>(Provider, typeof(T));
        }
    }
}
