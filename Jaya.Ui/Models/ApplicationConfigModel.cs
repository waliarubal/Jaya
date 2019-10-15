using Jaya.Ui.Base;
using Newtonsoft.Json;

namespace Jaya.Ui.Models
{
    public class ApplicationConfigModel: ConfigModelBase
    {
        [JsonProperty]
        public bool IsItemCheckBoxVisible
        {
            get => Get<bool>();
            set => Set(value);
        }

        [JsonProperty]
        public bool IsFileNameExtensionVisible
        {
            get => Get<bool>();
            set => Set(value);
        }

        [JsonProperty]
        public bool IsHiddenItemVisible
        {
            get => Get<bool>();
            set => Set(value);
        }
    }
}
