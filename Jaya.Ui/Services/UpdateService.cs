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

        [ImportingConstructor]
        public UpdateService([Import(nameof(SharedService))]IService sharedService)
        {
            _sharedService = sharedService as SharedService;

            Version = Assembly.GetExecutingAssembly().GetName().Version;
            VersionString = string.Format("{0}.{1}.{2}.{3}", Version.Major, Version.Minor, Version.Build, Version.Revision);
            Bitness = Environment.Is64BitOperatingSystem ? (byte)64 : (byte)32;
        }

        public Version Version { get; }

        public DateTime? Checked => _sharedService?.ApplicationConfiguration.UpdateChecked;

        public ReleaseModel Update => _sharedService?.ApplicationConfiguration.Update;

        public string VersionString { get; }

        public byte Bitness { get; }

        public async Task CheckForUpdate()
        {
            var client = new RestClient(GITHUB_API);
            client.UseNewtonsoftJson();

            var request = new RestRequest("repos/waliarubal/Jaya/releases", DataFormat.Json);

            var response = await client.GetAsync<ReleaseModel[]>(request);

            if (response.Length > 0)
                _sharedService.ApplicationConfiguration.Update = response[0];

            _sharedService.ApplicationConfiguration.UpdateChecked = DateTime.Now;
            _sharedService.SaveConfigurations();
        }
    }
}
