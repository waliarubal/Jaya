using Jaya.Ui.Models;
using Jaya.Ui.Models.Providers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace Jaya.Ui.Services
{
    public class ConfigurationService
    {
        [JsonObject(MemberSerialization.OptIn)]
        private class Config
        {
            [JsonProperty]
            internal ApplicationConfigModel ApplicationConfiguration { get; set; }

            [JsonProperty]
            internal ToolbarConfigModel ToolbarConfiguration { get; set; }

            [JsonProperty]
            internal PaneConfigModel PaneConfiguration { get; set; }

            [JsonProperty]
            internal FileSystemServiceConfigModel FileSystemServiceConfiguration { get; set; }
        }

        const string CONFIGURATION_FILE_NAME = "configuration.json";

        readonly Subscription<CommandType> _onSimpleCommand;
        readonly Subscription<KeyValuePair<CommandType, object>> _onParameterizedCommand;
        readonly CommandService _commandService;

        public ConfigurationService(CommandService commandService)
        {
            ConfigurationFilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Jaya", CONFIGURATION_FILE_NAME);

            _commandService = commandService;
            _onSimpleCommand = commandService.EventAggregator.Subscribe<CommandType>(SimpleCommandAction);
            _onParameterizedCommand = commandService.EventAggregator.Subscribe<KeyValuePair<CommandType, object>>(ParameterizedCommandAction);
        }

        ~ConfigurationService()
        {
            _commandService.EventAggregator.UnSubscribe(_onSimpleCommand);
            _commandService.EventAggregator.UnSubscribe(_onParameterizedCommand);
        }

        #region properties

        public FileSystemServiceConfigModel FileSystemServiceConfiguration { get; private set; }

        public ToolbarConfigModel ToolbarConfiguration { get; private set; }

        public PaneConfigModel PaneConfiguration { get; private set; }

        public ApplicationConfigModel ApplicationConfiguration { get; private set; }

        public string ConfigurationFilePath { get; private set; }

        #endregion

        internal void LoadConfiguration()
        {
            if (File.Exists(ConfigurationFilePath))
            {
                Config settings;
                using (var reader = File.OpenText(ConfigurationFilePath))
                {
                    var serializer = new JsonSerializer { Formatting = Formatting.None };
                    settings = serializer.Deserialize(reader, typeof(Config)) as Config;
                }

                ApplicationConfiguration = settings.ApplicationConfiguration;
                ToolbarConfiguration = settings.ToolbarConfiguration;
                PaneConfiguration = settings.PaneConfiguration;

                FileSystemServiceConfiguration = settings.FileSystemServiceConfiguration;
            }
            else
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
        }


        internal void SaveConfiguration()
        {
            var settings = new Config
            {
                ApplicationConfiguration = ApplicationConfiguration,
                ToolbarConfiguration = ToolbarConfiguration,
                PaneConfiguration = PaneConfiguration,
                FileSystemServiceConfiguration = FileSystemServiceConfiguration
            };

            // create configuration directory if missing
            var fileInfo = new FileInfo(ConfigurationFilePath);
            if (!fileInfo.Directory.Exists)
                Directory.CreateDirectory(fileInfo.DirectoryName);

            using (var writer = File.CreateText(ConfigurationFilePath))
            {
                var serializer = new JsonSerializer { Formatting = Formatting.None };
                serializer.Serialize(writer, settings, typeof(Config));
            }
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
