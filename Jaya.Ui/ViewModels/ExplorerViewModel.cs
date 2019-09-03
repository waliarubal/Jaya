using Jaya.Ui.Models;
using Jaya.Ui.Services;
using Jaya.Ui.Services.Providers;
using ReactiveUI;
using System.Collections.ObjectModel;
using System.Reactive;

namespace Jaya.Ui.ViewModels
{
    public class ExplorerViewModel: ViewModelBase
    {
        readonly Subscription<DirectoryChangedEventArgs> _onDirectoryChanged;
        readonly ConfigurationService _configService;

        ReactiveCommand<FileSystemObjectModel, Unit> _invokeObject;
        IProviderService _service;
        ProviderModel _provider;

        public ExplorerViewModel()
        {
            _configService = GetService<ConfigurationService>();
            _onDirectoryChanged = EventAggregator.Subscribe<DirectoryChangedEventArgs>(DirectoryChanged);
        }

        ~ExplorerViewModel()
        {
            EventAggregator.UnSubscribe(_onDirectoryChanged);
        }

        #region properties

        public ReactiveCommand<FileSystemObjectModel, Unit> InvokeObjectCommand
        {
            get
            {
                if (_invokeObject == null)
                    _invokeObject = ReactiveCommand.Create<FileSystemObjectModel>(InvokeObject);

                return _invokeObject;
            }
        }

        public ApplicationConfigModel ApplicationConfig => _configService.ApplicationConfiguration;

        public PaneConfigModel PaneConfig => _configService.PaneConfiguration;

        public DirectoryModel Directory
        {
            get => Get<DirectoryModel>();
            private set => Set(value);
        }

        #endregion

        void InvokeObject(FileSystemObjectModel @object)
        {
            switch(@object.Type)
            {
                case FileSystemObjectType.Drive:
                case FileSystemObjectType.Directory:
                    var args = new DirectoryChangedEventArgs(_service, _provider, @object as DirectoryModel);
                    EventAggregator.Publish(args);
                    break;

                case FileSystemObjectType.File:
                    break;
            }
        }

        void DirectoryChanged(DirectoryChangedEventArgs args)
        {
            _service = args.Service;
            _provider = args.Provider;

            var directory = args.Service.GetDirectory(args.Provider, args.Directory.Path);
            Directory = directory;
        }
    }
}
