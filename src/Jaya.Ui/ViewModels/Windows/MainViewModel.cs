using Jaya.Shared;
using Jaya.Shared.Base;
using Jaya.Ui.Models;
using Jaya.Ui.Services;
using System.ComponentModel;

namespace Jaya.Ui.ViewModels.Windows
{
    public class MainViewModel : ViewModelBase
    {
        readonly Subscription<SelectionChangedEventArgs> _onDirectoryChanged;
        readonly SharedService _shared;

        public MainViewModel()
        {
            WindowTitle = Constants.APP_NAME;

            _onDirectoryChanged = EventAggregator?.Subscribe<SelectionChangedEventArgs>(DirectoryChanged);

            _shared = GetService<SharedService>();
            _shared.ToolbarConfiguration.PropertyChanged += OnPropertyChanged;
            _shared.PaneConfiguration.PropertyChanged += OnPropertyChanged;
        }

        ~MainViewModel()
        {
            _shared.ToolbarConfiguration.PropertyChanged -= OnPropertyChanged;
            _shared.PaneConfiguration.PropertyChanged -= OnPropertyChanged;

            EventAggregator.UnSubscribe(_onDirectoryChanged);
        }

        public ToolbarConfigModel ToolbarConfig => _shared.ToolbarConfiguration;

        public PaneConfigModel PaneConfig => _shared.PaneConfiguration;

        public ApplicationConfigModel ApplicationConfig => _shared.ApplicationConfiguration;

        public bool IsToolbarVisible => ToolbarConfig.IsVisible && !PaneConfig.IsRibbonVisible;

        public string WindowTitle
        {
            get => Get<string>();
            private set => Set(value);
        }

        void DirectoryChanged(SelectionChangedEventArgs args)
        {
            if (args.Account == null)
                WindowTitle = Constants.APP_NAME;
            else if (args.Directory == null || string.IsNullOrEmpty(args.Directory.Path))
                WindowTitle = args.Account.Name;
            else
                WindowTitle = args.Directory.Name;
        }

        void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch(e.PropertyName)
            {
                case nameof(PaneConfigModel.IsRibbonVisible):
                case nameof(ToolbarConfigModel.IsVisible):
                    RaisePropertyChanged(nameof(IsToolbarVisible));
                    break;
            }
        }
    }
}
