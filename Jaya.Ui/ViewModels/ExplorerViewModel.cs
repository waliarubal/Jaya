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
        readonly Subscription<DirectoryChangedEventArgs> _onDirectoryChanged;
        readonly SharedService _shared;

        ICommand _invokeObject;
        ProviderServiceBase _service;
        ProviderModel _provider;

        public ExplorerViewModel()
        {
            _shared = GetService<SharedService>();
            _onDirectoryChanged = EventAggregator.Subscribe<DirectoryChangedEventArgs>(DirectoryChanged);
        }

        ~ExplorerViewModel()
        {
            EventAggregator.UnSubscribe(_onDirectoryChanged);
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
                    var args = new DirectoryChangedEventArgs(_service, _provider, fileSystemObject as DirectoryModel);
                    EventAggregator.Publish(args);
                    break;

                case FileSystemObjectType.File:
                    break;
            }
        }

        async void DirectoryChanged(DirectoryChangedEventArgs args)
        {
            _service = args.Service;
            _provider = args.Provider;

            var directory = await args.Service.GetDirectoryAsync(args.Provider, args.Directory.Path);
            Directory = directory;
        }
    }
}
