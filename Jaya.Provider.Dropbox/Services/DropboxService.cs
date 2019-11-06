using Dropbox.Api;
using Jaya.Provider.Dropbox.Views;
using Jaya.Shared.Base;
using Jaya.Shared.Models;
using Jaya.Shared.Services;
using System;
using System.Collections.Generic;
using System.Composition;
using System.Diagnostics;
using System.Net;
using System.Threading.Tasks;

namespace Jaya.Provider.Dropbox.Services
{
    [Export(typeof(IProviderService))]
    [Shared]
    public class DropboxService : ProviderServiceBase, IProviderService
    {
        const string REDIRECT_URI = "http://localhost:99/DropboxAuth";
        const string APP_KEY = "wr1084dwe5oimdh";

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
        }

        public override T GetConfiguration<T>()
        {
            throw new NotImplementedException();
        }

        public async Task<string> GetToken()
        {
            var redirectUri = new Uri(REDIRECT_URI);
            var authUri = DropboxOAuth2Helper.GetAuthorizeUri(OAuthResponseType.Code, APP_KEY, redirectUri);

            Process.Start(authUri.ToString());

            var http = new HttpListener();
            http.Prefixes.Add(REDIRECT_URI);
            http.Start();

            var context = await http.GetContextAsync();
            while(context.Request.Url.AbsolutePath != redirectUri.AbsolutePath)
                context = await http.GetContextAsync();

            redirectUri = new Uri(context.Request.QueryString["url_with_fragment"]);

            var result = DropboxOAuth2Helper.ParseTokenFragment(redirectUri);
            return result.AccessToken;
        }

        public override async Task<DirectoryModel> GetDirectoryAsync(ProviderModel provider, string path = null)
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

            using (var client = new DropboxClient("token"))
            {
                var entries = await client.Files.ListFolderAsync(path);
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
            }

            AddToCache(provider, model);
            return model;
        }

        public override async Task<IEnumerable<ProviderModel>> GetProvidersAsync()
        {
            var providers = new List<ProviderModel>();
            using (var client = new DropboxClient("token"))
            {
                var accountInfo = await client.Users.GetCurrentAccountAsync();

                var provider = new ProviderModel(accountInfo.Name.DisplayName, this);
                provider.ImagePath = "avares://Jaya.Provider.Dropbox/Assets/Images/Account-32.png";
                providers.Add(provider);
            }

            return providers;
        }
    }
}
