using Jaya.Shared.Services;
using System.Collections.Generic;
using System.Composition;

namespace Jaya.Ui.Services
{
    [Export(nameof(ProviderService), typeof(IService))]
    public sealed class ProviderService: IService
    {
        readonly List<IProviderService> _services;

        public ProviderService()
        {
            _services = new List<IProviderService>();
        }

        public IEnumerable<IProviderService> Services => _services;
    }
}
