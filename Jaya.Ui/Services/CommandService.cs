using Avalonia;

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
        readonly Subscription<CommandType> _onSimpleCommand;

        public CommandService()
        {
            EventAggregator = new EventAggregator();
            _onSimpleCommand = EventAggregator.Subscribe<CommandType>(SimpleCommandAction);
        }

        ~CommandService()
        {
            EventAggregator.UnSubscribe(_onSimpleCommand);
        }

        public EventAggregator EventAggregator { get; }

        void SimpleCommandAction(CommandType type)
        {
            switch (type)
            {
                case CommandType.Exit:
                    App.Lifetime.Shutdown();
                    break;
            }
        }
    }
}
