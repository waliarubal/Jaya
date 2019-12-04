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
        readonly Subscription<DirectoryChangedEventArgs> _onDirectoryChanged;
        readonly char[] _pathSeparator;
        ICommand _clearSearch;

        public AddressbarViewModel()
        {
            _pathSeparator = new char[]
            {
                Path.DirectorySeparatorChar,
                Path.AltDirectorySeparatorChar
            };
            _navigationService = GetService<NavigationService>();
            _onDirectoryChanged = EventAggregator.Subscribe<DirectoryChangedEventArgs>(DirectoryChanged);

            SearchQuery = string.Empty;
            SearchWatermark = "Search";
        }

        ~AddressbarViewModel()
        {
            EventAggregator.UnSubscribe(_onDirectoryChanged);
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
            private set
            {
                Set(value);
                RaisePropertyChanged(nameof(IsHavingImage));
            }
        }

        public bool IsHavingImage => !string.IsNullOrEmpty(ImagePath);

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

        void ClearSearch()
        {
            SearchQuery = string.Empty;
        }

        void DirectoryChanged(DirectoryChangedEventArgs args)
        {
            var pathParts = new List<string> { args.Service.Name, args.Provider.Name };
            if (args.Provider == null)
            {
                SearchWatermark = string.Format("Search {0}", args.Service.Name);
                ImagePath = args.Service.ImagePath;
            }
            else if (args.Directory == null || string.IsNullOrEmpty(args.Directory.Path))
            {
                SearchWatermark = string.Format("Search {0}", args.Provider.Name);
                ImagePath = args.Provider.ImagePath;
            }
            else
            {
                SearchWatermark = string.Format("Search {0}", args.Directory.Name);
                if (args.Directory.Type == FileSystemObjectType.Drive)
                    ImagePath = "Hdd-16.png".GetImageUrl();
                else
                    ImagePath = null;

                pathParts.AddRange(args.Directory.Path.Split(_pathSeparator, StringSplitOptions.RemoveEmptyEntries));
            }

            PathParts = pathParts;
        }
    }
}
