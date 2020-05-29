//
// Copyright (c) Rubal Walia. All rights reserved.
// Licensed under the 3-Clause BSD license. See LICENSE file in the project root for full license information.
//
using FluentFTP;
using Jaya.Provider.Ftp.Models;
using Jaya.Provider.Ftp.Views;
using Jaya.Shared.Base;
using Jaya.Shared.Models;
using Jaya.Shared.Services;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace Jaya.Provider.Ftp.Services
{
    public class FtpService : ProviderServiceBase, IProviderService
    {
        public FtpService()
        {
            Name = "FTP/FTPS";
            ImagePath = "avares://Jaya.Provider.Ftp/Assets/Images/Ftp-32.png";
            Description = "View your FTP/FTPS accounts, inspect their contents and play with directories & files stored within them.";
            ConfigurationEditorType = typeof(ConfigurationView);
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

            using (var client = await GetConnection(account as AccountModel))
            {
                var entries = await client.GetListingAsync(path, FtpListOption.AllFiles);
                foreach (var entry in entries)
                {
                    switch (entry.Type)
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

                if (client.IsConnected)
                    await client.DisconnectAsync();
            }

            AddToCache(account, model);
            return model;
        }

        protected override async Task<AccountModelBase> AddAccountAsync(AccountModelBase account = null)
        {
            var ftpAccount = account as AccountModel;

            using (var connection = await GetConnection(ftpAccount))
            {
                if (!connection.IsConnected)
                    return null;

                await connection.DisconnectAsync();

                var config = GetConfiguration<ConfigModel>();
                config.Accounts.Add(ftpAccount);
                SetConfiguration(config);

                return ftpAccount;
            }
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
