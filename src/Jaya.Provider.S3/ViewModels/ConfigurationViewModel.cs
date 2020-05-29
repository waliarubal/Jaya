using System.Collections.Generic;
using Jaya.Provider.S3.Models;
using Jaya.Provider.S3.Services;
using Jaya.Shared;
using Jaya.Shared.Base;
using Jaya.Shared.Models;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Jaya.Provider.S3.ViewModels
{
    public class ConfigurationViewModel : ViewModelBase
    {
        readonly S3Service _amazonS3Service;
        readonly ConfigModel _config = new ConfigModel();
        ICommand _addAccount, _removeAccount;

        public ConfigurationViewModel()
        {
            _amazonS3Service = GetProvider<S3Service>();

          //  _config = _amazonS3Service.;
          Accounts = new ObservableCollection<AccountModelBase>(new List<AccountModelBase>());
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

        public ICommand AddAccountCommand => _addAccount ??= new RelayCommand(AddAccountAction);

        public ICommand RemoveAccountCommand => _removeAccount ??= new RelayCommand<AccountModel>(RemoveAccountAction);

        #endregion

        async void RemoveAccountAction(AccountModelBase account)
        {
            var isRemoved = await _amazonS3Service.RemoveAccount(account);
            if (isRemoved)
            {
                Accounts.Remove(account);
                SelectedAccount = null;
            }
        }

        async void AddAccountAction()
        {
            var account = await _amazonS3Service.AddAccount(null);
            if (account != null)
                Accounts.Add(account);
        }
    }
}
