using System.Runtime.InteropServices;

namespace Jaya.Shared.Services
{
    public interface IPlatformService : IService
    {
        OSPlatform GetPlatform();
    }
}