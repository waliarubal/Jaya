using Jaya.Shared.Base;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Jaya.Provider.S3.Models
{
    public class ConfigModel : ConfigModelBase
    {
        public ConfigModel()
        {
            Accounts = new List<AccountModel>();
        }

        [JsonConstructor]
        public ConfigModel(IEnumerable<AccountModel> accounts): this()
        {
            if (accounts != null)
                Accounts = new List<AccountModel>(accounts);
        }

        [JsonProperty]
        public int PageSize
        {
            get => Get<int>();
            set => Set(value);
        }

        [JsonProperty]
        public IList<AccountModel> Accounts { get; private set; }

        protected override ConfigModelBase Empty()
        {
            return new ConfigModel(null)
            {
                PageSize = 1000
            };
        }
    }
}
