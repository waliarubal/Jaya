//
// Copyright (c) Rubal Walia. All rights reserved.
// Licensed under the 3-Clause BSD license. See LICENSE file in the project root for full license information.
//
using Jaya.Shared;
using Jaya.Shared.Base;
using Jaya.Shared.Models;
using Jaya.Ui.Models;
using Jaya.Ui.Services;

namespace Jaya.Ui.ViewModels
{
    public class StatusbarViewModel : ViewModelBase
    {
        readonly Subscription<SelectionChangedEventArgs> _onSelectionChanged;
        readonly SharedService _shared;
        ProviderServiceBase _service;
        AccountModelBase _account;
        DirectoryModel _directory;

        public StatusbarViewModel()
        {
            _onSelectionChanged = EventAggregator?.Subscribe<SelectionChangedEventArgs>(SelectionChanged);
            _shared = GetService<SharedService>();
        }

        ~StatusbarViewModel()
        {
            EventAggregator?.UnSubscribe(_onSelectionChanged);
        }

        public PaneConfigModel PaneConfig => _shared.PaneConfiguration;

        public long Count
        {
            get => Get<long>();
            private set => Set(value);
        }

        async void SelectionChanged(SelectionChangedEventArgs args)
        {
            var count = 0L;

            _service = args.Service;
            _account = args.Account;

            if (_account == null)
            {
                var accounts = await _service.GetAccountsAsync();
                foreach (var account in accounts)
                    count += 1L;
            }
            else if (args.Directory != null)
            {
                _directory = await _service.GetDirectoryAsync(_account, args.Directory);

                if (_directory.Directories != null)
                    count += _directory.Directories.Count;
                if (_directory.Files != null)
                    count += _directory.Files.Count;
            }
            else
                _directory = null;

            Count = count;
        }
    }
}
