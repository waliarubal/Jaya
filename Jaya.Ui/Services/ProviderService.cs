using Jaya.Shared.Base;
using System.Collections.Generic;

namespace Jaya.Ui.Services
{
    public sealed class ProviderService
    {
        readonly List<ProviderServiceBase> _services;

        public ProviderService()
        {
            _services = new List<ProviderServiceBase>();
        }

        public IEnumerable<ProviderServiceBase> Services => _services;
    }
}
