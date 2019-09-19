using Jaya.Ui.Base;
using Jaya.Ui.Services;
using System.Collections.Generic;

namespace Jaya.Ui.ViewModels
{
    public class PluginsWindowViewModel: ViewModelBase
    {
        public IEnumerable<ProviderServiceBase> Plugins => GetService<ProviderService>().Services;
    }
}
