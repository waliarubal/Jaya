using Jaya.Ui.Base;
using Jaya.Ui.Models;

namespace Jaya.Ui.ViewModels.Windows
{
    public class HostViewModel: ViewModelBase
    {
        public WindowOptionsModel Option
        {
            get => Get<WindowOptionsModel>();
            set => Set(value);
        }
    }
}
