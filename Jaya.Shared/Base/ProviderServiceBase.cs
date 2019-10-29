using Jaya.Shared.Models;
using Jaya.Shared.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Jaya.Shared.Base
{
    public abstract class ProviderServiceBase: IProviderService
    {
        readonly MemoryCacheService _cache;

        protected ProviderServiceBase()
        {
            _cache = ServiceLocator.Instance.GetService<MemoryCacheService>();
            ConfigurationService = ServiceLocator.Instance.GetService<ConfigurationService>();
        }

        #region properties

        protected ConfigurationService ConfigurationService { get; }

        public bool IsRootDrive
        {
            get;
            protected set;
        }

        public string Name
        {
            get;
            protected set;
        }

        public string Description
        {
            get;
            protected set;
        }

        public string ImagePath
        {
            get;
            protected set;
        }

        public Type ConfigurationEditorType
        {
            get;
            protected set;
        }

        protected ConfigModelBase Configuration { get; set; }

        #endregion

        protected DirectoryModel GetFromCache(ProviderModel provider, string path)
        {
            var hash = provider.GetHashCode();
            if (!string.IsNullOrEmpty(path))
                hash += path.GetHashCode();

            if (_cache.TryGetValue(hash, out DirectoryModel directory))
                return directory;

            return null;
        }

        protected void AddToCache(ProviderModel provider, DirectoryModel directory)
        {
            if (directory == null)
                throw new ArgumentNullException(nameof(directory));

            var hash = provider.GetHashCode();
            if (!string.IsNullOrEmpty(directory.Path))
                hash += directory.Path.GetHashCode();

            _cache.Set(hash, directory);
        }

        public T GetConfigurtion<T>() where T : ConfigModelBase
        {
            return (T)Configuration;
        }

        public virtual async Task<IEnumerable<ProviderModel>> GetProvidersAsync()
        {
            return await Task.Run(() => GetProviders());
        }

        public virtual async Task<DirectoryModel> GetDirectoryAsync(ProviderModel provider, string path = null)
        {
            return await Task.Run(() => GetDirectory(provider, path));
        }

        protected abstract IEnumerable<ProviderModel> GetProviders();

        protected abstract DirectoryModel GetDirectory(ProviderModel provider, string path = null);

        public override string ToString()
        {
            return Name;
        }

        public override int GetHashCode()
        {
            return Name.GetHashCode();
        }

        public T GetConfiguration<T>() where T : ConfigModelBase
        {
            throw new NotImplementedException();
        }
    }
}
