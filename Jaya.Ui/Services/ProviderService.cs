using Jaya.Shared.Base;
using Jaya.Shared.Services;
using System.Collections.Generic;

namespace Jaya.Ui.Services
{
    public sealed class ProviderService: IService
    {
        readonly List<ProviderServiceBase> _services;

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
