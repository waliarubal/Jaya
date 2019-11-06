using Jaya.Provider.Dropbox.Services;
using Jaya.Shared.Models;
using Newtonsoft.Json;

namespace Jaya.Provider.Dropbox.Models
{
    public class DropboxProviderModel: AccountModelBase
    {
        public DropboxProviderModel(string id, string name): base(id, name)
        {
            ImagePath = "avares://Jaya.Provider.Dropbox/Assets/Images/Account-32.png";
        }

        [JsonProperty]
        public string Email
        {
            get => Get<string>();
            set => Set(value);
        }

        [JsonProperty]
        public string Token
        {
            get => Get<string>();
            set => Set(value);
        }
    }
}
