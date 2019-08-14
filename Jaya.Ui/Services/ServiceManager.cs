using Microsoft.Extensions.DependencyInjection;
using System;

namespace Jaya.Ui.Services
{
    sealed class ServiceManager: IDisposable
    {
        static ServiceManager _instance;
        static readonly object _syncRoot;
        IServiceScope _scope;

        static ServiceManager()
        {
            _syncRoot = new object();
        }

        private ServiceManager()
        {

        }

        ~ServiceManager()
        {
            Dispose();
        }

        #region properties

        public static ServiceManager Instance
        {
            get
            {
                lock(_syncRoot)
                {
                    if (_instance == null)
                        _instance = new ServiceManager();
                }

                return _instance;
            }
        }

        #endregion

        IServiceScope RegisterServices()
        {
            var collection = new ServiceCollection();
            collection.AddScoped<ConfigurationService>();
            collection.AddScoped<CommandService>();

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
    }
}
