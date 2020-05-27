//
// Copyright (c) Rubal Walia. All rights reserved.
// Licensed under the 3-Clause BSD license. See LICENSE file in the project root for full license information.
//
using Jaya.Provider.Dropbox.Models;
using Jaya.Provider.Dropbox.Services;
using Jaya.Shared;
using Jaya.Shared.Base;
using Jaya.Shared.Models;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Jaya.Provider.Dropbox.ViewModels
{
    public class ConfigurationViewModel : ViewModelBase
    {
        readonly DropboxService _dropboxService;
        ICommand _addAccount, _removeAccount;
       
        public ConfigurationViewModel()
        {
            _dropboxService = GetProvider<DropboxService>();

            var config = _dropboxService.GetConfiguration<ConfigModel>();
            Accounts = new ObservableCollection<AccountModelBase>(config.Accounts);
        }

        #region properties

        public ObservableCollection<AccountModelBase> Accounts { get; }

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

        async void RemoveAccountAction(AccountModel account)
        {
            var isRemoved = await _dropboxService.RemoveAccount(account);
            if (isRemoved)
            {
                Accounts.Remove(account);
                SelectedAccount = null;
            }
        }

        async void AddAccountAction()
        {
            var account = await _dropboxService.AddAccount(null);
            if (account != null)
                Accounts.Add(account);
        }
    }
}
