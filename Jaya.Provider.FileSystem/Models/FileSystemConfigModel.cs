using Jaya.Shared.Base;
using Newtonsoft.Json;

namespace Jaya.Provider.FileSystem.Models
{
    public class FileSystemConfigModel: ConfigModelBase
    {
        [JsonProperty]
        public bool IsProtectedFileVisible
        {
            get => Get<bool>();
            set => Set(value);
        }

        protected override ConfigModelBase Empty()
        {
            return new FileSystemConfigModel();
        }
    }
}
