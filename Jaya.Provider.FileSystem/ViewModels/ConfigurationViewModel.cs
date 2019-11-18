using Jaya.Provider.FileSystem.Models;
using Jaya.Provider.FileSystem.Services;
using Jaya.Shared.Base;

namespace Jaya.Provider.FileSystem.ViewModels
{
    public class ConfigurationViewModel: ViewModelBase
    {
        readonly FileSystemService _fileSystemService;

        public ConfigurationViewModel()
        {
            _fileSystemService = GetProvider<FileSystemService>();
            Configuration = _fileSystemService.GetConfiguration<ConfigModel>();
        }

        public ConfigModel Configuration { get; }
    }
}
