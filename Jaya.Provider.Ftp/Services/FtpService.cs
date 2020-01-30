using FluentFTP;
using Jaya.Provider.Ftp.Models;
using Jaya.Provider.Ftp.Views;
using Jaya.Shared.Base;
using Jaya.Shared.Models;
using Jaya.Shared.Services;
using System;
using System.Collections.Generic;
using System.Composition;
using System.Net;
using System.Threading.Tasks;

namespace Jaya.Provider.Ftp.Services
{
    [Export(typeof(IProviderService))]
    [Shared]
    public class FtpService : ProviderServiceBase, IProviderService
    {
        ConfigModel _config;

        /// <summary>
        /// Refer pages https://www.daimto.com/google-drive-authentication-c/ and https://www.daimto.com/google-drive-api-c/ for examples.
        /// </summary>
        public FtpService()
        {
            Name = "FTP/FTPS";
            ImagePath = "avares://Jaya.Provider.Ftp/Assets/Images/Ftp-32.png";
            Description = "View your FTP/FTPS accounts, inspect their contents and play with directories & files stored within them.";
            ConfigurationEditorType = typeof(ConfigurationView);
        }

        ConfigModel Config
        {
            get
            {
                if (_config == null)
                    _config = GetConfiguration<ConfigModel>();

                return _config;
            }
        }

        async Task<FtpClient> GetConnection(AccountModel account)
        {
            var connection = new FtpClient();
            connection.Host = account.Host;
            connection.Port = account.Port;
            
            if (!account.IsAnonymous)
            {
                var credentials = new NetworkCredential(account.UserName, account.Password);
                connection.Credentials = credentials;
            }

            await connection.ConnectAsync();
            return connection;
        }

        public override async Task<DirectoryModel> GetDirectoryAsync(AccountModelBase account, DirectoryModel directory = null)
        {
            var model = GetFromCache(account, directory);
            if (model != null)
                return model;
            else
            {
                model = new DirectoryModel();
                model.Path = "/";
            }

            var path = directory == null || string.IsNullOrEmpty(directory.Path) ? "/" : directory.Path;

            model.Name = directory.Name;
            model.Path = path;
            model.Directories = new List<DirectoryModel>();
            model.Files = new List<FileModel>();

            var client = await GetConnection(account as AccountModel);
            var entries = await client.GetListingAsync(path, FtpListOption.AllFiles);
            foreach (var entry in entries)
            {
                switch(entry.Type)
                {
                    case FtpFileSystemObjectType.Directory:
                        var dir = new DirectoryModel();
                        dir.Id = entry.FullName.GetHashCode().ToString();
                        dir.Name = entry.Name;
                        dir.Path = entry.FullName;
                        dir.Size = entry.Size;
                        dir.Created = entry.Created;
                        dir.Modified = entry.Modified;
                        model.Directories.Add(dir);
                        break;

                    case FtpFileSystemObjectType.File:
                        var file = new FileModel();
                        file.Id = entry.FullName.GetHashCode().ToString();
                        file.Name = entry.Name;
                        file.Path = entry.FullName;
                        file.Size = entry.Size;
                        file.Created = entry.Created;
                        file.Modified = entry.Modified;
                        model.Files.Add(file);
                        break;
                }
            }

            AddToCache(account, model);
            return model;
        }

        protected override async Task<AccountModelBase> AddAccountAsync()
        {
            var config = GetConfiguration<ConfigModel>();

            var account = new AccountModel("", "");

            config.Accounts.Add(account);
            SetConfiguration(config);

            return account;
            /*
            var credentials = await GetCredential();
            if (credentials == null)
                return null;

            Userinfoplus userInfo;
            using (var authService = new Oauth2Service(GetServiceInitializer(credentials)))
            {
                userInfo = await authService.Userinfo.Get().ExecuteAsync();
            }

            var config = GetConfiguration<ConfigModel>();

            var provider = new AccountModel(userInfo.Id, userInfo.Name)
            {
                Email = userInfo.Email
            };

            config.Accounts.Add(provider);
            SetConfiguration(config);

            return provider;
            */
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
