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

        protected ProviderServiceBase(IService configService)
        {
            _cache = ServiceLocator.Instance.GetService<MemoryCacheService>();
            ConfigurationService = configService as ConfigurationService;
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

        #endregion

        protected DirectoryModel GetFromCache(AccountModelBase account, string path)
        {
            var hash = account.GetHashCode();
            if (!string.IsNullOrEmpty(path))
                hash += path.GetHashCode();

            if (_cache.TryGetValue(hash, out DirectoryModel directory))
                return directory;

            return null;
        }

        protected void AddToCache(AccountModelBase account, DirectoryModel directory)
        {
            if (directory == null)
                throw new ArgumentNullException(nameof(directory));

            var hash = account.GetHashCode();
            if (!string.IsNullOrEmpty(directory.Path))
                hash += directory.Path.GetHashCode();

            _cache.Set(hash, directory);
        }

        public T GetConfiguration<T>() where T : ConfigModelBase
        {
            return ConfigurationService.GetOrDefault<T>(Name);
        }

        protected void SetConfiguration<T>(T configuration) where T : ConfigModelBase
        {
            ConfigurationService.Set<T>(configuration, Name);
        }

        public abstract Task<IEnumerable<AccountModelBase>> GetAccountsAsync();

        public abstract Task<DirectoryModel> GetDirectoryAsync(AccountModelBase account, string path = null);

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
