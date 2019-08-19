using Jaya.Ui.Models;
using Jaya.Ui.Services;
using System;

namespace Jaya.Ui.ViewModels
{
    public class NavigationViewModel: ViewModelBase
    {
        public NavigationViewModel()
        {
            Node = new TreeNodeModel(null, null);
            Populate(Node);
        }

        public PaneConfigModel PaneConfig => GetService<ConfigurationService>().PaneConfiguration;

        public TreeNodeModel Node { get; }

        void OnNodeExpanded(TreeNodeModel node, bool isExpaded)
        {
            if (!isExpaded)
                return;

            if (node.IsHavingDummyChild)
            {
                node.RemoveDummyChild();
                Populate(node);
            }
        }

        void Populate(TreeNodeModel node)
        {
            if (node == null)
                throw new ArgumentNullException(nameof(node));

            if (node.IsExpanded)
                return;

            if (node.Service == null)
            {
                foreach(var service in GetService<ProviderService>().Services)
                {
                    var child = new TreeNodeModel(service, null);
                    child.Label = service.Name;
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
                child.NodeExpanded += OnNodeExpanded;
                node.Children.Add(child);

                child.AddDummyChild();
            }
        }
    }
}
