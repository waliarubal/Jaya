using Jaya.Ui.Models;
using Jaya.Ui.Services;

namespace Jaya.Ui.ViewModels
{
    public class MenuViewModel : ViewModelBase
    {
        readonly ConfigurationService _configService;

        public MenuViewModel(ConfigurationService configService)
        {
            _configService = configService;
        }

        public ToolbarConfigModel ToolbarConfig => _configService.ToolbarConfiguration;

        public PaneConfigModel PaneConfig => _configService.PaneConfiguration;

        public ApplicationConfigModel ApplicationConfig => _configService.ApplicationConfiguration;

    }
}
