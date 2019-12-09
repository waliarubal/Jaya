namespace Jaya.Ui
{
    static class Constants
    {
        public const string IMAGES_PATH_FORMAT = "avares://Jaya.Ui/Assets/Images/{0}";
        public const string APP_NAME = "Jaya";

        public static readonly string
            COLLAPSED_IMAGE_URL,
            EXPANDED_IMAGE_URL;


        static Constants()
        {
            COLLAPSED_IMAGE_URL = "Folder-16.png".GetImageUrl();
            EXPANDED_IMAGE_URL = "Folder-Open-16.png".GetImageUrl();
        }

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
        ToggleHiddenItems,
        ChangeTheme
    }
}
