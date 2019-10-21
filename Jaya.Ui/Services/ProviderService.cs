using Jaya.Shared.Services;
using System.Collections.Generic;
using System.Composition;

namespace Jaya.Ui.Services
{
    [Export(typeof(ProviderService))]
    public sealed class ProviderService: IService
    {
        readonly List<IPorviderService> _services;

        public ProviderService()
        {
            _services = new List<IPorviderService>();
            _services.AddRange(ServiceLocator.Instance.Providers);
        }

        public IEnumerable<IPorviderService> Services => _services;
    }
}
