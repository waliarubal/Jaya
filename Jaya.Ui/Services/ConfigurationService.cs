using Jaya.Shared;
using Jaya.Shared.Services;
using Jaya.Ui.Models;
using Jaya.Ui.Models.Providers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace Jaya.Ui.Services
{
    public sealed class ConfigurationService
    {
        const string CONFIGURATION_FILE_NAME = "configuration.json";

        readonly Subscription<byte> _onSimpleCommand;
        readonly Subscription<KeyValuePair<byte, object>> _onParameterizedCommand;
        readonly CommandService _commandService;
        ConfigModel _config;

        public ConfigurationService(CommandService commandService)
        {
            ConfigurationFilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Jaya", CONFIGURATION_FILE_NAME);

            _commandService = commandService;
            _onSimpleCommand = commandService.EventAggregator.Subscribe<byte>(SimpleCommandAction);
            _onParameterizedCommand = commandService.EventAggregator.Subscribe<KeyValuePair<byte, object>>(ParameterizedCommandAction);
        }

        ~ConfigurationService()
        {
            _commandService.EventAggregator.UnSubscribe(_onSimpleCommand);
            _commandService.EventAggregator.UnSubscribe(_onParameterizedCommand);
        }

        #region properties

        public string ConfigurationFilePath { get; private set; }

        public ApplicationConfigModel ApplicationConfiguration => _config.ApplicationConfiguration;

        public ToolbarConfigModel ToolbarConfiguration => _config.ToolbarConfiguration;

        public PaneConfigModel PaneConfiguration => _config.PaneConfiguration;

        public FileSystemServiceConfigModel FileSystemServiceConfiguration => _config.FileSystemServiceConfiguration;

        #endregion

        void SimpleCommandAction(byte type)
        {
            switch ((CommandType)type)
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

        void ParameterizedCommandAction(KeyValuePair<byte, object> parameter)
        {

        }

        internal void LoadConfiguration()
        {
            var fileInfo = new FileInfo(ConfigurationFilePath);
            if (fileInfo.Exists)
            {
                using (var reader = File.OpenText(fileInfo.FullName))
                {
                    var serializer = new JsonSerializer { Formatting = Formatting.None };
                    _config = serializer.Deserialize(reader, typeof(ConfigModel)) as ConfigModel;
                }
            }
            else
                _config = new ConfigModel();
        }


        internal void SaveConfiguration()
        {
            // create configuration directory if missing
            var fileInfo = new FileInfo(ConfigurationFilePath);
            if (!fileInfo.Directory.Exists)
                Directory.CreateDirectory(fileInfo.DirectoryName);

            using (var writer = File.CreateText(fileInfo.FullName))
            {
                var serializer = new JsonSerializer { Formatting = Formatting.None };
                serializer.Serialize(writer, _config, typeof(ConfigModel));
            }
        }

    }
}
