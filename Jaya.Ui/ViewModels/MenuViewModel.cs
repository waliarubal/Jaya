using Jaya.Ui.Models;

namespace Jaya.Ui.ViewModels
{
    public class MenuViewModel: ViewModelBase
    {
        public ToolbarsConfigurationModel ToolbarsConfiguration
        {
            get => Shared.Instance.ToolbarsConfiguration;
        }
    }
}
