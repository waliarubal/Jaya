using Avalonia;
using Jaya.Ui.ViewModels;
using Jaya.Ui.Views;
using System;

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
        readonly Subscription<OpenWindowArgs> _onOpenWindow;

        public CommandService()
        {
            EventAggregator = new EventAggregator();
            _onSimpleCommand = EventAggregator.Subscribe<CommandType>(SimpleCommandAction);
            _onOpenWindow = EventAggregator.Subscribe<OpenWindowArgs>(OpenWindowCommandAction);
        }

        ~CommandService()
        {
            EventAggregator.UnSubscribe(_onSimpleCommand);
            EventAggregator.UnSubscribe(_onOpenWindow);
        }

        public EventAggregator EventAggregator { get; }

        void SimpleCommandAction(CommandType type)
        {
            switch (type)
            {
                case CommandType.Exit:
                    Application.Current.Exit();
                    break;
            }
        }

        async void OpenWindowCommandAction(OpenWindowArgs parameter)
        {
            var child = Activator.CreateInstance(parameter.ChildType);
            if (child != null)
            {
                var windowView = new HostWindowView();
                ((HostWindowViewModel)windowView.DataContext).Child = child;
                windowView.Title = parameter.Title;
                windowView.Width = parameter.Width;
                windowView.Height = parameter.Height;
                await windowView.ShowDialog(Application.Current.MainWindow);
            }
        }
    }
}
