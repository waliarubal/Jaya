using DotNetBox;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Oauth2.v2;
using Google.Apis.Oauth2.v2.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using Jaya.Provider.GoogleDrive.Models;
using Jaya.Provider.GoogleDrive.Views;
using Jaya.Shared.Base;
using Jaya.Shared.Models;
using Jaya.Shared.Services;
using System;
using System.Collections.Generic;
using System.Composition;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Jaya.Provider.GoogleDrive.Services
{
    [Export(typeof(IProviderService))]
    [Shared]
    public class GoogleDriveService : ProviderServiceBase, IProviderService
    {
        const string REDIRECT_URI = "http://localhost:4321/DropboxAuth/";
        const string CLIENT_ID = "538742722606-equtrav33c2tqaq2io7h19mkf4ch6jbp.apps.googleusercontent.com";
        const string CLIENT_SECRET = "UGprjYfFkb--RHnGbgAnm_Aj";

        /// <summary>
        /// Refer pages https://www.daimto.com/google-drive-authentication-c/ and https://www.daimto.com/google-drive-api-c/ for examples.
        /// </summary>
        public GoogleDriveService()
        {
            Name = "Google Drive";
            ImagePath = "avares://Jaya.Provider.GoogleDrive/Assets/Images/GoogleDrive-32.png";
            Description = "View your Google Drive accounts, inspect their contents and play with directories & files stored within them.";
            IsRootDrive = true;
            ConfigurationEditorType = typeof(ConfigurationView);
        }

        async Task<UserCredential> GetToken()
        {
            var scopes = new string[]
            {
                DriveService.Scope.Drive
            };
            var secret = new ClientSecrets
            {
                ClientId = CLIENT_ID,
                ClientSecret = CLIENT_SECRET
            };

            var configDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Jaya");
            var dataStore = new FileDataStore(configDirectory, true);

            return await GoogleWebAuthorizationBroker.AuthorizeAsync(secret, scopes, Environment.UserName, CancellationToken.None, dataStore);

            //var redirectUri = new Uri(REDIRECT_URI);

            //var client = new DropboxClient(CLIENT_ID, CLIENT_SECRET);
            //var authorizeUrl = client.GetAuthorizeUrl(ResponseType.Code, REDIRECT_URI);
            //OpenBrowser(authorizeUrl);

            //var http = new HttpListener();
            //http.Prefixes.Add(REDIRECT_URI);
            //http.Start();

            //var context = await http.GetContextAsync();
            //while (context.Request.Url.AbsolutePath != redirectUri.AbsolutePath)
            //    context = await http.GetContextAsync();

            //http.Stop();

            //var code = Uri.UnescapeDataString(context.Request.QueryString["code"]);

            //var response = await client.AuthorizeCode(code, REDIRECT_URI);
            //return response.AccessToken;
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

        protected override async Task<AccountModelBase> AddAccountAsync()
        {
            var credentials = await GetToken();
            if (credentials == null)
                return null;

            Userinfoplus userInfo;
            using (var oauthSerivce = new Oauth2Service(new BaseClientService.Initializer { HttpClientInitializer = credentials }))
            {
                userInfo = await oauthSerivce.Userinfo.Get().ExecuteAsync();
            }

            var config = GetConfiguration<ConfigModel>();

            var provider = new AccountModel(userInfo.Id, userInfo.Name)
            {
                Email = userInfo.Email,
                Token = credentials.Token.AccessToken
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
    }
}
