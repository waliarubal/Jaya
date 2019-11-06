using Jaya.Shared.Services;
using System.Collections.Generic;
using System.Composition;

namespace Jaya.Ui.Services
{
    [Export(nameof(ProviderService), typeof(IService))]
    [Shared]
    public sealed class ProviderService : IService
    {
        [ImportingConstructor]
        public ProviderService([ImportMany]IEnumerable<IProviderService> providers)
        {
            Providers = providers;
        }

        public IEnumerable<IProviderService> Providers { get; }
    }
}
