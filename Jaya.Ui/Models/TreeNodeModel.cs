using System.Collections.ObjectModel;

namespace Jaya.Ui.Models
{
    public class TreeNodeModel: ModelBase
    {
        public TreeNodeModel()
        {
            Children = new ObservableCollection<TreeNodeModel>();
        }

        public string Label
        {
            get => Get<string>();
            set => Set(value);
        }

        public bool IsExpanded
        {
            get => Get<bool>();
            set => Set(value);
        }

        public ObservableCollection<TreeNodeModel> Children { get; }
    }
}
