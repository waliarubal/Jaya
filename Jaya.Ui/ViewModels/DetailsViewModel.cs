using Jaya.Shared.Base;
using Jaya.Ui.Models;
using Jaya.Ui.Services;

namespace Jaya.Ui.ViewModels
{
    public class DetailsViewModel : ViewModelBase
    {
        readonly ISharedService _shared;

        public DetailsViewModel()
        {
            _shared = GetService<ISharedService>();
        }

        public PaneConfigModel PaneConfig => _shared.PaneConfiguration;
    }
}
