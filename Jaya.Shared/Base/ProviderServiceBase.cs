using Jaya.Shared.Models;
using Jaya.Shared.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Jaya.Shared.Base
{
    public abstract class ProviderServiceBase : IProviderService
    {
        readonly MemoryCacheService _cache;

        protected ProviderServiceBase()
        {
            _cache = ServiceLocator.Instance.GetService<MemoryCacheService>();
            ConfigurationService = ServiceLocator.Instance.GetService<ConfigurationService>();
        }

        #region properties

        protected ConfigurationService ConfigurationService { get; }

        public abstract ConfigModelBase Configuration { get; }

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

        #endregion

        protected DirectoryModel GetFromCache(ProviderAccountModelBase account, string path)
        {
            var hash = account.GetHashCode();
            if (!string.IsNullOrEmpty(path))
                hash += path.GetHashCode();

            if (_cache.TryGetValue(hash, out DirectoryModel directory))
                return directory;

            return null;
        }

        protected void AddToCache(ProviderAccountModelBase account, DirectoryModel directory)
        {
            if (directory == null)
                throw new ArgumentNullException(nameof(directory));

            var hash = account.GetHashCode();
            if (!string.IsNullOrEmpty(directory.Path))
                hash += directory.Path.GetHashCode();

            _cache.Set(hash, directory);
        }

        protected T GetConfiguration<T>() where T : ConfigModelBase
        {
            return ConfigurationService.Get<T>();
        }

        protected void SetConfiguration<T>(T configuration) where T : ConfigModelBase
        {
            ConfigurationService.Set<T>(configuration);
        }

        public abstract Task<IEnumerable<ProviderAccountModelBase>> GetAccountsAsync();

        public abstract Task<DirectoryModel> GetDirectoryAsync(ProviderAccountModelBase account, string path = null);

        public abstract void SaveConfigurations();

        public override string ToString()
        {
            return Name;
        }

        public override int GetHashCode()
        {
            return Name.GetHashCode();
        }
    }
}
