using Jaya.Shared.Base;
using Jaya.Shared.Models;

namespace Jaya.Ui.ViewModels.Windows
{
    public class HostViewModel : ViewModelBase
    {
        public WindowOptionsModel Option
        {
            get => Get<WindowOptionsModel>();
            set => Set(value);
        }
    }
}
