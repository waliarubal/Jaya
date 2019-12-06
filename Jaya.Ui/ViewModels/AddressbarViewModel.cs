using Jaya.Shared;
using Jaya.Shared.Base;
using Jaya.Shared.Models;
using Jaya.Ui.Models;
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
        ICommand _clearSearch;
        TreeNodeType? _nodeType;

        public AddressbarViewModel()
        {
            _pathSeparator = new char[]
            {
                Path.DirectorySeparatorChar,
                Path.AltDirectorySeparatorChar
            };
            _navigationService = GetService<NavigationService>();
            _onSelectionChanged = EventAggregator.Subscribe<SelectionChangedEventArgs>(SelectionChanged);

            SearchQuery = string.Empty;
            SearchWatermark = "Search";
        }

        ~AddressbarViewModel()
        {
            EventAggregator.UnSubscribe(_onSelectionChanged);
        }

        #region properties

        public ICommand ClearSearchCommand
        {
            get
            {
                if (_clearSearch == null)
                    _clearSearch = new RelayCommand(ClearSearch);

                return _clearSearch;
            }
        }

        TreeNodeType? NodeType
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

        public bool IsService => NodeType == TreeNodeType.Service;

        public bool IsDrive => NodeType == TreeNodeType.Drive;

        public bool IsDirectory => NodeType == TreeNodeType.Directory;

        public bool IsAccount => NodeType == TreeNodeType.Account;

        public bool IsComputer => NodeType == TreeNodeType.Computer;

        public ICommand NavigateBackCommand => _navigationService.NavigateBackCommand;

        public ICommand NavigateForwardCommand => _navigationService.NavigateForwardCommand;

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

        void TriggerFileSystemObjectTypeChanged(TreeNodeType? type)
        {
            switch (type)
            {
                case TreeNodeType.Service:
                    RaisePropertyChanged(nameof(IsService));
                    break;

                case TreeNodeType.Account:
                    RaisePropertyChanged(nameof(IsAccount));
                    break;

                case TreeNodeType.Computer:
                    RaisePropertyChanged(nameof(IsComputer));
                    break;

                case TreeNodeType.Drive:
                    RaisePropertyChanged(nameof(IsDrive));
                    break;

                case TreeNodeType.Directory:
                    RaisePropertyChanged(nameof(IsDirectory));
                    break;
            }
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
                NodeType = TreeNodeType.Service;
            }
            else if (args.Directory == null || string.IsNullOrEmpty(args.Directory.Path))
            {
                pathParts.Add(args.Account.Name);

                SearchWatermark = string.Format("Search {0}", args.Account.Name);
                ImagePath = args.Account.ImagePath;
                NodeType = args.Service.IsRootDrive ? TreeNodeType.Computer : TreeNodeType.Account;
            }
            else
            {
                SearchWatermark = string.Format("Search {0}", args.Directory.Name);
                if (args.Directory.Type == FileSystemObjectType.Drive)
                    NodeType = TreeNodeType.Drive;
                else
                    NodeType = TreeNodeType.Directory;

                pathParts.AddRange(args.Directory.Path.Split(_pathSeparator, StringSplitOptions.RemoveEmptyEntries));
            }

            PathParts = pathParts;
        }
    }
}
