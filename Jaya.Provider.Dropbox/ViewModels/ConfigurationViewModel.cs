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
        ICommand _addAccount;

        public ConfigurationViewModel()
        {
            _dropboxService = GetProvider<DropboxService>();
        }
  
        ConfigModel Configuration => _dropboxService.GetConfiguration<ConfigModel>();

        public IEnumerable<AccountModel> Accounts => Configuration.Accounts;

        public ICommand AddAccountCommand
        {
            get
            {
                if (_addAccount == null)
                    _addAccount = new RelayCommand(AddAccountAction);

                return _addAccount;
            }
        }

        async void AddAccountAction()
        {
            var account = await _dropboxService.AddAccount();
            if (account != null)
                RaisePropertyChanged(nameof(Accounts));
        }
    }
}
