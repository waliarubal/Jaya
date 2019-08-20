using Jaya.Ui.Models;
using Jaya.Ui.Services;

namespace Jaya.Ui.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        readonly ConfigurationService _configService;
        public MainWindowViewModel(ConfigurationService configService)
        {
            _configService = configService;
        }

        public ToolbarConfigModel ToolbarConfig => _configService.ToolbarConfiguration;

        public PaneConfigModel PaneConfig => _configService.PaneConfiguration;
    }
}
