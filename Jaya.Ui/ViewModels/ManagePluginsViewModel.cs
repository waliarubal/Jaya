using Jaya.Ui.Base;
using Jaya.Ui.Services;
using System.Collections.Generic;

namespace Jaya.Ui.ViewModels
{
    public class ManagePluginsViewModel: ViewModelBase
    {
        public IEnumerable<ProviderServiceBase> Plugins => GetService<ProviderService>().Services;

        public ProviderServiceBase SelectedPlugin
        {
            get => Get<ProviderServiceBase>();
            set => Set(value);
        }
    }
}
