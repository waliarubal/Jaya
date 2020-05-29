
using Jaya.Provider.S3.Models;
using Jaya.Provider.S3.Views;
using Jaya.Shared.Base;
using Jaya.Shared.Models;
using Jaya.Shared.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Amazon.S3;
using Amazon.S3.Model;

namespace Jaya.Provider.S3.Services
{
    public class S3Service : ProviderServiceBase
    {
        private static IAmazonS3 client;
        ConfigModel _config = new ConfigModel();
        /// <summary>
        /// Refer pages https://www.daimto.com/google-drive-authentication-c/ and https://www.daimto.com/google-drive-api-c/ for examples.
        /// </summary>
        public S3Service()
        {
            Name = "Amazon S3";
            ImagePath = "avares://Jaya.Provider.GoogleDrive/Assets/Images/AmazonS3-32.png";
            Description = "View your S3 account accounts, inspect their contents and play with directories & files stored within them.";
            ConfigurationEditorType = typeof(ConfigurationView);
        }

        public override async Task<DirectoryModel> GetDirectoryAsync(AccountModelBase account, DirectoryModel directory = null)
        {
            var model = Task.FromResult(new DirectoryModel());
            return await model;
        }

        protected override async Task<AccountModelBase> AddAccountAsync(AccountModelBase account = null)
        {
            var provider = new AccountModel("aps","aprs");
            return await Task.FromResult(provider);
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
