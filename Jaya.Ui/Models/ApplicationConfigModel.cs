namespace Jaya.Ui.Models
{
    public class ApplicationConfigModel: ModelBase
    {
        public bool IsItemCheckBoxVisible
        {
            get => Get<bool>();
            set => Set(value);
        }

        public bool IsFileNameExtensionVisible
        {
            get => Get<bool>();
            set => Set(value);
        }

        public bool IsHiddenItemVisible
        {
            get => Get<bool>();
            set => Set(value);
        }
    }
}
