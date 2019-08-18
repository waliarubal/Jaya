using Jaya.Ui.Models;
using Jaya.Ui.Services;

namespace Jaya.Ui.ViewModels
{
    public class NavigationViewModel: ViewModelBase
    {
        public NavigationViewModel()
        {
            Node = new TreeNodeModel();
        }

        public PaneConfigModel PaneConfig => GetService<ConfigurationService>().PaneConfiguration;

        public TreeNodeModel Node { get; }

    }
}
