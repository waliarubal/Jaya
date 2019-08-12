namespace Jaya.Ui.Models
{
    public class ToolbarsConfigurationModel: ModelBase
    {
        public bool IsToolbarTrayVisible
        {
            get => Get<bool>();
            set => Set(value);
        }

        public bool IsFileToolbarVisible
        {
            get => Get<bool>();
            set => Set(value);
        }

        public bool IsEditToolbarVisible
        {
            get => Get<bool>();
            set => Set(value);
        }

        public bool IsViewToolbarVisible
        {
            get => Get<bool>();
            set => Set(value);
        }

        public bool IsHelpToolbarVisible
        {
            get => Get<bool>();
            set => Set(value);
        }
    }
}
