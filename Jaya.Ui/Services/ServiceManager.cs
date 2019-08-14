using System;
using System.Collections.Generic;
using System.Reflection;

namespace Jaya.Ui.Services
{
    sealed class ServiceManager : IDisposable
    {
        static ServiceManager _instance;
        static object _syncRoot;
        readonly Dictionary<Type, ServiceBase> _services;

        static ServiceManager()
        {
            _syncRoot = new object();
        }

        private ServiceManager()
        {
            _services = new Dictionary<Type, ServiceBase>();
        }

        #region properties

        public static ServiceManager Instance
        {
            get
            {
                lock (_syncRoot)
                {
                    if (_instance == null)
                        _instance = new ServiceManager();

                    return _instance;
                }
            }
        }

        public bool IsRunning { get; private set; }

        #endregion

        public T Get<T>() where T : ServiceBase
        {
            var service = Get(typeof(T));
            return service as T;
        }

        public ServiceBase Get(Type serviceType)
        {
            if (!IsRunning)
                return null;

            ServiceBase service;
            if (_services.ContainsKey(serviceType))
                service = _services[serviceType];
            else
            {
                service = Activator.CreateInstance(serviceType) as ServiceBase;
                _services.Add(serviceType, service);
            }

            return service;
        }

        public void Dispose()
        {
            Stop();
        }

        public void Start()
        {
            if (IsRunning)
                return;

            Stop();

            var name_space = GetType().Namespace;
            var serviceBaseType = typeof(ServiceBase);
            var serviceAttributeType = typeof(Service);

            var types = Assembly.GetExecutingAssembly().GetTypes();
            foreach (var type in types)
            {
                type.GetCustomAttribute(typeof(Service));
                if (!type.Namespace.Equals(name_space) ||
                    !type.IsClass ||
                    type.IsAbstract ||
                    !serviceBaseType.IsAssignableFrom(type) || 
                    !type.IsDefined(serviceAttributeType))
                    continue;

                a type.GetCustomAttribute<Service>();

                var service = Activator.CreateInstance(type) as ServiceBase;
                if (_services.ContainsKey(type))
                    continue;

                _services.Add(type, service);
                service.Start();
            }

            IsRunning = true;
        }

        public void Stop()
        {
            if (!IsRunning || _services.Count == 0)
                return;

            foreach (var service in _services.Values)
                service.Stop();

            _services.Clear();
            IsRunning = false;
        }

    }
}
