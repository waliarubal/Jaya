using Jaya.Shared.Base;
using Jaya.Ui.Models;
using Jaya.Ui.Services;

namespace Jaya.Ui.ViewModels
{
    public class PreviewViewModel : ViewModelBase
    {
        readonly ISharedService _shared;

        public PreviewViewModel()
        {
            _shared = GetService<ISharedService>();
        }

        public PaneConfigModel PaneConfig => _shared.PaneConfiguration;
    }
}
