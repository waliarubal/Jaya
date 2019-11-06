using Jaya.Provider.Dropbox.Models;
using Jaya.Provider.Dropbox.Services;
using Jaya.Shared;
using Jaya.Shared.Base;
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

        public ConfigModel Configuration => (ConfigModel)_dropboxService.Configuration;

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
            await _dropboxService.AddAccount();
        }
    }
}
