//using Jaya.Shared.Contracts;
//using Jaya.Shared.Models;
//using Jaya.Shared.Services;
//using System;
//using System.Threading.Tasks;

//namespace Jaya.Shared.Base
//{
//    public abstract class ProviderServiceBase : IProviderService
//    {
//        protected readonly IMemoryCacheService memoryCacheService; // Dependency #1
//        protected readonly IConfigurationService configurationService;

//        protected ProviderServiceBase(IMemoryCacheService memoryCacheService, IConfigurationService configurationService)
//        {
//            this.memoryCacheService = memoryCacheService; // Dependency #1
//            this.configurationService = configurationService; // Dependency #2
//        }

//        #region properties


//        public bool IsRootDrive
//        {
//            get;
//            protected set;
//        }

//        public string Name
//        {
//            get;
//            protected set;
//        }

//        public string Description
//        {
//            get;
//            protected set;
//        }

//        public string ImagePath
//        {
//            get;
//            protected set;
//        }

//        public Type ConfigurationEditorType
//        {
//            get;
//            protected set;
//        }

//        public ConfigModelBase Configuration
//        {
//            get;
//            protected set;
//        }

//        #endregion

//        protected DirectoryModel GetFromCache(ProviderModel provider, string path)
//        {
//            var hash = provider.GetHashCode();
//            if (!string.IsNullOrEmpty(path))
//                hash += path.GetHashCode();

//            if (this.memoryCacheService.TryGetValue(hash, out DirectoryModel directory))
//                return directory;

//            return null;
//        }

//        protected void AddToCache(ProviderModel provider, DirectoryModel directory)
//        {
//            if (directory == null)
//                throw new ArgumentNullException(nameof(directory));

//            var hash = provider.GetHashCode();
//            if (!string.IsNullOrEmpty(directory.Path))
//                hash += directory.Path.GetHashCode();

//            this.memoryCacheService.Set(hash, directory);
//        }

//        public async Task<ProviderModel> GetDefaultProviderAsync()
//        {
//            return await Task.Run(() => GetDefaultProvider());
//        }

//        public async Task<DirectoryModel> GetDirectoryAsync(ProviderModel provider, string path = null)
//        {
//            return await Task.Run(() => GetDirectory(provider, path));
//        }

//        protected abstract ProviderModel GetDefaultProvider();

//        protected abstract DirectoryModel GetDirectory(ProviderModel provider, string path = null);

//        public override string ToString()
//        {
//            return Name;
//        }

//        public override int GetHashCode()
//        {
//            return Name.GetHashCode();
//        }
//    }
//}
