using Jaya.Ui.Models;
using Jaya.Ui.Services;

namespace Jaya.Ui.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        public ToolbarConfigModel ToolbarConfig => GetService<ConfigurationService>().ToolbarConfiguration;

        public PaneConfigModel PaneConfig => GetService<ConfigurationService>().PaneConfiguration;
    }
}
