using Jaya.Ui.Models;
using Jaya.Ui.Services;
using System.Collections.ObjectModel;

namespace Jaya.Ui.ViewModels
{
    public class NavigationViewModel: ViewModelBase
    {
        public NavigationViewModel()
        {
            Services = new ObservableCollection<ProviderModel>();
        }

        public PaneConfigModel PaneConfig => GetService<ConfigurationService>().PaneConfiguration;

        public ObservableCollection<ProviderModel> Services { get; }

        //void PopulateNode(TreeNodeModel node)
        //{
        //    node.Children.Clear();

        //    if (node.Service == null)
        //    {
        //        foreach (var service in GetService<ProviderService>().Services)
        //        {
        //            var provider = new ProviderModel(service.Name, service);

        //            var childNode = new TreeNodeModel(service);
        //            childNode.Data = provider;
        //            childNode.Label = provider.Name;
        //            //childNode.AddDummyNode();
        //            node.Children.Add(childNode);
        //        }
        //    }
        //    else if (node.Data is ProviderModel)
        //    {
        //        var provider = node.Data as ProviderModel;
        //        provider.GetDirectory();

        //        foreach(var directory in provider.Directory.Directories)
        //        {
        //            var childNode = new TreeNodeModel(node.Service);
        //            childNode.Label = directory.Name;
        //            node.Children.Add(childNode);
        //        }
        //    }
        //}
    }
}
