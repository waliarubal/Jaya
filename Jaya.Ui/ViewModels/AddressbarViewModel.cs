using Jaya.Ui.Base;
using Jaya.Ui.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Input;

namespace Jaya.Ui.ViewModels
{
    public class AddressbarViewModel : ViewModelBase
    {
        readonly Stack<DirectoryChangedEventArgs> _backwardStack, _forwardStack;
        readonly Subscription<DirectoryChangedEventArgs> _onDirectoryChanged;
        readonly char[] _pathSeparator;
        RelayCommand _clearSearch, _navigateBack, _navigateForward;
        ProviderServiceBase _service;
        ProviderModel _provider;

        public AddressbarViewModel()
        {
            _backwardStack = new Stack<DirectoryChangedEventArgs>();
            _forwardStack = new Stack<DirectoryChangedEventArgs>();

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

        public ICommand ClearSearchCommand
        {
            get
            {
                if (_clearSearch == null)
                    _clearSearch = new RelayCommand(ClearSearch);

                return _clearSearch;
            }
        }

        public RelayCommand NavigateBackCommand
        {
            get
            {
                if (_navigateBack == null)
                    _navigateBack = new RelayCommand(NavigateBack, false);

                return _navigateBack;
            }
        }

        public RelayCommand NavigateForwardCommand
        {
            get
            {
                if (_navigateForward == null)
                    _navigateForward = new RelayCommand(NavigateForward, false);

                return _navigateForward;
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

        void NavigateBack()
        {
            var item = _backwardStack.Pop();
            _forwardStack.Push(item);


            var args = new DirectoryChangedEventArgs(item.Service, item.Provider, item.Directory, NavigationDirection.Backward);
            EventAggregator.Publish(args);
        }

        void NavigateForward()
        {
            var item = _forwardStack.Pop();
            _backwardStack.Push(item);

            var args = new DirectoryChangedEventArgs(item.Service, item.Provider, item.Directory, NavigationDirection.Forward);
            EventAggregator.Publish(args);
        }

        void ClearSearch()
        {
            SearchQuery = string.Empty;
        }

        void DirectoryChanged(DirectoryChangedEventArgs args)
        {
            if (args.Direction == NavigationDirection.Unknown)
                _backwardStack.Push(args);

            NavigateBackCommand.IsEnabled = _backwardStack.Count > 0;
            NavigateForwardCommand.IsEnabled = _forwardStack.Count > 0;

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
