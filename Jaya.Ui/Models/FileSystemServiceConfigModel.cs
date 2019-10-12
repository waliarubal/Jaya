using Jaya.Ui.Base;

namespace Jaya.Ui.Models
{
    public class FileSystemServiceConfigModel: ConfigModelBase
    {
        public bool IsProtectedFileVisible
        {
            get => Get<bool>();
            set => Set(value);
        }
    }
}
