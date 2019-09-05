using Jaya.Ui.Base;
using Jaya.Ui.Models;
using Jaya.Ui.Services;

namespace Jaya.Ui.ViewModels
{
    public class ToolbarViewModel : ViewModelBase
    {
        readonly ConfigurationService _configService;

        public ToolbarViewModel()
        {
            _configService = GetService<ConfigurationService>();
        }

        public ToolbarConfigModel ToolbarConfig => _configService.ToolbarConfiguration;

        public PaneConfigModel PaneConfig => _configService.PaneConfiguration;

        public ApplicationConfigModel ApplicationConfig => _configService.ApplicationConfiguration;
    }
}
