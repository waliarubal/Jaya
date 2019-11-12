using Jaya.Provider.GoogleDrive.Models;
using Jaya.Provider.GoogleDrive.Services;
using Jaya.Shared;
using Jaya.Shared.Base;
using Jaya.Shared.Models;
using System.Collections.Generic;
using System.Windows.Input;

namespace Jaya.Provider.GoogleDrive.ViewModels
{
    public class ConfigurationViewModel: ViewModelBase
    {
        readonly GoogleDriveService _googleDriveService;
        ICommand _addAccount, _removeAccount;

        public ConfigurationViewModel()
        {
            _googleDriveService = GetProvider<GoogleDriveService>();
            _googleDriveService.AccountAdded += OnAccountAddedOrRemoved;
            _googleDriveService.AccountRemoved += OnAccountAddedOrRemoved;
        }

        ~ConfigurationViewModel()
        {
            _googleDriveService.AccountAdded -= OnAccountAddedOrRemoved;
            _googleDriveService.AccountRemoved -= OnAccountAddedOrRemoved;
        }

        #region properties

        ConfigModel Configuration => _googleDriveService.GetConfiguration<ConfigModel>();

        public IEnumerable<AccountModel> Accounts => Configuration.Accounts;

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
