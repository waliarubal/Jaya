using Jaya.Shared.Services;
using System.Collections.Generic;

namespace Jaya.Ui.Services
{
    public sealed class ProviderService : IService
    {
        public ProviderService(IEnumerable<IProviderService> providers)
        {
            Providers = providers;
        }

        public IEnumerable<IProviderService> Providers { get; }
    }
}
