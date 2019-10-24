using Jaya.Shared.Base;

namespace Jaya.Provider.Dropbox.Models
{
    public class ConfigModel : ConfigModelBase
    {
        protected override ConfigModelBase Empty()
        {
            return new ConfigModel();
        }
    }
}
