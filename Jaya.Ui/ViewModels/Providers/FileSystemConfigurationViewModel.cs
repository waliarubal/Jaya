using Jaya.Shared.Base;
using Jaya.Ui.Models.Providers;
using Jaya.Ui.Services;

namespace Jaya.Ui.ViewModels.Providers
{
    public class FileSystemConfigurationViewModel: ViewModelBase
    {
        readonly SharedService _shared;

        public FileSystemConfigurationViewModel()
        {
            _shared = GetService<SharedService>();
        }

        public FileSystemServiceConfigModel FileSystemServiceConfiguration => _shared.FileSystemServiceConfiguration;
    }
}
