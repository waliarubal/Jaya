using Jaya.Shared.Models;
using Newtonsoft.Json;

namespace Jaya.Provider.Ftp.Models
{
    public class AccountModel: AccountModelBase
    {
        public AccountModel(string id, string name): base(id, name)
        {
            
        }

        [JsonProperty]
        public string Host
        {
            get => Get<string>();
            set
            {
                if (Set(value))
                    UpdateIdAndName();
            }
        }

        [JsonProperty]
        public int Port
        {
            get => Get<int>();
            set
            {
                if (Set(value))
                    UpdateIdAndName();
            }
        }

        [JsonProperty]
        public bool IsAnonymous
        {
            get => Get<bool>();
            set => Set(value);
        }

        [JsonProperty]
        public string UserName
        {
            get => Get<string>();
            set => Set(value);
        }

        [JsonProperty]
        public string Password
        {
            get => Get<string>();
            set => Set(value);
        }

        void UpdateIdAndName()
        {
            Name = Id = string.Format("{0}:{1}", Host, Port);
        }

        public static AccountModel Empty()
        {
            var account = new AccountModel(string.Empty, string.Empty)
            {
                Port = 21
            };
            return account;
        }
    }
}
