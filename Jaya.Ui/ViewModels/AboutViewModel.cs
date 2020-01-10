using Jaya.Shared.Base;
using System;

namespace Jaya.Ui.ViewModels
{
    public class AboutViewModel: ViewModelBase
    {
        public string Name => Constants.APP_NAME;

        public string Description => Constants.APP_DESCRIPTION;

        Version Version => new Version(0,0,0,0);

        public string VersionString => string.Format("{0}.{1}.{2}.{3}", Version.Major, Version.Minor, Version.Build, Version.Revision);

        public byte Bitness => Environment.Is64BitOperatingSystem ? (byte)64 : (byte)32;

        public Uri DonationUrl => Constants.URL_DONATION;

        public Uri IssuesUrl => Constants.URL_ISSUES;

        public Uri LicenseUrl => Constants.URL_LICENSE;

        public Uri ReleasesUrl => Constants.URL_LICENSE;
    }
}
