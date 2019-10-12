using Jaya.Ui.Base;

namespace Jaya.Ui.Models
{
    public class ProviderConfigModel: ConfigModelBase
    {
        public FileSystemServiceConfigModel FileSystemServiceConfig
        {
            get => Get<FileSystemServiceConfigModel>();
            private set => Set(value);
        }
    }
}
