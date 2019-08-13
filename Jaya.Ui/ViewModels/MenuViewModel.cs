using Jaya.Ui.Models;

namespace Jaya.Ui.ViewModels
{
    public class MenuViewModel : ViewModelBase
    {
        public ToolbarConfigModel ToolbarConfig => Shared.Instance.ToolbarConfiguration;

        public PaneConfigModel PaneConfig => Shared.Instance.PaneConfiguration;

        public ApplicationConfigModel ApplicationConfig => Shared.Instance.ApplicationConfiguration;

    }
}
