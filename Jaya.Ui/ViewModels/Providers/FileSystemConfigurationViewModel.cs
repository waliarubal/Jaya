using Jaya.Shared.Base;
using Jaya.Ui.Models.Providers;
using Jaya.Ui.Services;

namespace Jaya.Ui.ViewModels.Providers
{
    public class FileSystemConfigurationViewModel: ViewModelBase
    {
        readonly ConfigurationService _configService;

        public FileSystemConfigurationViewModel()
        {
            _configService = GetService<ConfigurationService>();
        }

        public FileSystemServiceConfigModel FileSystemServiceConfiguration => _configService.FileSystemServiceConfiguration;
    }
}
