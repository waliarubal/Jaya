using Jaya.Shared.Models;
using Jaya.Shared.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Jaya.Shared.Base
{
    public abstract class ProviderServiceBase : IProviderService
    {
        MemoryCacheService _cache;
        ConfigurationService _config;
        PlatformService _platform;

        public delegate void OnAccountAdded(AccountModelBase account);
        public event OnAccountAdded AccountAdded;

        public delegate void OnAccountRemoved(AccountModelBase account);
        public event OnAccountRemoved AccountRemoved;

        protected ProviderServiceBase()
        {
            
        }

        #region properties

        MemoryCacheService Cache
        {
            get
            {
                if (_cache == null)
                    _cache = ServiceLocator.Instance.GetService<MemoryCacheService>();

                return _cache;
            }
        }

        protected string ConfigurationDirectory => ConfigurationService.ConfigurationDirectory;

        protected ConfigurationService ConfigurationService
        {
            get
            {
                if (_config == null)
                    _config = ServiceLocator.Instance.GetService<ConfigurationService>();

                return _config;
            }
        }

        PlatformService Platform
        {
            get
            {
                if (_platform == null)
                    _platform = ServiceLocator.Instance.GetService<PlatformService>();

                return _platform;
            }
        }

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

        protected (string Name, string Extension) SplitName(string fileName)
        {
            var nameParts = fileName.Split('.');
            if (nameParts.Length == 1)
                return (nameParts[0], null);

            var extensionBuilder = new StringBuilder();
            for (var index = 1; index < nameParts.Length - 1; index++)
                extensionBuilder.AppendFormat("{0}.", nameParts[index].ToLower());
            extensionBuilder.Append(nameParts[nameParts.Length - 1]);

            return (nameParts[0], extensionBuilder.ToString());
        }

        protected void OpenBrowser(string url)
        {
            Platform.OpenBrowser(url);
        }

        protected DirectoryModel GetFromCache(AccountModelBase account, DirectoryModel directory)
        {
            var hash = account.GetHashCode();
            if (directory != null)
                hash += directory.GetHashCode();

            if (Cache.TryGetValue(hash, out DirectoryModel dir))
                return dir;

            return null;
        }

        protected void AddToCache(AccountModelBase account, DirectoryModel directory)
        {
            if (directory == null)
                throw new ArgumentNullException(nameof(directory));

            var hash = account.GetHashCode();
            if (!string.IsNullOrEmpty(directory.Path))
                hash += directory.Path.GetHashCode();

            Cache.Set(hash, directory);
        }

        public T GetConfiguration<T>() where T : ConfigModelBase
        {
            return ConfigurationService.GetOrDefault<T>(Name);
        }

        protected void SetConfiguration<T>(T configuration) where T : ConfigModelBase
        {
            ConfigurationService.Set<T>(configuration, Name);
        }

        public async Task<AccountModelBase> AddAccount(AccountModelBase account)
        {
            account = await AddAccountAsync(account);
            if (account != null)
            {
                var handler = AccountAdded;
                if (handler != null)
                    handler.Invoke(account);
            }

            return account;
        }

        public async Task<bool> RemoveAccount(AccountModelBase account)
        {
            var isRemoved = await RemoveAccountAsync(account);
            if (isRemoved)
            {
                var handler = AccountRemoved;
                if (handler != null)
                    handler.Invoke(account);
            }

            return isRemoved;
        }

        protected abstract Task<AccountModelBase> AddAccountAsync(AccountModelBase account = null);

        protected abstract Task<bool> RemoveAccountAsync(AccountModelBase account);

        public abstract Task<IEnumerable<AccountModelBase>> GetAccountsAsync();

        public abstract Task<DirectoryModel> GetDirectoryAsync(AccountModelBase account, DirectoryModel directory = null);

        public abstract Task FormatAsync(AccountModelBase account, DirectoryModel directory = null);

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
