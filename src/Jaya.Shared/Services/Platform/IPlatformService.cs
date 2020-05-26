using System.Runtime.InteropServices;

namespace Jaya.Shared.Services
{
    public interface IPlatformService : IService
    {
        void OpenBrowser(string url);

        OSPlatform GetPlatform();
    }
}