using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;
using System.Reflection;
using System.Runtime.Loader;

namespace Jaya.Shared.Services
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
            var pluginFiles = Directory.GetFiles(Environment.CurrentDirectory, "Jaya.Provider.*.dll");
            foreach (var filePath in pluginFiles)
                AssemblyLoadContext.Default.LoadFromAssemblyPath(filePath);

            var collection = new ServiceCollection();

            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            foreach (var assembly in assemblies)
            {
                if (!assembly.FullName.StartsWith("Jaya", StringComparison.InvariantCultureIgnoreCase))
                    continue;

                var types = assembly.DefinedTypes;
                foreach (TypeInfo typeInfo in types)
                    if (typeInfo.IsClass && typeInfo.Name.EndsWith("Service", StringComparison.InvariantCulture))
                        collection.AddScoped(typeInfo);
            }

            //domain.AssemblyResolve -= OnAssemblyResolve;

            var container = collection.BuildServiceProvider();
            var scopeFactory = container.GetRequiredService<IServiceScopeFactory>();
            return scopeFactory.CreateScope();
        }

        //Assembly OnAssemblyResolve(object sender, ResolveEventArgs args)
        //{
        //    var uri = new UriBuilder(args.RequestingAssembly.CodeBase);
        //    var directory = Path.GetDirectoryName(Uri.UnescapeDataString(uri.Path));
        //    var assemblyPath = Path.Combine(directory, string.Format("{0}.dll", args.Name.Split(',')[0]));

        //    return Assembly.Load(File.ReadAllBytes(assemblyPath));
        //}

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
