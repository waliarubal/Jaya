using Jaya.Ui.Models;
using Jaya.Ui.Services.Providers;
using ReactiveUI;
using System.Reactive;

namespace Jaya.Ui.ViewModels
{
    public class ExplorerViewModel: ViewModelBase
    {
        readonly Subscription<DirectoryChangedEventArgs> _onDirectoryChanged;
        ReactiveCommand<DirectoryModel, Unit> _changeDirectory;
        IProviderService _service;
        ProviderModel _provider;

        public ExplorerViewModel()
        {
            _onDirectoryChanged = EventAggregator.Subscribe<DirectoryChangedEventArgs>(DirectoryChanged);
        }

        ~ExplorerViewModel()
        {
            EventAggregator.UnSubscribe(_onDirectoryChanged);
        }

        public ReactiveCommand<DirectoryModel, Unit> ChangeDirectoryCommand
        {
            get
            {
                if (_changeDirectory == null)
                    _changeDirectory = ReactiveCommand.Create<DirectoryModel>(ChangeDirectory);

                return _changeDirectory;
            }
        }

        public DirectoryModel Directory
        {
            get => Get<DirectoryModel>();
            private set => Set(value);
        }

        void ChangeDirectory(DirectoryModel directory)
        {
            var args = new DirectoryChangedEventArgs(_service, _provider, directory);
            EventAggregator.Publish(args);
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
