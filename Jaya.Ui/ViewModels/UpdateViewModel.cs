using Jaya.Shared.Base;
using System;

namespace Jaya.Ui.ViewModels
{
    public class UpdateViewModel: ViewModelBase
    {
        Version Version => Constants.VERSION;

        public string VersionString => string.Format("{0}.{1}.{2}.{3}", Version.Major, Version.Minor, Version.Build, Version.Revision);

        public byte Bitness => Environment.Is64BitOperatingSystem ? (byte)64 : (byte)32;
    }
}
