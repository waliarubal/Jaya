using Jaya.Shared.Base;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Jaya.Provider.Ftp.Models
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
        public IList<AccountModel> Accounts { get; private set; }

        protected override ConfigModelBase Empty()
        {
            return new ConfigModel(null);
        }
    }
}
