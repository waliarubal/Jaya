using Jaya.Shared;
using Jaya.Shared.Base;
using Jaya.Shared.Models;
using Jaya.Ui.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Input;

namespace Jaya.Ui.ViewModels
{
    public class AddressbarViewModel : ViewModelBase
    {
        readonly NavigationService _navigationService;
        readonly Subscription<SelectionChangedEventArgs> _onSelectionChanged;
        readonly char[] _pathSeparator;
        ICommand _clearSearch, _search;
        ItemType? _nodeType;

        public AddressbarViewModel()
        {
            _pathSeparator = new char[]
            {
                Path.DirectorySeparatorChar,
                Path.AltDirectorySeparatorChar
            };
            _navigationService = GetService<NavigationService>();
            _onSelectionChanged = EventAggregator?.Subscribe<SelectionChangedEventArgs>(SelectionChanged);

            SearchQuery = string.Empty;
            SearchWatermark = "Search";
        }

        ~AddressbarViewModel()
        {
            EventAggregator?.UnSubscribe(_onSelectionChanged);
        }

        #region properties

        public ICommand SearchCommand
        {
            get
            {
                if (_search == null)
                    _search = new RelayCommand<string>(SearchAction);

                return _search;
            }
        }

        public ICommand ClearSearchCommand
        {
            get
            {
                if (_clearSearch == null)
                    _clearSearch = new RelayCommand(ClearSearch);

                return _clearSearch;
            }
        }

        ItemType? NodeType
        {
            get => _nodeType;
            set
            {
                if (value == _nodeType)
                    return;

                var oldValue = _nodeType;
                _nodeType = value;

                TriggerFileSystemObjectTypeChanged(oldValue);
                TriggerFileSystemObjectTypeChanged(value);
            }
        }

        public bool IsService => NodeType == ItemType.Service;

        public bool IsDrive => NodeType == ItemType.Drive;

        public bool IsDirectory => NodeType == ItemType.Directory;

        public bool IsAccount => NodeType == ItemType.Account;

        public bool IsComputer => NodeType == ItemType.Computer;

        public ICommand NavigateBackCommand => _navigationService?.NavigateBackCommand;

        public ICommand NavigateForwardCommand => _navigationService?.NavigateForwardCommand;

        public string SearchQuery
        {
            get => Get<string>();
            set => Set(value);
        }

        public string ImagePath
        {
            get => Get<string>();
            private set => Set(value);
        }

        public List<string> PathParts
        {
            get => Get<List<string>>();
            private set => Set(value);
        }

        public string SearchWatermark
        {
            get => Get<string>();
            private set => Set(value);
        }

        #endregion

        void TriggerFileSystemObjectTypeChanged(ItemType? type)
        {
            switch (type)
            {
                case ItemType.Service:
                    RaisePropertyChanged(nameof(IsService));
                    break;

                case ItemType.Account:
                    RaisePropertyChanged(nameof(IsAccount));
                    break;

                case ItemType.Computer:
                    RaisePropertyChanged(nameof(IsComputer));
                    break;

                case ItemType.Drive:
                    RaisePropertyChanged(nameof(IsDrive));
                    break;

                case ItemType.Directory:
                    RaisePropertyChanged(nameof(IsDirectory));
                    break;
            }
        }

        void SearchAction(string searchQuery)
        {

        }

        void ClearSearch()
        {
            SearchQuery = string.Empty;
        }

        void SelectionChanged(SelectionChangedEventArgs args)
        {
            var pathParts = new List<string> { args.Service.Name };
            if (args.Account == null)
            {
                SearchWatermark = string.Format("Search {0}", args.Service.Name);
                ImagePath = args.Service.ImagePath;
                NodeType = ItemType.Service;
            }
            else if (args.Directory == null || string.IsNullOrEmpty(args.Directory.Path))
            {
                pathParts.Add(args.Account.Name);

                SearchWatermark = string.Format("Search {0}", args.Account.Name);
                ImagePath = args.Account.ImagePath;
                NodeType = args.Service.IsRootDrive ? ItemType.Computer : ItemType.Account;
            }
            else
            {
                SearchWatermark = string.Format("Search {0}", args.Directory.Name);
                if (args.Directory.Type == FileSystemObjectType.Drive)
                    NodeType = ItemType.Drive;
                else
                    NodeType = ItemType.Directory;

                pathParts.AddRange(args.Directory.Path.Split(_pathSeparator, StringSplitOptions.RemoveEmptyEntries));
            }

            PathParts = pathParts;
        }
    }
}
