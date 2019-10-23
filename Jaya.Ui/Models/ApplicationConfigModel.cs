using Jaya.Shared.Base;
using Newtonsoft.Json;

namespace Jaya.Ui.Models
{
    public class ApplicationConfigModel : ConfigModelBase
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

        [JsonProperty]
        public double WidthPx
        {
            get => Get<double>();
            set => Set(value);
        }

        [JsonProperty]
        public double HeightPx
        {
            get => Get<double>();
            set => Set(value);
        }

        protected override ConfigModelBase Empty()
        {
            return new ApplicationConfigModel
            {
                WidthPx = 800,
                HeightPx = 600
            };
        }
    }
}
