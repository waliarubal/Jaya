using System.Collections.ObjectModel;

namespace Jaya.Ui.Models
{
    public class TreeNodeModel: ModelBase
    {
        public delegate void TreeNodeExpanded(TreeNodeModel node, bool isExpaded);
        public event TreeNodeExpanded NodeExpanded;

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
            set
            {
                Set(value);

                var handler = NodeExpanded;
                if (handler != null)
                    handler.Invoke(this, value);
            }
        }

        public ObservableCollection<TreeNodeModel> Children { get; }
    }
}
