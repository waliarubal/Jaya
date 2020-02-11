using System;
using System.IO;
using System.Reflection;

namespace Jaya.Ui
{
    static class Constants
    {
        const string IMAGES_PATH_FORMAT = "avares://Jaya.Ui/Assets/Images/{0}";
        const string REPO_URL = "https://github.com/waliarubal/Jaya/";

        public const string APP_SHORT_NAME = "Jaya";
        public const string APP_NAME = "Jaya - Cross Plat";
        public const string APP_DESCRIPTION = "Jaya - Cross Plat is a small .NET Core based cross platform file explorer application which runs on Windows, Mac and Linux. Its goal is very simple, \"Allow browsing and managing of several file systems simultaneously using a single application which should work and look similar on all desktop platforms it supports.\".";

        public static readonly Uri
            URL_DONATION,
            URL_LICENSE,
            URL_ISSUES;

        public static readonly Version VERSION;
        public static readonly string DATA_DIRECTORY;

        static Constants()
        {
            URL_DONATION = new Uri("https://www.paypal.com/cgi-bin/webscr?cmd=_s-xclick&hosted_button_id=DEXCFJ6R48SR2");
            URL_LICENSE = new Uri("https://raw.githubusercontent.com/waliarubal/Jaya/dev/LICENSE");
            URL_ISSUES = GetRepositoryUrl("issues");
            VERSION = Assembly.GetExecutingAssembly().GetName().Version;
            DATA_DIRECTORY = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), APP_SHORT_NAME);
        }

        static Uri GetRepositoryUrl(string urlFragment)
        {
            return new Uri(string.Format("{0}{1}", REPO_URL, urlFragment));
        }

        public static string GetImageUrl(this string fileName)
        {
            return string.Format(IMAGES_PATH_FORMAT, fileName);
        }
    }

    internal enum AccountAction : byte
    {
        Added,
        Removed
    }

    public enum ItemType : byte
    {
        Service,
        Account,
        Computer,
        Drive,
        Directory,
        File,
        Dummy
    }

    public enum CommandType : byte
    {
        Exit,
        ToggleToolbars,
        ToggleToolbarFile,
        ToggleToolbarEdit,
        ToggleToolbarView,
        ToggleToolbarHelp,
        TogglePaneNavigation,
        TogglePanePreview,
        TogglePaneDetails,
        ToggleItemCheckBoxes,
        ToggleFileNameExtensions,
        ToggleHiddenItems
    }
}