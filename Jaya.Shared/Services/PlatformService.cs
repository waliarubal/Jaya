using System;
using System.Composition;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace Jaya.Shared.Services
{
    [Export(nameof(PlatformService), typeof(IService))]
    [Shared]
    public class PlatformService : IService
    {
        public void OpenBrowser(string url)
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                url = url.Replace("&", "^&"); // works on Windows and escape is needed for cmd.exe
                Process.Start(new ProcessStartInfo("cmd", $"/c start {url}"));
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                Process.Start("xdg-open", url); // works on Linux
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            {
                Process.Start("open", url); // not tested
            }
            else
                throw new NotImplementedException();
        }

        public OSPlatform GetPlatform()
        {

            if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
                return OSPlatform.OSX;

            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                return OSPlatform.Linux;

            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                return OSPlatform.Windows;

            // return windows for all others
            return OSPlatform.Windows;

        }
    }
}
