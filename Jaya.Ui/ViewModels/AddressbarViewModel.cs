using Jaya.Ui.Base;
using Jaya.Ui.Models;
using Jaya.Ui.Services.Providers;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reactive;

namespace Jaya.Ui.ViewModels
{
    public class AddressbarViewModel : ViewModelBase
    {
        readonly Subscription<DirectoryChangedEventArgs> _onDirectoryChanged;
        readonly char[] _pathSeparator;
        ReactiveCommand<Unit, Unit> _clearSearch;
        IProviderService _service;
        ProviderModel _provider;

        public AddressbarViewModel()
        {
            _onDirectoryChanged = EventAggregator.Subscribe<DirectoryChangedEventArgs>(DirectoryChanged);
            _pathSeparator = new char[]
            {
                Path.DirectorySeparatorChar,
                Path.AltDirectorySeparatorChar
            };
            SearchQuery = string.Empty;
            SearchWatermark = "Search";
        }

        ~AddressbarViewModel()
        {
            EventAggregator.UnSubscribe(_onDirectoryChanged);
        }

        #region properties

        public ReactiveCommand<Unit, Unit> ClearSearchCommand
        {
            get
            {
                if (_clearSearch == null)
                    _clearSearch = ReactiveCommand.Create(ClearSearch);

                return _clearSearch;
            }
        }

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

        void ClearSearch()
        {
            SearchQuery = string.Empty;
        }

        void DirectoryChanged(DirectoryChangedEventArgs args)
        {
            _service = args.Service;
            _provider = args.Provider;

            var pathParts = new List<string> { _service.Name, _provider.Name };
            if (_provider == null)
            {
                SearchWatermark = string.Format("Search {0}", _service.Name);
                ImagePath = _service.ImagePath;
            }
            else if (args.Directory == null || string.IsNullOrEmpty(args.Directory.Path))
            {
                SearchWatermark = string.Format("Search {0}", _provider.Name);
                ImagePath = _provider.ImagePath;
            }
            else
            {
                SearchWatermark = string.Format("Search {0}", args.Directory.Name);
                if (args.Directory.Type == FileSystemObjectType.Drive)
                    ImagePath = "avares://Jaya.Ui/Assets/Images/Hdd-16.png";
                else
                    ImagePath = "avares://Jaya.Ui/Assets/Images/Folder-16.png";

                pathParts.AddRange(args.Directory.Path.Split(_pathSeparator, StringSplitOptions.RemoveEmptyEntries));
            }

            PathParts = pathParts;
        }
    }
}
