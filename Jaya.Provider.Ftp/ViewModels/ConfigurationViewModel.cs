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
        readonly FtpService _service;
        ICommand _addAccount, _removeAccount;

        public ConfigurationViewModel()
        {
            _service = GetProvider<FtpService>();
            _service.AccountAdded += OnAccountAddedOrRemoved;
            _service.AccountRemoved += OnAccountAddedOrRemoved;

            Configuration = _service.GetConfiguration<ConfigModel>();
            SelectedAccount = AccountModel.Empty();
        }

        ~ConfigurationViewModel()
        {
            _service.AccountAdded -= OnAccountAddedOrRemoved;
            _service.AccountRemoved -= OnAccountAddedOrRemoved;
        }

        #region properties

        ConfigModel Configuration { get; }

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
            var isRemoved = await _service.RemoveAccount(account);
            if (isRemoved)
                SelectedAccount = null;
        }

        async void AddAccountAction()
        {
            await _service.AddAccount();
        }
    }
}
