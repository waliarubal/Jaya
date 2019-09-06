namespace Jaya.Ui.Services
{
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

    public class CommandService
    {
        public CommandService()
        {
            EventAggregator = new EventAggregator();
        }

        public EventAggregator EventAggregator { get; }
    }
}
