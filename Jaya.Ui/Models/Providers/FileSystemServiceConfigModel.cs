using Jaya.Ui.Base;
using Newtonsoft.Json;

namespace Jaya.Ui.Models.Providers
{
    public class FileSystemServiceConfigModel: ConfigModelBase
    {
        [JsonProperty]
        public bool IsProtectedFileVisible
        {
            get => Get<bool>();
            set => Set(value);
        }
    }
}
