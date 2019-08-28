using Jaya.Ui.Models;
using Jaya.Ui.Services.Providers;
using System;
using System.Collections.Generic;
using System.IO;

namespace Jaya.Ui.ViewModels
{
    public class AddressbarViewModel : ViewModelBase
    {
        readonly Subscription<DirectoryChangedEventArgs> _onDirectoryChanged;
        readonly char[] _pathSeparator;
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
        }

        ~AddressbarViewModel()
        {
            EventAggregator.UnSubscribe(_onDirectoryChanged);
        }

        #region properties

        public string SearchQuery
        {
            get => Get<string>();
            set
            {
                Set(value);
                RaisePropertyChanged(nameof(IsSearchQueryEmpty));
            }
        }

        public bool IsSearchQueryEmpty => string.IsNullOrEmpty(SearchQuery);

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

        #endregion

        public void ClearSearchQuery()
        {
            SearchQuery = null;
        }

        void DirectoryChanged(DirectoryChangedEventArgs args)
        {
            _service = args.Service;
            _provider = args.Provider;

            var pathParts = new List<string> { _service.Name, _provider.Name };
            if (_provider == null)
            {
                ImagePath = _service.ImagePath;
            }
            else if (args.Directory == null || string.IsNullOrEmpty(args.Directory.Path))
            {
                ImagePath = _provider.ImagePath;
            }
            else
            {
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
