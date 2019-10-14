using Jaya.Ui.Models;
using Jaya.Ui.Models.Providers;
using System.Collections.Generic;

namespace Jaya.Ui.Services
{
    public class ConfigurationService
    {
        readonly Subscription<CommandType> _onSimpleCommand;
        readonly Subscription<KeyValuePair<CommandType, object>> _onParameterizedCommand;
        readonly CommandService _commandService;

        public ConfigurationService(CommandService commandService)
        {
            _commandService = commandService;
            _onSimpleCommand = commandService.EventAggregator.Subscribe<CommandType>(SimpleCommandAction);
            _onParameterizedCommand = commandService.EventAggregator.Subscribe<KeyValuePair<CommandType, object>>(ParameterizedCommandAction);

            LoadConfiguration();
        }

        ~ConfigurationService()
        {
            SaveConfiguration();

            _commandService.EventAggregator.UnSubscribe(_onSimpleCommand);
            _commandService.EventAggregator.UnSubscribe(_onParameterizedCommand);
        }

        #region properties

        public FileSystemServiceConfigModel FileSystemServiceConfiguration { get; private set; }

        public ToolbarConfigModel ToolbarConfiguration { get; private set; }

        public PaneConfigModel PaneConfiguration { get; private set; }

        public ApplicationConfigModel ApplicationConfiguration { get; private set; }

        #endregion

        void LoadConfiguration()
        {
            ToolbarConfiguration = new ToolbarConfigModel
            {
                IsFileVisible = true,
                IsEditVisible = true,
                IsViewVisible = true,
                IsHelpVisible = true,
                IsVisible = true
            };
            PaneConfiguration = new PaneConfigModel
            {
                NavigationPaneWidthPx = 220,
                PreviewOrDetailsPanePaneWidthPx = 240,
                IsNavigationPaneVisible = true,
                IsDetailsPaneVisible = false,
                IsPreviewPaneVisible = false,
                IsDetailsView = false,
                IsThumbnailView = true
            };
            ApplicationConfiguration = new ApplicationConfigModel();

            FileSystemServiceConfiguration = new FileSystemServiceConfigModel();
        }

        void SaveConfiguration()
        {

        }

        void SimpleCommandAction(CommandType type)
        {
            switch (type)
            {
                case CommandType.ToggleItemCheckBoxes:
                    ApplicationConfiguration.IsItemCheckBoxVisible = !ApplicationConfiguration.IsItemCheckBoxVisible;
                    break;

                case CommandType.ToggleFileNameExtensions:
                    ApplicationConfiguration.IsFileNameExtensionVisible = !ApplicationConfiguration.IsFileNameExtensionVisible;
                    break;

                case CommandType.ToggleHiddenItems:
                    ApplicationConfiguration.IsHiddenItemVisible = !ApplicationConfiguration.IsHiddenItemVisible;
                    break;

                case CommandType.ToggleToolbars:
                    ToolbarConfiguration.IsVisible = !ToolbarConfiguration.IsVisible;
                    break;

                case CommandType.ToggleToolbarFile:
                    ToolbarConfiguration.IsFileVisible = !ToolbarConfiguration.IsFileVisible;
                    break;

                case CommandType.ToggleToolbarEdit:
                    ToolbarConfiguration.IsEditVisible = !ToolbarConfiguration.IsEditVisible;
                    break;

                case CommandType.ToggleToolbarView:
                    ToolbarConfiguration.IsViewVisible = !ToolbarConfiguration.IsViewVisible;
                    break;

                case CommandType.ToggleToolbarHelp:
                    ToolbarConfiguration.IsHelpVisible = !ToolbarConfiguration.IsHelpVisible;
                    break;

                case CommandType.TogglePaneNavigation:
                    PaneConfiguration.IsNavigationPaneVisible = !PaneConfiguration.IsNavigationPaneVisible;
                    break;

                case CommandType.TogglePanePreview:
                    PaneConfiguration.IsPreviewPaneVisible = !PaneConfiguration.IsPreviewPaneVisible;
                    break;

                case CommandType.TogglePaneDetails:
                    PaneConfiguration.IsDetailsPaneVisible = !PaneConfiguration.IsDetailsPaneVisible;
                    break;
            }
        }

        void ParameterizedCommandAction(KeyValuePair<CommandType, object> parameter)
        {
            
        }

    }
}
