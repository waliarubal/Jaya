using Avalonia;
using Avalonia.Controls;
using System;
using System.Collections.Generic;

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
        ToggleHiddenItems,
        OpenWindow,
        CloseWindow
    }

    public class CommandService
    {
        readonly Subscription<CommandType> _onSimpleCommand;
        readonly Subscription<KeyValuePair<CommandType, object>> _onParameterizedCommand;

        public CommandService()
        {
            EventAggregator = new EventAggregator();
            _onSimpleCommand = EventAggregator.Subscribe<CommandType>(SimpleCommandAction);
            _onParameterizedCommand = EventAggregator.Subscribe<KeyValuePair<CommandType, object>>(ParameterizedCommandAction);
        }

        ~CommandService()
        {
            EventAggregator.UnSubscribe(_onSimpleCommand);
            EventAggregator.UnSubscribe(_onParameterizedCommand);
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

        async void ParameterizedCommandAction(KeyValuePair<CommandType, object> parameter)
        {
            switch(parameter.Key)
            {
                case CommandType.OpenWindow:
                    var window = Activator.CreateInstance(parameter.Value as Type) as Window;
                    if (window != null)
                        await window.ShowDialog(Application.Current.MainWindow);
                    break;

                case CommandType.CloseWindow:
                    var windowToClose = parameter.Value as Window;
                    if (windowToClose != null)
                        windowToClose.Close(true);
                    break;
            }
        }
    }
}
