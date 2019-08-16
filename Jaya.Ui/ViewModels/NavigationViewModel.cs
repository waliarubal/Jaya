using Jaya.Ui.Models;
using Jaya.Ui.Services;
using System.Collections.ObjectModel;

namespace Jaya.Ui.ViewModels
{
    public class NavigationViewModel: ViewModelBase
    {
        public NavigationViewModel()
        {
            Providers = new ObservableCollection<ProviderModel>();
            Populate();
        }

        public PaneConfigModel PaneConfig => GetService<ConfigurationService>().PaneConfiguration;

        public ObservableCollection<ProviderModel> Providers { get; }

        void Populate(object node = null)
        {

            if (node == null)
            {
                foreach (var service in GetService<ProviderService>().Services)
                {
                    var provider = new ProviderModel(service.Name, service);
                    provider.GetDirectory();
                    Providers.Add(provider);
                }
            }
            else if (node is ProviderModel)
            {
                var provider = node as ProviderModel;
                provider.GetDirectory();
            }
        }
    }
}
