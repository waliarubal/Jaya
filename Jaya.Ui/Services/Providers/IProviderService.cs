using Jaya.Ui.Models;

namespace Jaya.Ui.Services.Providers
{
    public interface IProviderService
    {
        bool IsRootDrive { get; }

        string Name { get;  }

        DirectoryModel GetDirectory(ProviderModel provider, string path = null);

        ProviderModel GetDefaultProvider();
    }
}
