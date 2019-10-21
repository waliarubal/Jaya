using Jaya.Shared.Base;
using Jaya.Shared.Services;
using System.Collections.Generic;
using System.Composition;

namespace Jaya.Ui.Services
{
    [Export(typeof(IService))]
    public sealed class ProviderService: IService
    {
        readonly List<ProviderServiceBase> _services;

        [ImportingConstructor]
        public ProviderService()
        {
            _services = new List<ProviderServiceBase>
            {
                
            };
        }

        public string Name => nameof(ProviderService);

        public IEnumerable<ProviderServiceBase> Services => _services;
    }
}
