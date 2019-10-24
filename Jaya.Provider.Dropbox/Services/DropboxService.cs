using Jaya.Shared.Base;
using Jaya.Shared.Models;
using Jaya.Shared.Services;
using System.Composition;

namespace Jaya.Provider.Dropbox.Services
{
    [Export(typeof(IProviderService))]
    public class DropboxService : ProviderServiceBase, IProviderService
    {
        public DropboxService()
        {
            Name = "Dropbox";
            ImagePath = "avares://Jaya.Provider.Dropbox/Assets/Images/Dropbox-32.png";
            Description = "View your Dropbox accounts, inspect their contents and play with directories & files stored within them.";
            IsRootDrive = true;
            //ConfigurationEditorType = typeof(ConfigurationView);

            //Configuration = ConfigurationService.Get<FileSystemConfigModel>();
            //if (Configuration == null)
            //    Configuration = new FileSystemConfigModel { IsProtectedFileVisible = false };
        }

        protected override ProviderModel GetDefaultProvider()
        {
            var provider = new ProviderModel("<Unknown Account>", this);
            provider.ImagePath = "avares://Jaya.Provider.Dropbox/Assets/Images/Dropbox-32.png";
            return provider;
        }

        protected override DirectoryModel GetDirectory(ProviderModel provider, string path = null)
        {
            return null;
        }
    }
}
