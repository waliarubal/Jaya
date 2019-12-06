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
        AccountModelBase _provider;

        public ExplorerViewModel()
        {
            _shared = GetService<SharedService>();
            _onSelectionChanged = EventAggregator.Subscribe<SelectionChangedEventArgs>(SelectionChanged);
        }

        ~ExplorerViewModel()
        {
            EventAggregator.UnSubscribe(_onSelectionChanged);
        }

        #region properties

        public ICommand InvokeObjectCommand
        {
            get
            {
                if (_invokeObject == null)
                    _invokeObject = new RelayCommand<FileSystemObjectModel>(InvokeObject);

                return _invokeObject;
            }
        }

        public ApplicationConfigModel ApplicationConfig => _shared.ApplicationConfiguration;

        public PaneConfigModel PaneConfig => _shared.PaneConfiguration;

        public DirectoryModel Directory
        {
            get => Get<DirectoryModel>();
            private set => Set(value);
        }

        #endregion

        void InvokeObject(FileSystemObjectModel fileSystemObject)
        {
            switch(fileSystemObject.Type)
            {
                case FileSystemObjectType.Drive:
                case FileSystemObjectType.Directory:
                    var args = new SelectionChangedEventArgs(_service, _provider, fileSystemObject as DirectoryModel);
                    EventAggregator.Publish(args);
                    break;

                case FileSystemObjectType.File:
                    break;
            }
        }

        async void SelectionChanged(SelectionChangedEventArgs args)
        {
            _service = args.Service;
            _provider = args.Account;

            if (args.Directory != null)
            {
                var directory = await args.Service.GetDirectoryAsync(args.Account, args.Directory);
                Directory = directory;
            }
            else
                Directory = null;
        }
    }
}
