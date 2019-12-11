using Jaya.Provider.Ftp.Models;
using Jaya.Provider.Ftp.Services;
using Jaya.Shared;
using Jaya.Shared.Base;
using Jaya.Shared.Models;
using System.Collections.Generic;
using System.Windows.Input;

namespace Jaya.Provider.Ftp.ViewModels
{
    public class ConfigurationViewModel: ViewModelBase
    {
        readonly FtpService _googleDriveService;
        ICommand _addAccount, _removeAccount;

        public ConfigurationViewModel()
        {
            _googleDriveService = GetProvider<FtpService>();
            _googleDriveService.AccountAdded += OnAccountAddedOrRemoved;
            _googleDriveService.AccountRemoved += OnAccountAddedOrRemoved;

            Configuration = _googleDriveService.GetConfiguration<ConfigModel>();
        }

        ~ConfigurationViewModel()
        {
            _googleDriveService.AccountAdded -= OnAccountAddedOrRemoved;
            _googleDriveService.AccountRemoved -= OnAccountAddedOrRemoved;
        }

        #region properties

        ConfigModel Configuration { get; }

        public IEnumerable<AccountModel> Accounts => Configuration.Accounts;

        public int PageSize
        {
            get => Configuration.PageSize;
            set
            {
                Configuration.PageSize = value;
                RaisePropertyChanged();
            }
        }

        public AccountModel SelectedAccount
        {
            get => Get<AccountModel>();
            set => Set(value);
        }

        public ICommand AddAccountCommand
        {
            get
            {
                if (_addAccount == null)
                    _addAccount = new RelayCommand(AddAccountAction);

                return _addAccount;
            }
        }

        public ICommand RemoveAccountCommand
        {
            get
            {
                if (_removeAccount == null)
                    _removeAccount = new RelayCommand<AccountModel>(RemoveAccountAction);

                return _removeAccount;
            }
        }

        #endregion

        void OnAccountAddedOrRemoved(AccountModelBase account)
        {
            RaisePropertyChanged(nameof(Accounts));
        }

        async void RemoveAccountAction(AccountModel account)
        {
            var isRemoved = await _googleDriveService.RemoveAccount(account);
            if (isRemoved)
                SelectedAccount = null;
        }

        async void AddAccountAction()
        {
            await _googleDriveService.AddAccount();
        }
    }
}
