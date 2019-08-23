using Jaya.Ui.Models;

namespace Jaya.Ui.ViewModels
{
    public class ExplorerViewModel: ViewModelBase
    {
        public DirectoryModel Directory
        {
            get => Get<DirectoryModel>();
            private set => Set(value);
        }
    }
}
