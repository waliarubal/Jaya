using Jaya.Shared.Services;
using System;
using System.Composition;
using System.Reflection;

namespace Jaya.Ui.Services
{
    [Shared]
    [Export(nameof(UpdateService), typeof(IService))]
    public sealed class UpdateService: IService
    {
        public UpdateService()
        {
            Version = Assembly.GetExecutingAssembly().GetName().Version;
            VersionString = string.Format("{0}.{1}.{2}.{3}", Version.Major, Version.Minor, Version.Build, Version.Revision);
            Bitness = Environment.Is64BitOperatingSystem ? (byte)64 : (byte)32;
        }

        public Version Version { get; }

        public string VersionString { get; }

        public byte Bitness { get; }
    }
}
