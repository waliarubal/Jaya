using Dropbox.Api;
using Jaya.Provider.Dropbox.Models;
using Jaya.Provider.Dropbox.Views;
using Jaya.Shared.Base;
using Jaya.Shared.Models;
using Jaya.Shared.Services;
using System.Collections.Generic;
using System.Composition;
using System.Threading.Tasks;

namespace Jaya.Provider.Dropbox.Services
{
    [Export(typeof(IProviderService))]
    [Shared]
    public class DropboxService : ProviderServiceBase, IProviderService
    {
        DropboxClient _client;

        /// <summary>
        /// Refer https://www.dropbox.com/developers/documentation/dotnet#tutorial for Dropbox SDK documentation.
        /// </summary>
        public DropboxService()
        {
            Name = "Dropbox";
            ImagePath = "avares://Jaya.Provider.Dropbox/Assets/Images/Dropbox-32.png";
            Description = "View your Dropbox accounts, inspect their contents and play with directories & files stored within them.";
            IsRootDrive = true;
            ConfigurationEditorType = typeof(ConfigurationView);
            Configuration = ConfigurationService.GetOrDefault<ConfigModel>();
        }

        DropboxClient Client
        {
            get
            {
                if (_client == null)
                    _client = new DropboxClient("JYfV_JpVuKMAAAAAAAAEWW5ARisW4MoJnkHnD-1YcXergmD6ucwyW48pALcfpwhv");

                return _client;
            }
        }

        protected override ProviderModel GetDefaultProvider()
        {
            var provider = new ProviderModel("<Unknown Account>", this);
            provider.ImagePath = "avares://Jaya.Provider.Dropbox/Assets/Images/Dropbox-32.png";
            return provider;
        }

        protected override DirectoryModel GetDirectory(ProviderModel provider, string path = null)
        {
            return Task.Run(() => GetDirectoryAsync(provider, path)).Result;
        }

        new async Task<DirectoryModel> GetDirectoryAsync(ProviderModel provider, string path = null)
        {
            if (path == null)
                path = string.Empty;

            var model = GetFromCache(provider, path);
            if (model != null)
                return model;
            else
                model = new DirectoryModel();

            model.Name = path;
            model.Path = path;
            model.Directories = new List<DirectoryModel>();
            model.Files = new List<FileModel>();

            var entries = await Client.Files.ListFolderAsync(path);
            foreach (var entry in entries.Entries)
            {
                if (entry.IsDeleted)
                    continue;

                if (entry.IsFolder)
                {
                    var directoryInfo = entry.AsFolder;

                    var directory = new DirectoryModel();
                    directory.Name = entry.Name;
                    directory.Path = entry.PathDisplay;
                    model.Directories.Add(directory);

                }
                else if (entry.IsFile)
                {
                    var fileInfo = entry.AsFile;

                    var file = new FileModel();
                    file.Name = entry.Name;
                    file.Path = entry.PathDisplay;
                    file.Size = (long)fileInfo.Size;
                    file.Created = fileInfo.ClientModified;
                    file.Modified = fileInfo.ClientModified;
                    file.Accessed = fileInfo.ClientModified;
                    model.Files.Add(file);
                }
            }

            AddToCache(provider, model);
            return model;
        }

        async void GetAccountAsync()
        {
            var accountInfo = await Client.Users.GetCurrentAccountAsync();
        }
    }
}
