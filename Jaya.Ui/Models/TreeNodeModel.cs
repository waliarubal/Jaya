using Jaya.Ui.Services.Providers;
using System.Collections.ObjectModel;

namespace Jaya.Ui.Models
{
    public class TreeNodeModel: ModelBase
    {
        private TreeNodeModel _dummyChild;

        public delegate void TreeNodeExpanded(TreeNodeModel node, bool isExpaded);
        public event TreeNodeExpanded NodeExpanded;

        public TreeNodeModel(IProviderService service, ProviderModel provider)
        {
            Service = service;
            Provider = provider;
            Children = new ObservableCollection<TreeNodeModel>();
        }

        #region properties

        public IProviderService Service { get; }

        public ProviderModel Provider { get; }

        public string Label
        {
            get => Get<string>();
            set => Set(value);
        }

        public string Path
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

        public bool IsHavingDummyChild => _dummyChild != null;

        public ObservableCollection<TreeNodeModel> Children { get; }

        #endregion

        public void AddDummyChild()
        {
            if (IsHavingDummyChild)
                return;

            var child = _dummyChild =  new TreeNodeModel(Service, Provider);
            child.Label = "Loading...";
            Children.Add(child);
        }

        public void RemoveDummyChild()
        {
            if (!IsHavingDummyChild)
                return;

            Children.Remove(_dummyChild);
            _dummyChild = null;
        }
    }
}
