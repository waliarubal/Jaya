using Dropbox.Api;
using Jaya.Provider.Dropbox.Models;
using Jaya.Provider.Dropbox.Views;
using Jaya.Shared.Base;
using Jaya.Shared.Models;
using Jaya.Shared.Services;
using System;
using System.Collections.Generic;
using System.Composition;
using System.Diagnostics;
using System.Net;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace Jaya.Provider.Dropbox.Services
{
    [Export(typeof(IProviderService))]
    [Shared]
    public class DropboxService : ProviderServiceBase, IProviderService
    {
        const string REDIRECT_URI = "http://localhost:4321/DropboxAuth/";
        const string APP_KEY = "wr1084dwe5oimdh";
        ConfigModel _config;

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

        public override ConfigModelBase Configuration
        {
            get
            {
                if (_config == null)
                    _config = ConfigurationService.GetOrDefault<ConfigModel>(Name);

                return _config;
            }
        }

        void OpenBrowser(string url)
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                url = url.Replace("&", "^&"); // works on Windows and escape is needed for cmd.exe
                Process.Start(new ProcessStartInfo("cmd", $"/c start {url}"));
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                Process.Start("xdg-open", url); // works on Linux
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            {
                Process.Start("open", url); // not tested
            }
            else
                throw new NotImplementedException();
        }

        public override void SaveConfigurations()
        {
            if (_config == null)
                return;

            ConfigurationService.Set(_config, Name);
        }

        async Task<string> GetToken()
        {
            var redirectUri = new Uri(REDIRECT_URI);
            var authUri = DropboxOAuth2Helper.GetAuthorizeUri(OAuthResponseType.Code, APP_KEY, redirectUri);

            OpenBrowser(authUri.ToString());

            var http = new HttpListener();
            http.Prefixes.Add(REDIRECT_URI);
            http.Start();

            var context = await http.GetContextAsync();
            while (context.Request.Url.AbsolutePath != redirectUri.AbsolutePath)
                context = await http.GetContextAsync();

            //TODO: add  error checks

            return context.Request.QueryString["code"];

            //redirectUri = new Uri(context.Request.QueryString["url_with_fragment"]);

            //var result = DropboxOAuth2Helper.ParseTokenFragment(redirectUri);
            //return result.AccessToken;
        }

        public override async Task<DirectoryModel> GetDirectoryAsync(ProviderModelBase provider, string path = null)
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

        public async Task<ProviderModelBase> AddAccount()
        {
            var token = await GetToken();
            if (string.IsNullOrEmpty(token))
                return null;

            var config = Configuration as ConfigModel;
            using (var client = new DropboxClient(token))
            {
                var accountInfo = await client.Users.GetCurrentAccountAsync();

                var provider = new DropboxProviderModel(accountInfo.Name.DisplayName, this)
                {
                    Email = accountInfo.Email,
                    Token = token
                };
                config.Providers.Add(provider);

                SaveConfigurations();

                return provider;
            }
        }

        public override async Task<IEnumerable<ProviderModelBase>> GetProvidersAsync()
        {
            return await Task.Run(() =>
            {
                var config = Configuration as ConfigModel;
                return config.Providers;
            });
        }
    }
}
