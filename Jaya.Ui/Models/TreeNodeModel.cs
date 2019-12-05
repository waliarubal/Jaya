using Jaya.Shared;
using Jaya.Shared.Base;
using Jaya.Shared.Models;
using Jaya.Ui.Services;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace Jaya.Ui.Models
{
    public enum TreeNodeType: byte
    {
        Service,
        Account,
        Drive,
        Directory,
        Dummy,
        Computer
    }

    public class TreeNodeModel : ModelBase
    {
        TreeNodeModel _dummyChild;
        readonly SharedService _shared;

        public delegate void TreeNodeExpanded(TreeNodeModel node, bool isExpaded);
        public event TreeNodeExpanded NodeExpanded;

        public TreeNodeModel(ProviderServiceBase service, AccountModelBase account, TreeNodeType? nodeType)
        {
            Service = service;
            Account = account;
            Children = new ObservableCollection<TreeNodeModel>();
            NodeType = nodeType;

            if (Service != null && Account != null)
            {
                _shared = ServiceLocator.Instance.GetService<SharedService>();
                _shared.ApplicationConfiguration.PropertyChanged += OnApplicationConfigChanged;
            }
        }

        ~TreeNodeModel()
        {
            if (_shared != null)
                _shared.ApplicationConfiguration.PropertyChanged -= OnApplicationConfigChanged;
        }

        #region properties

        internal TreeNodeType? NodeType { get; }

        public bool IsDummy => NodeType == TreeNodeType.Dummy;

        public bool IsService => NodeType == TreeNodeType.Service;

        public bool IsDrive => NodeType == TreeNodeType.Drive;

        public bool IsDirectory => NodeType == TreeNodeType.Directory;

        public bool IsAccount => NodeType == TreeNodeType.Account;

        public bool IsComputer => NodeType == TreeNodeType.Computer;

        public ProviderServiceBase Service { get; }

        public AccountModelBase Account { get; }

        public string Label
        {
            get => Get<string>();
            set => Set(value);
        }

        public string ImagePath
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

        public FileSystemObjectModel FileSystemObject
        {
            get => Get<FileSystemObjectModel>();
            set => Set(value);
        }

        private void OnApplicationConfigChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(ApplicationConfigModel.IsHiddenItemVisible):
                    if (FileSystemObject != null)
                        FileSystemObject.RaisePropertyChanged(nameof(FileSystemObject.IsHidden));
                    break;
            }
        }

        #endregion

        public override bool Equals(object obj)
        {
            var compareWith = obj as TreeNodeModel;
            if (compareWith == null)
                return false;

            return GetHashCode() == compareWith.GetHashCode();
        }

        public override int GetHashCode()
        {
            return new { Service, Account, FileSystemObject, IsDummy }.GetHashCode();
        }

        public void AddDummyChild()
        {
            if (IsHavingDummyChild)
                return;

            var child = _dummyChild = new TreeNodeModel(Service, Account, TreeNodeType.Dummy);
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
