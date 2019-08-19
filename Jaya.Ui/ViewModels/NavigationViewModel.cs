using Jaya.Ui.Models;
using Jaya.Ui.Services;
using System;

namespace Jaya.Ui.ViewModels
{
    public class NavigationViewModel : ViewModelBase
    {
        public NavigationViewModel()
        {
            Node = new TreeNodeModel(null, null);
            Populate(Node);
        }

        public PaneConfigModel PaneConfig => GetService<ConfigurationService>().PaneConfiguration;

        public TreeNodeModel Node { get; }

        public TreeNodeModel SelectedNode
        {
            get => Get<TreeNodeModel>();
            set => Set(value);
        }

        void OnNodeExpanded(TreeNodeModel node, bool isExpaded)
        {
            if (!isExpaded)
                return;

            if (node.IsHavingDummyChild)
                Populate(node);
        }

        void Populate(TreeNodeModel node)
        {
            if (node == null)
                throw new ArgumentNullException(nameof(node));

            if (node.IsExpanded && !node.IsHavingDummyChild)
                return;

            node.RemoveDummyChild();

            if (node.Service == null)
            {
                foreach (var service in GetService<ProviderService>().Services)
                {
                    var child = new TreeNodeModel(service, null);
                    child.Label = service.Name;
                    child.ImagePath = service.ImagePath;
                    child.NodeExpanded += OnNodeExpanded;
                    node.Children.Add(child);

                    child.AddDummyChild();
                }
            }
            else if (node.Provider == null)
            {
                var provider = node.Service.GetDefaultProvider();

                var child = new TreeNodeModel(node.Service, provider);
                child.Label = provider.Name;
                child.ImagePath = provider.ImagePath;
                child.NodeExpanded += OnNodeExpanded;
                node.Children.Add(child);

                child.AddDummyChild();
            }
            else
            {
                var currentDirectory = node.Service.GetDirectory(node.Provider, node.FileSystemObject?.Path);
                foreach (var directory in currentDirectory.Directories)
                {
                    var child = new TreeNodeModel(node.Service, node.Provider);
                    child.Label = directory.Name;
                    child.FileSystemObject = directory;
                    child.NodeExpanded += OnNodeExpanded;
                    node.Children.Add(child);

                    child.AddDummyChild();
                }
            }
        }
    }
}
