using Jaya.Shared.Base;
using Jaya.Ui.Models;
using Jaya.Ui.Services;

namespace Jaya.Ui.ViewModels
{
    public class DetailsViewModel : ViewModelBase
    {
        readonly SharedService _shared;

        public DetailsViewModel()
        {
            _shared = GetService<SharedService>();
        }

        public PaneConfigModel PaneConfig => _shared.PaneConfiguration;
    }
}
