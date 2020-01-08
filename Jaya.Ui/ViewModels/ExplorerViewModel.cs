using Jaya.Shared;
using Jaya.Shared.Base;
using Jaya.Shared.Models;
using Jaya.Ui.Models;
using Jaya.Ui.Services;
using System.Windows.Input;

namespace Jaya.Ui.ViewModels
{
    public class ExplorerViewModel: ViewModelBase
    {
        readonly Subscription<SelectionChangedEventArgs> _onSelectionChanged;
        readonly SharedService _shared;

        ICommand _invokeObject;
        ProviderServiceBase _service;
        AccountModelBase _account;

        public ExplorerViewModel()
        {
            _shared = GetService<SharedService>();
            _onSelectionChanged = EventAggregator?.Subscribe<SelectionChangedEventArgs>(SelectionChanged);
        }

        ~ExplorerViewModel()
        {
            EventAggregator?.UnSubscribe(_onSelectionChanged);
        }

        #region properties

        public ICommand InvokeObjectCommand
        {
            get
            {
                if (_invokeObject == null)
                    _invokeObject = new RelayCommand<ExplorerItemModel>(InvokeObject);

                return _invokeObject;
            }
        }

        public ApplicationConfigModel ApplicationConfig => _shared.ApplicationConfiguration;

        public PaneConfigModel PaneConfig => _shared.PaneConfiguration;

        public ExplorerItemModel Item
        {
            get => Get<ExplorerItemModel>();
            private set => Set(value);
        }

        #endregion

        void InvokeObject(ExplorerItemModel obj)
        {
            if (!obj.Type.HasValue)
                return;

            IsBusy = true;

            DirectoryModel directory = null;
            switch (obj.Type.Value)
            {
                case ItemType.Drive:
                case ItemType.Directory:
                    directory = obj.Object as DirectoryModel;
                    break;

                case ItemType.File:
                    break;

                case ItemType.Computer:
                case ItemType.Account:
                    _account = obj.Object as AccountModelBase;
                    break;

                case ItemType.Service:
                    _service = obj.Object as ProviderServiceBase;
                    _account = null;
                    break;
            }

            IsBusy = false;

            var eventArgs = new SelectionChangedEventArgs(_service, _account, directory);
            EventAggregator.Publish(eventArgs);
        }

        async void SelectionChanged(SelectionChangedEventArgs args)
        {
            Item = null;
            IsBusy = true;

            _service = args.Service;
            _account = args.Account;

            if (_account == null)
            {
                var accounts = await _service.GetAccountsAsync();

                var serviceItem = new ExplorerItemModel(ItemType.Service, _service.Name, _service.ImagePath);
                foreach (var account in accounts)
                {
                    var accountItem = new ExplorerItemModel(_service.IsRootDrive ? ItemType.Computer : ItemType.Account, account.Name, account);
                    serviceItem.Children.Add(accountItem);
                }

                Item = serviceItem;
            }
            else if (args.Directory != null)
            {
                var directory = await args.Service.GetDirectoryAsync(args.Account, args.Directory);

                var directoryItem = new ExplorerItemModel(directory.Type == FileSystemObjectType.Drive ? ItemType.Drive : ItemType.Directory, directory.Name, directory);
                foreach (var subDirectory in directory.Directories)
                {
                    var subDirectoryItem = new ExplorerItemModel(subDirectory.Type == FileSystemObjectType.Drive ? ItemType.Drive : ItemType.Directory, subDirectory.Name, subDirectory);
                    directoryItem.Children.Add(subDirectoryItem);
                }
                if (directory.Files != null)
                {
                    foreach (var file in directory.Files)
                    {
                        var fileItem = new ExplorerItemModel(ItemType.File, file.Name, file);
                        directoryItem.Children.Add(fileItem);
                    }
                }

                Item = directoryItem;
            }

            IsBusy = false;
        }
    }
}
