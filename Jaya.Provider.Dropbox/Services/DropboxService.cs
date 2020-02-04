using Dropbox.Api;
using Jaya.Provider.Dropbox.Models;
using Jaya.Provider.Dropbox.Views;
using Jaya.Shared.Base;
using Jaya.Shared.Models;
using Jaya.Shared.Services;
using System;
using System.Collections.Generic;
using System.Composition;
using System.Net;
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
        /// Refer https://www.dropbox.com/developers/documentation/dotnet#tutorial and http://dropbox.github.io/dropbox-sdk-dotnet/html/T_Dropbox_Api_DropboxOAuth2Helper.htm for Dropbox SDK documentation.
        /// </summary>
        public DropboxService()
        {
            Name = "Dropbox";
            ImagePath = "avares://Jaya.Provider.Dropbox/Assets/Images/Dropbox-32.png";
            Description = "View your Dropbox accounts, inspect their contents and play with directories & files stored within them.";
            ConfigurationEditorType = typeof(ConfigurationView);
        }

        async Task<string> GetToken()
        {
            var redirectUri = new Uri(REDIRECT_URI);

            var authorizeUrl = DropboxOAuth2Helper.GetAuthorizeUri(OAuthResponseType.Code, APP_KEY, new Uri(REDIRECT_URI));
            OpenBrowser(authorizeUrl.ToString());

            var http = new HttpListener();
            http.Prefixes.Add(REDIRECT_URI);
            http.Start();

            var context = await http.GetContextAsync();
            while (context.Request.Url.AbsolutePath != redirectUri.AbsolutePath)
                context = await http.GetContextAsync();

            http.Stop();

            var code = Uri.UnescapeDataString(context.Request.QueryString["code"]);

            var response = await DropboxOAuth2Helper.ProcessCodeFlowAsync(code, APP_KEY, APP_SECRET, REDIRECT_URI);
            return response.AccessToken;
        }

        public override async Task<DirectoryModel> GetDirectoryAsync(AccountModelBase account, DirectoryModel directory = null)
        {
            var model = GetFromCache(account, directory);
            if (model != null)
                return model;
            else
                model = new DirectoryModel();

            model.Name = directory.Name;
            model.Path = directory.Path;
            model.Directories = new List<DirectoryModel>();
            model.Files = new List<FileModel>();

            var accountDetails = account as AccountModel;

            var client = new DropboxClient(accountDetails.Token);

            var path = directory == null || directory.Path == null ? string.Empty : directory.Path;

            var entries = await client.Files.ListFolderAsync(path);
            foreach (var entry in entries.Entries)
            {
                if (entry.IsDeleted)
                    continue;

                if (entry.IsFolder)
                {
                    var dir = new DirectoryModel();
                    dir.Name = entry.Name;
                    dir.Path = entry.PathDisplay;
                    model.Directories.Add(dir);

                }
                else if (entry.IsFile)
                {
                    var nameParts = SplitName(entry.Name);

                    var fileInfo = entry.AsFile;

                    var file = new FileModel();
                    file.Name = nameParts.Name;
                    file.Extension = nameParts.Extension;
                    file.Path = entry.PathDisplay;
                    file.Size = (long)fileInfo.Size;
                    file.Modified = fileInfo.ClientModified;
                    model.Files.Add(file);
                }
            }

            AddToCache(account, model);
            return model;
        }

        protected override async Task<AccountModelBase> AddAccountAsync(AccountModelBase account = null)
        {
            var token = await GetToken();
            if (string.IsNullOrEmpty(token))
                return null;

            var config = GetConfiguration<ConfigModel>();
            var client = new DropboxClient(token);

            var accountInfo = await client.Users.GetCurrentAccountAsync();

            var provider = new AccountModel(accountInfo.AccountId, accountInfo.Name.DisplayName)
            {
                Email = accountInfo.Email,
                Token = token
            };

            config.Accounts.Add(provider);
            SetConfiguration(config);

            return provider;
        }

        protected override async Task<bool> RemoveAccountAsync(AccountModelBase account)
        {
            var config = GetConfiguration<ConfigModel>();

            var isRemoved = config.Accounts.Remove(account as AccountModel);
            if (isRemoved)
                SetConfiguration(config);

            return await Task.Run(() => isRemoved);
        }

        public override async Task<IEnumerable<AccountModelBase>> GetAccountsAsync()
        {
            var config = GetConfiguration<ConfigModel>();
            return await Task.Run(() => config.Accounts);
        }

        public override Task FormatAsync(AccountModelBase account, DirectoryModel directory = null)
        {
            throw new NotImplementedException();
        }
    }
}
