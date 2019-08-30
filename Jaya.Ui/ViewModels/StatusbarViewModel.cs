using Jaya.Ui.Models;
using Jaya.Ui.Services.Providers;

namespace Jaya.Ui.ViewModels
{
    public class StatusbarViewModel: ViewModelBase
    {
        readonly Subscription<DirectoryChangedEventArgs> _onDirectoryChanged;
        IProviderService _service;
        ProviderModel _provider;
        DirectoryModel _directory;

        public StatusbarViewModel()
        {
            _onDirectoryChanged = EventAggregator.Subscribe<DirectoryChangedEventArgs>(DirectoryChanged);
        }

        ~StatusbarViewModel()
        {
            EventAggregator.UnSubscribe(_onDirectoryChanged);
        }

        public long Count
        {
            get => Get<long>();
            private set => Set(value);
        }

        void DirectoryChanged(DirectoryChangedEventArgs args)
        {
            _service = args.Service;
            _provider = args.Provider;
            _directory = _service.GetDirectory(_provider, args.Directory.Path);

            var count = 0L;
            if (_directory.Directories != null)
                count += _directory.Directories.Count;
            if (_directory.Files != null)
                count += _directory.Files.Count;

            Count = count;
        }
    }
}
