using Jaya.Ui.Base;

namespace Jaya.Ui.ViewModels
{
    public class HostWindowViewModel: ViewModelBase
    {
        public string Header
        {
            get => Get<string>();
            set => Set(value);
        }

        public object Child
        {
            get => Get<object>();
            set => Set(value);
        }
    }
}
