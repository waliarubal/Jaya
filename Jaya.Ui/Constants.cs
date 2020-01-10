namespace Jaya.Ui
{
    static class Constants
    {
        public const string IMAGES_PATH_FORMAT = "avares://Jaya.Ui/Assets/Images/{0}";
        public const string APP_NAME = "Jaya - Cross Plat";
        public const string APP_DESCRIPTION = "Jaya - Cross Plat is a small .NET Core based cross platform file explorer application which runs on Windows, Mac and Linux. Its goal is very simple, \"Allow browsing and managing of several file systems simultaneously using a single application which should work and look similar on all desktop platforms it supports.\".";

        public static string GetImageUrl(this string fileName)
        {
            return string.Format(IMAGES_PATH_FORMAT, fileName);
        }
    }

    internal enum AccountAction: byte
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
