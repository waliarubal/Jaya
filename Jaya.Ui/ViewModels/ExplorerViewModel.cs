using Jaya.Ui.Models;

namespace Jaya.Ui.ViewModels
{
    public class ExplorerViewModel: ViewModelBase
    {
        readonly Subscription<DirectoryModel> _onDirectoryChanged;

        public ExplorerViewModel()
        {
            _onDirectoryChanged = EventAggregator.Subscribe<DirectoryModel>(DirectoryChanged);
        }

        ~ExplorerViewModel()
        {
            EventAggregator.UnSubscribe(_onDirectoryChanged);
        }

        public DirectoryModel Directory
        {
            get => Get<DirectoryModel>();
            private set => Set(value);
        }

        void DirectoryChanged(DirectoryModel directory)
        {
            Directory = directory;
        }
    }
}
