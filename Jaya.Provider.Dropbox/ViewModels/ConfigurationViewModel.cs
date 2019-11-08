using Jaya.Provider.Dropbox.Models;
using Jaya.Provider.Dropbox.Services;
using Jaya.Shared;
using Jaya.Shared.Base;
using System.Collections.Generic;
using System.Windows.Input;

namespace Jaya.Provider.Dropbox.ViewModels
{
    public class ConfigurationViewModel: ViewModelBase
    {
        readonly DropboxService _dropboxService;
        ICommand _addAccount, _removeAccount;

        public ConfigurationViewModel()
        {
            _dropboxService = GetProvider<DropboxService>();
        }
  
        ConfigModel Configuration => _dropboxService.GetConfiguration<ConfigModel>();

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

        async void RemoveAccountAction(AccountModel account)
        {
            var isRemoved = await _dropboxService.RemoveAccountAsync(account);
            if (isRemoved)
            {
                SelectedAccount = null;
                RaisePropertyChanged(nameof(Accounts));
            }
        }

        async void AddAccountAction()
        {
            var account = await _dropboxService.AddAccountAsync();
            if (account != null)
                RaisePropertyChanged(nameof(Accounts));
        }
    }
}
