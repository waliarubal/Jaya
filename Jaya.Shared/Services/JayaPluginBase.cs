using Jaya.Shared.Base;
using Jaya.Shared.Contracts;
using Jaya.Shared.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Jaya.Shared.Services
{
    public abstract class JayaPluginBase
    {
        protected readonly IMemoryCacheService memoryCacheService; // Dependency #1
        protected readonly IConfigurationService configurationService; // Dependency #2

        protected JayaPluginBase(IMemoryCacheService memoryCacheService, IConfigurationService configurationService)
        {
            this.memoryCacheService = memoryCacheService; // Dependency #1
            this.configurationService = configurationService; // Dependency #2
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

        public ConfigModelBase Configuration
        {
            get;
            protected set;
        }

        protected DirectoryModel GetFromCache(ProviderModel provider, string path)
        {
            if (provider == null)
                return null;

            var hash = provider.GetHashCode();
            if (!string.IsNullOrEmpty(path))
                hash += path.GetHashCode();

            if (this.memoryCacheService.TryGetValue(hash, out DirectoryModel directory))
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

            this.memoryCacheService.Set(hash, directory);
        }
        
        public abstract ProviderModel GetDefaultProvider();

        public abstract DirectoryModel GetDirectory(ProviderModel provider, string path = null);
    }
}
