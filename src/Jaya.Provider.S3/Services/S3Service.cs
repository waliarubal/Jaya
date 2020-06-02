
using Jaya.Provider.S3.Models;
using Jaya.Provider.S3.Views;
using Jaya.Shared.Base;
using Jaya.Shared.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Amazon.S3;

namespace Jaya.Provider.S3.Services
{
    public class S3Service : ProviderServiceBase
    {
        static readonly IAmazonS3 client;
        readonly ConfigModel _config = new ConfigModel();
        
        public S3Service()
        {
            Name = "Amazon S3";
            ImagePath = "avares://Jaya.Provider.S3/Assets/Images/AmazonS3-32.png";
            Description = "View your Amazon S3 accounts, inspect their contents and play with directories & files stored within them.";
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
