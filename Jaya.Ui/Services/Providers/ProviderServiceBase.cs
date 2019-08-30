using Jaya.Ui.Models;
using System;
using System.Collections.Generic;

namespace Jaya.Ui.Services.Providers
{
    public abstract class ProviderServiceBase : IProviderService
    {
        readonly Dictionary<int, DirectoryModel> _cache;

        protected ProviderServiceBase()
        {
            _cache = new Dictionary<int, DirectoryModel>();
        }

        ~ProviderServiceBase()
        {
            _cache.Clear();
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

        public string ImagePath
        {
            get;
            protected set;
        }

        protected DirectoryModel GetFromCache(ProviderModel provider, string path)
        {
            var hash = provider.GetHashCode();
            if (!string.IsNullOrEmpty(path))
                hash += path.GetHashCode();

            if (_cache.ContainsKey(hash))
                return _cache[hash];

            return null;
        }

        protected void AddToCache(ProviderModel provider, DirectoryModel directory)
        {
            if (directory == null)
                throw new ArgumentNullException(nameof(directory));

            var hash = provider.GetHashCode();
            if (!string.IsNullOrEmpty(directory.Path))
                hash += directory.Path.GetHashCode();

            if (_cache.ContainsKey(hash))
                _cache[hash] = directory;
            else
                _cache.Add(hash, directory);
        }

        public abstract ProviderModel GetDefaultProvider();

        public abstract DirectoryModel GetDirectory(ProviderModel provider, string path = null);

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
