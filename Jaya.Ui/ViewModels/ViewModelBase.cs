using Jaya.Ui.Models;

namespace Jaya.Ui.ViewModels
{
    public abstract class ViewModelBase : ModelBase
    {
        public void ExecuteCommand(CommandType type)
        {
            Shared.Instance.ExecuteCommand(type);
        }
    }
}
