using Newtonsoft.Json;

namespace Jaya.Ui.Models
{
    public class ReleaseAssetModel
    {
        [JsonProperty("size")]
        public long Size { get; set; }

        [JsonProperty("browser_download_url")]
        public string Url { get; set; }

        public override string ToString()
        {
            return Url;
        }
    }
}
