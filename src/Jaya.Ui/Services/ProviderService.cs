using Jaya.Shared;
using Jaya.Shared.Services;
using System.Collections.Generic;

namespace Jaya.Ui.Services
{
    public sealed class ProviderService : IService
    {
        public ProviderService()
        {
            Providers = ServiceLocator.Instance.GetProviders();
        }

        public IEnumerable<IProviderService> Providers { get; }
    }
}
