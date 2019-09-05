using Jaya.Ui.Base;

namespace Jaya.Ui.Models
{
    public class ToolbarConfigModel: ModelBase
    {
        public bool IsVisible
        {
            get => Get<bool>();
            set => Set(value);
        }

        public bool IsFileVisible
        {
            get => Get<bool>();
            set => Set(value);
        }

        public bool IsEditVisible
        {
            get => Get<bool>();
            set => Set(value);
        }

        public bool IsViewVisible
        {
            get => Get<bool>();
            set => Set(value);
        }

        public bool IsHelpVisible
        {
            get => Get<bool>();
            set => Set(value);
        }
    }
}
