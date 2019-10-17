using Jaya.Shared.Base;
using Jaya.Ui.Models;
using Jaya.Ui.Services;

namespace Jaya.Ui.ViewModels
{
    public class PreviewViewModel : ViewModelBase
    {
        readonly ConfigurationService _configService;

        public PreviewViewModel()
        {
            _configService = GetService<ConfigurationService>();
        }

        public PaneConfigModel PaneConfig => _configService.PaneConfiguration;
    }
}
