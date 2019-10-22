using Jaya.Shared.Services;
using System.Collections.Generic;
using System.Composition;

namespace Jaya.Ui.Services
{
    [Export(nameof(ProviderService), typeof(IService))]
    public sealed class ProviderService : IService
    {
        [ImportingConstructor]
        public ProviderService([ImportMany]IEnumerable<IProviderService> services)
        {
            Services = services;
        }

        public IEnumerable<IProviderService> Services { get; }
    }
}
