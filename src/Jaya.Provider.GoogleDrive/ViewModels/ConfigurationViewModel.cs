using Jaya.Provider.GoogleDrive.Models;
using Jaya.Provider.GoogleDrive.Services;
using Jaya.Shared;
using Jaya.Shared.Base;
using Jaya.Shared.Models;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Jaya.Provider.GoogleDrive.ViewModels
{
    public class ConfigurationViewModel : ViewModelBase
    {
        readonly GoogleDriveService _googleDriveService;
        readonly ConfigModel _config;
        ICommand _addAccount, _removeAccount;

        public ConfigurationViewModel()
        {
            _googleDriveService = GetProvider<GoogleDriveService>();

            _config = _googleDriveService.Config;

            Accounts = new ObservableCollection<AccountModelBase>(_config.Accounts);
        }

        #region properties

        public ObservableCollection<AccountModelBase> Accounts { get; }

        public int PageSize
        {
            get => _config.PageSize;
            set
            {
                _config.PageSize = value;
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

        async void RemoveAccountAction(AccountModelBase account)
        {
            var isRemoved = await _googleDriveService.RemoveAccount(account);
            if (isRemoved)
            {
                Accounts.Remove(account);
                SelectedAccount = null;
            }
        }

        async void AddAccountAction()
        {
            var account = await _googleDriveService.AddAccount(null);
            if (account != null)
                Accounts.Add(account);
        }
    }
}
