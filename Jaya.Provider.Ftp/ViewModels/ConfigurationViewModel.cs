using Jaya.Provider.Ftp.Models;
using Jaya.Provider.Ftp.Services;
using Jaya.Shared;
using Jaya.Shared.Base;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Jaya.Provider.Ftp.ViewModels
{
    public class ConfigurationViewModel: ViewModelBase
    {
        readonly FtpService _service;
        readonly ConfigModel _config;
        ICommand _addAccount, _removeAccount, _clear;

        public ConfigurationViewModel()
        {
            _service = GetProvider<FtpService>();

            ClearAction();

            _config = _service.GetConfiguration<ConfigModel>();
            Accounts = new ObservableCollection<AccountModel>(_config.Accounts);
        }

        #region properties

        public ObservableCollection<AccountModel> Accounts { get; }

        public AccountModel SelectedAccount
        {
            get => Get<AccountModel>();
            set => Set(value);
        }

        public AccountModel NewAccount
        {
            get => Get<AccountModel>();
            private set => Set(value);
        }

        public ICommand AddAccountCommand
        {
            get
            {
                if (_addAccount == null)
                    _addAccount = new RelayCommand<AccountModel>(AddAccountAction);

                return _addAccount;
            }
        }

        public ICommand ClearCommand
        {
            get
            {
                if (_clear == null)
                    _clear = new RelayCommand(ClearAction);

                return _clear;
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

        void ClearAction()
        {
            NewAccount = AccountModel.Empty();
        }

        async void RemoveAccountAction(AccountModel account)
        {
            var isRemoved = await _service.RemoveAccount(account);
            if (isRemoved)
            {
                Accounts.Remove(account);
                SelectedAccount = null;
            }
        }

        async void AddAccountAction(AccountModel account)
        {
            var newAccount = await _service.AddAccount(account);
            if (newAccount == null)
                return;

            Accounts.Add(account);
            ClearAction();
        }
    }
}
