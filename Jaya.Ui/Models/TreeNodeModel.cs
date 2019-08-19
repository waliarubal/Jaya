using Jaya.Ui.Services.Providers;
using System.Collections.ObjectModel;

namespace Jaya.Ui.Models
{
    public class TreeNodeModel : ModelBase
    {
        const string COLLAPSED_IMAGE = "avares://Jaya.Ui/Assets/Images/Folder-16.png";
        const string EXPANDED_IMAGE = "avares://Jaya.Ui/Assets/Images/Folder-Open-16.png";

        private TreeNodeModel _dummyChild;

        public delegate void TreeNodeExpanded(TreeNodeModel node, bool isExpaded);
        public event TreeNodeExpanded NodeExpanded;

        public TreeNodeModel(IProviderService service, ProviderModel provider)
        {
            Service = service;
            Provider = provider;
            IconImagePath = COLLAPSED_IMAGE;
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

        public string ImagePath
        {
            get => Get<string>();
            set
            {
                Set(value);

                if (string.IsNullOrEmpty(value))
                    IconImagePath = COLLAPSED_IMAGE;
                else
                    IconImagePath = ImagePath;
            }
        }

        public string IconImagePath
        {
            get => Get<string>();
            private set => Set(value);
        }

        public bool IsExpanded
        {
            get => Get<bool>();
            set
            {
                Set(value);

                if (string.IsNullOrEmpty(ImagePath))
                {
                    if (value && Children.Count > 0)
                        IconImagePath = EXPANDED_IMAGE;
                    else
                        IconImagePath = COLLAPSED_IMAGE;
                }
                else
                    IconImagePath = ImagePath;

                var handler = NodeExpanded;
                if (handler != null)
                    handler.Invoke(this, value);
            }
        }

        public bool IsHavingDummyChild => _dummyChild != null;

        public ObservableCollection<TreeNodeModel> Children { get; }

        public FileSystemObjectModel FileSystemObject
        {
            get => Get<FileSystemObjectModel>();
            set => Set(value);
        }

        #endregion

        public void AddDummyChild()
        {
            if (IsHavingDummyChild)
                return;

            var child = _dummyChild = new TreeNodeModel(Service, Provider);
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
