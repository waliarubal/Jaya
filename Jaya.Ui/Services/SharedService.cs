using Avalonia;
using Jaya.Shared;
using Jaya.Shared.Contracts;
using Jaya.Shared.Services;
using Jaya.Ui.Models;
using System.Collections.Generic;
using System.Composition;

namespace Jaya.Ui.Services
{
    public interface ISharedService
    {
        ApplicationConfigModel ApplicationConfiguration { get; }

        ToolbarConfigModel ToolbarConfiguration { get; }

        PaneConfigModel PaneConfiguration { get; }

        void LoadConfigurations();
        void SaveConfigurations();
    }

    //[Export(typeof(IService))]
    public sealed class SharedService : ISharedService
    {
        readonly Subscription<byte> _onSimpleCommand;
        readonly Subscription<KeyValuePair<byte, object>> _onParameterizedCommand;

        readonly ICommandService _commandService;
        readonly IConfigurationService _configService;

        public SharedService(ICommandService commandService, IConfigurationService configurationService)
        {
            _commandService = commandService;
            _configService = configurationService;

            _onSimpleCommand = _commandService.EventAggregator.Subscribe<byte>(SimpleCommandAction);
            _onParameterizedCommand = _commandService.EventAggregator.Subscribe<KeyValuePair<byte, object>>(ParameterizedCommandAction);
        }

        ~SharedService()
        {
            _commandService.EventAggregator.UnSubscribe(_onSimpleCommand);
            _commandService.EventAggregator.UnSubscribe(_onParameterizedCommand);
        }

        #region properties

        public ApplicationConfigModel ApplicationConfiguration { get; private set; }

        public ToolbarConfigModel ToolbarConfiguration { get; private set; }

        public PaneConfigModel PaneConfiguration { get; private set; }

        #endregion

        public void LoadConfigurations()
        {
            ApplicationConfiguration = _configService.Get<ApplicationConfigModel>();
            if (ApplicationConfiguration == null)
                ApplicationConfiguration = new ApplicationConfigModel();

            ToolbarConfiguration = _configService.Get<ToolbarConfigModel>();
            if (ToolbarConfiguration == null)
            {
                ToolbarConfiguration = new ToolbarConfigModel
                {
                    IsFileVisible = true,
                    IsEditVisible = true,
                    IsViewVisible = true,
                    IsHelpVisible = true,
                    IsVisible = true
                };
            }

            PaneConfiguration = _configService.Get<PaneConfigModel>();
            if (PaneConfiguration == null)
            {
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
            }
        }

        public void SaveConfigurations()
        {
            _configService.Set(ApplicationConfiguration);
            _configService.Set(ToolbarConfiguration);
            _configService.Set(PaneConfiguration);
        }

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

                case CommandType.Exit:
                    Application.Current.Exit();
                    break;
            }
        }

        void ParameterizedCommandAction(KeyValuePair<byte, object> parameter)
        {

        }
    }
}
