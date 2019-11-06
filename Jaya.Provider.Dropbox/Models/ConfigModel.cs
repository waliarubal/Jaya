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
            Providers = new ObservableCollection<DropboxProviderModel>();
        }

        [JsonConstructor]
        public ConfigModel(IEnumerable<DropboxProviderModel> accounts)
        {
            Providers = new ObservableCollection<DropboxProviderModel>(accounts);
        }

        [JsonProperty]
        public ObservableCollection<DropboxProviderModel> Providers { get; private set; }

        protected override ConfigModelBase Empty()
        {
            return new ConfigModel();
        }
    }
}
