using Jaya.Shared.Base;
using Jaya.Ui.Services.Providers;
using System.Collections.Generic;

namespace Jaya.Ui.Services
{
    public class ProviderService
    {
        readonly List<ProviderServiceBase> _services;

        public ProviderService(FileSystemService fileSystemService)
        {
            _services = new List<ProviderServiceBase>
            {
                fileSystemService
            };
        }

        public IEnumerable<ProviderServiceBase> Services => _services;
    }
}
