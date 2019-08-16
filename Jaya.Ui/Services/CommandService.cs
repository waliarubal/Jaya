using System;
using System.Collections.Generic;

namespace Jaya.Ui.Services
{
    public class CommandService
    {
        readonly ConfigurationService _configService;

        public CommandService(ConfigurationService configService)
        {
            _configService = configService;
        }

        void CommandAction(CommandType type, object parameter)
        {
            switch (type)
            {
                case CommandType.ToggleItemCheckBoxes:
                    _configService.ApplicationConfiguration.IsItemCheckBoxVisible = !_configService.ApplicationConfiguration.IsItemCheckBoxVisible;
                    break;

                case CommandType.ToggleFileNameExtensions:
                    _configService.ApplicationConfiguration.IsFileNameExtensionVisible = !_configService.ApplicationConfiguration.IsFileNameExtensionVisible;
                    break;

                case CommandType.ToggleHiddenItems:
                    _configService.ApplicationConfiguration.IsHiddenItemVisible = !_configService.ApplicationConfiguration.IsHiddenItemVisible;
                    break;

                case CommandType.ToggleToolbars:
                    _configService.ToolbarConfiguration.IsVisible = !_configService.ToolbarConfiguration.IsVisible;
                    break;

                case CommandType.ToggleToolbarFile:
                    _configService.ToolbarConfiguration.IsFileVisible = !_configService.ToolbarConfiguration.IsFileVisible;
                    break;

                case CommandType.ToggleToolbarEdit:
                    _configService.ToolbarConfiguration.IsEditVisible = !_configService.ToolbarConfiguration.IsEditVisible;
                    break;

                case CommandType.ToggleToolbarView:
                    _configService.ToolbarConfiguration.IsViewVisible = !_configService.ToolbarConfiguration.IsViewVisible;
                    break;

                case CommandType.ToggleToolbarHelp:
                    _configService.ToolbarConfiguration.IsHelpVisible = !_configService.ToolbarConfiguration.IsHelpVisible;
                    break;

                case CommandType.TogglePaneNavigation:
                    _configService.PaneConfiguration.IsNavigationPaneVisible = !_configService.PaneConfiguration.IsNavigationPaneVisible;
                    break;

                case CommandType.TogglePanePreview:
                    _configService.PaneConfiguration.IsPreviewPaneVisible = !_configService.PaneConfiguration.IsPreviewPaneVisible;
                    break;

                case CommandType.TogglePaneDetails:
                    _configService.PaneConfiguration.IsDetailsPaneVisible = !_configService.PaneConfiguration.IsDetailsPaneVisible;
                    break;
            }
        }

        public void SimpleCommandAction(CommandType type)
        {
            CommandAction(type, null);
        }

        public void ParameterizedCommandAction(object parameter)
        {
            if (parameter == null)
                throw new ArgumentNullException(nameof(parameter));

            var parameters = parameter as List<object>;
            if (parameters == null)
                throw new ArgumentException("Failed to parse command parameters.", nameof(parameter));

            CommandAction((CommandType)parameters[0], parameters[1]);
        }
    }
}
