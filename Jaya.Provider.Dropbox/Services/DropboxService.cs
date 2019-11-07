using DotNetBox;
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
        const string APP_SECRET = "ipwwjur866rwk3o";

        /// <summary>
        /// Refer https://dotnetbox.readthedocs.io/en/latest/index.html for Dropbox SDK documentation.
        /// </summary>
        public DropboxService([Import(nameof(ConfigurationService))]IService configService) : base(configService)
        {
            Name = "Dropbox";
            ImagePath = "avares://Jaya.Provider.Dropbox/Assets/Images/Dropbox-32.png";
            Description = "View your Dropbox accounts, inspect their contents and play with directories & files stored within them.";
            IsRootDrive = true;
            ConfigurationEditorType = typeof(ConfigurationView);
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

        async Task<string> GetToken()
        {
            var redirectUri = new Uri(REDIRECT_URI);

            var client = new DropboxClient(APP_KEY, APP_SECRET);
            var authorizeUrl = client.GetAuthorizeUrl(ResponseType.Code, REDIRECT_URI);
            OpenBrowser(authorizeUrl);

            var http = new HttpListener();
            http.Prefixes.Add(REDIRECT_URI);
            http.Start();

            var context = await http.GetContextAsync();
            while (context.Request.Url.AbsolutePath != redirectUri.AbsolutePath)
                context = await http.GetContextAsync();

            var code = Uri.UnescapeDataString(context.Request.QueryString["code"]);

            var response = await client.AuthorizeCode(code, REDIRECT_URI);
            return response.AccessToken;
        }

        public override async Task<DirectoryModel> GetDirectoryAsync(AccountModelBase account, string path = null)
        {
            if (path == null)
                path = string.Empty;

            var model = GetFromCache(account, path);
            if (model != null)
                return model;
            else
                model = new DirectoryModel();

            model.Name = path;
            model.Path = path;
            model.Directories = new List<DirectoryModel>();
            model.Files = new List<FileModel>();

            var accountDetails = account as AccountModel;

            var client = new DropboxClient(accountDetails.Token);

            var entries = await client.Files.ListFolder(path);
            foreach (var entry in entries.Entries)
            {
                if (entry.IsDeleted)
                    continue;

                if (entry.IsFolder)
                {
                    var directory = new DirectoryModel();
                    directory.Name = entry.Name;
                    directory.Path = entry.Path;
                    model.Directories.Add(directory);

                }
                else if (entry.IsFile)
                {
                    var file = new FileModel();
                    file.Name = entry.Name;
                    file.Path = entry.Path;
                    model.Files.Add(file);
                }
            }

            AddToCache(account, model);
            return model;
        }

        public async Task<AccountModelBase> AddAccount()
        {
            var token = await GetToken();
            if (string.IsNullOrEmpty(token))
                return null;

            var config = GetConfiguration<ConfigModel>();
            var client = new DropboxClient(token);

            var accountInfo = await client.Users.GetCurrentAccount();

            var provider = new AccountModel(accountInfo.AccountId, accountInfo.Name.DisplayName)
            {
                Email = accountInfo.Email,
                Token = token
            };
            config.Providers.Add(provider);

            SetConfiguration(config);

            return provider;
        }

        public override async Task<IEnumerable<AccountModelBase>> GetAccountsAsync()
        {
            var config = GetConfiguration<ConfigModel>();
            return await Task.Run(() => config.Providers);
        }
    }
}
