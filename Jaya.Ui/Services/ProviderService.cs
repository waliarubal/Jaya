using Jaya.Ui.Services.Providers;
using System.Collections.Generic;

namespace Jaya.Ui.Services
{
    public class ProviderService
    {
        readonly List<IProviderService> _services;

        public ProviderService(FileSystemService fileSystemService)
        {
            _services = new List<IProviderService>
            {
                fileSystemService
            };
        }

        public IEnumerable<IProviderService> Services => _services;
    }
}
