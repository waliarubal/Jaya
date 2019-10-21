using Jaya.Provider.FileSystem.Services;
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
