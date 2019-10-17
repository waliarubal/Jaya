using Jaya.Shared;
using Jaya.Shared.Base;
using Jaya.Ui.Models;
using Jaya.Ui.Services;

namespace Jaya.Ui.ViewModels.Windows
{
    public class MainViewModel : ViewModelBase
    {
        readonly Subscription<DirectoryChangedEventArgs> _onDirectoryChanged;
        readonly ConfigurationService _configService;

        public MainViewModel()
        {
            WindowTitle = "Jaya";
            _onDirectoryChanged = EventAggregator.Subscribe<DirectoryChangedEventArgs>(DirectoryChanged);
            _configService = GetService<ConfigurationService>();
        }

        ~MainViewModel()
        {
            EventAggregator.UnSubscribe(_onDirectoryChanged);
        }

        public ToolbarConfigModel ToolbarConfig => _configService.ToolbarConfiguration;

        public PaneConfigModel PaneConfig => _configService.PaneConfiguration;

        public string WindowTitle
        {
            get => Get<string>();
            private set => Set(value);
        }

        void DirectoryChanged(DirectoryChangedEventArgs args)
        {
            if (args.Provider == null)
                WindowTitle = "Jaya";
            else if (args.Directory == null || string.IsNullOrEmpty(args.Directory.Path))
                WindowTitle = args.Provider.Name;
            else
                WindowTitle = args.Directory.Name;
        }
    }
}
