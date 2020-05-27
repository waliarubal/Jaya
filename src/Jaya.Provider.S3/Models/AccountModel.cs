using Jaya.Shared.Models;
using Newtonsoft.Json;

namespace Jaya.Provider.S3.Models
{
    public class AccountModel: AccountModelBase
    {
        public AccountModel(string id, string name): base(id, name)
        {
            
        }

        [JsonProperty]
        public string Email
        {
            get => Get<string>();
            set => Set(value);
        }
    }
}
