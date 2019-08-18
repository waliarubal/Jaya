using Jaya.Ui.Models;
using Jaya.Ui.Services;

namespace Jaya.Ui.ViewModels
{
    public class PreviewViewModel : ViewModelBase
    {
        public PaneConfigModel PaneConfig => GetService<ConfigurationService>().PaneConfiguration;
    }
}
