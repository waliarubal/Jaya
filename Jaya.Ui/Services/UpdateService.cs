using Jaya.Shared.Services;
using Jaya.Ui.Models;
using RestSharp;
using RestSharp.Serializers.NewtonsoftJson;
using System;
using System.Composition;
using System.Reflection;
using System.Threading.Tasks;

namespace Jaya.Ui.Services
{
    [Shared]
    [Export(nameof(UpdateService), typeof(IService))]
    public sealed class UpdateService: IService
    {
        const string GITHUB_API = "https://api.github.com/";

        readonly SharedService _sharedService;
        readonly PlatformService _platformService;

        [ImportingConstructor]
        public UpdateService(
            [Import(nameof(SharedService))]IService sharedService,
            [Import(nameof(PlatformService))]IService platformService)
        {
            _sharedService = sharedService as SharedService;
            _platformService = platformService as PlatformService;

            Version = Assembly.GetExecutingAssembly().GetName().Version;
            VersionString = string.Format("{0}.{1}.{2}.{3}", Version.Major, Version.Minor, Version.Build, Version.Revision);
            Bitness = Environment.Is64BitOperatingSystem ? (byte)64 : (byte)32;
        }

        #region properties

        public Version Version { get; }

        public DateTime? Checked => _sharedService?.UpdateConfiguration.Checked;

        public ReleaseModel Update => _sharedService?.UpdateConfiguration.Update;

        public string VersionString { get; }

        public byte Bitness { get; }

        #endregion

        public async Task CheckForUpdate()
        {
            var client = new RestClient(GITHUB_API);
            client.UseNewtonsoftJson();

            var request = new RestRequest("repos/waliarubal/Jaya/releases", DataFormat.Json);

            var response = await client.GetAsync<ReleaseModel[]>(request);

            if (response.Length > 0 && Version.CompareTo(response[0].Version) < 0 && Update.Downloads != null && Update.Downloads.Length == 0)
                _sharedService.UpdateConfiguration.Update = response[0];

            _sharedService.UpdateConfiguration.Checked = DateTime.Now;
            _sharedService.SaveConfigurations();
        }

        public void DownloadUpdate()
        {
            if (Update == null )
                return;

            var platform = _platformService.GetPlatform();

            string downloadUrl = null;
            foreach(var download in Update.Downloads)
            {
                var x = platform.ToString();
                downloadUrl = download.Url;
            }

            var client = new RestClient(GITHUB_API);

            var request = new RestRequest(downloadUrl);
            request.ResponseWriter = responseStream =>
            {
                using (responseStream)
                {
                    
                }
            };

            var response = client.DownloadData(request);
        }
    }
}
