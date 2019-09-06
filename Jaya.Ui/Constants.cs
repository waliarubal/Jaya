namespace Jaya.Ui
{
    public enum FileSystemObjectType : byte
    {
        File,
        Directory,
        Drive
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

    public enum NavigationDirection: byte
    {
        Backward,
        Forward,
        Unknown
    }
}
