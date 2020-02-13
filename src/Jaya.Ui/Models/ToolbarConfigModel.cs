using Jaya.Shared.Base;
using Newtonsoft.Json;

namespace Jaya.Ui.Models
{
    public class ToolbarConfigModel : ConfigModelBase
    {
        [JsonProperty]
        public bool IsVisible
        {
            get => Get<bool>();
            set => Set(value);
        }

        [JsonProperty]
        public bool IsFileVisible
        {
            get => Get<bool>();
            set => Set(value);
        }

        [JsonProperty]
        public bool IsEditVisible
        {
            get => Get<bool>();
            set => Set(value);
        }

        [JsonProperty]
        public bool IsViewVisible
        {
            get => Get<bool>();
            set => Set(value);
        }

        [JsonProperty]
        public bool IsHelpVisible
        {
            get => Get<bool>();
            set => Set(value);
        }

        protected override ConfigModelBase Empty()
        {
            return new ToolbarConfigModel
            {
                IsFileVisible = true,
                IsEditVisible = true,
                IsViewVisible = true,
                IsHelpVisible = true,
                IsVisible = true
            };
        }
    }
}
