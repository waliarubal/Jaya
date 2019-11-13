using Jaya.Shared.Models;
using Newtonsoft.Json;

namespace Jaya.Provider.GoogleDrive.Models
{
    public class AccountModel: AccountModelBase
    {
        public AccountModel(string id, string name): base(id, name)
        {
            ImagePath = "avares://Jaya.Provider.GoogleDrive/Assets/Images/Account-32.png";
        }

        [JsonProperty]
        public string Email
        {
            get => Get<string>();
            set => Set(value);
        }
    }
}
