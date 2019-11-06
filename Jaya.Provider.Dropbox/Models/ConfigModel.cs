using Jaya.Shared.Base;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Jaya.Provider.Dropbox.Models
{
    public class ConfigModel : ConfigModelBase
    {
        public ConfigModel()
        {
            Providers = new ObservableCollection<AccountModel>();
        }

        [JsonConstructor]
        public ConfigModel(IEnumerable<AccountModel> accounts)
        {
            Providers = new ObservableCollection<AccountModel>(accounts);
        }

        [JsonProperty]
        public ObservableCollection<AccountModel> Providers { get; private set; }

        protected override ConfigModelBase Empty()
        {
            return new ConfigModel();
        }
    }
}
