using Jaya.Shared;
using Jaya.Shared.Base;
using Jaya.Shared.Models;
using Jaya.Ui.Models;
using Jaya.Ui.Services;

namespace Jaya.Ui.ViewModels
{
    public class StatusbarViewModel : ViewModelBase
    {
        readonly Subscription<DirectoryChangedEventArgs> _onDirectoryChanged;
        readonly SharedService _shared;
        ProviderServiceBase _service;
        AccountModelBase _provider;
        DirectoryModel _directory;

        public StatusbarViewModel()
        {
            _onDirectoryChanged = EventAggregator.Subscribe<DirectoryChangedEventArgs>(DirectoryChanged);
            _shared = GetService<SharedService>();
        }

        ~StatusbarViewModel()
        {
            EventAggregator.UnSubscribe(_onDirectoryChanged);
        }

        public PaneConfigModel PaneConfig => _shared.PaneConfiguration;

        public long Count
        {
            get => Get<long>();
            private set => Set(value);
        }

        async void DirectoryChanged(DirectoryChangedEventArgs args)
        {
            var count = 0L;

            _service = args.Service;
            _provider = args.Provider;

            if (args.Directory != null)
            {
                _directory = await _service.GetDirectoryAsync(_provider, args.Directory);

                if (_directory.Directories != null)
                    count += _directory.Directories.Count;
                if (_directory.Files != null)
                    count += _directory.Files.Count;
            }
            else
                _directory = null;

            Count = count;
        }
    }
}
