using Jaya.Ui.Base;
using Jaya.Ui.Models.Providers;
using Newtonsoft.Json;

namespace Jaya.Ui.Models
{
    [JsonObject(MemberSerialization.OptIn)]
    class ConfigModel : ModelBase
    {
        public ConfigModel()
        {
            ApplicationConfiguration = new ApplicationConfigModel();
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
            
            FileSystemServiceConfiguration = new FileSystemServiceConfigModel();
        }

        [JsonConstructor]
        public ConfigModel(
            ApplicationConfigModel appConfig,
            ToolbarConfigModel toolbarConfig, 
            PaneConfigModel paneConfig, 
            FileSystemServiceConfigModel fileSystemServiceConfig)
        {
            ApplicationConfiguration = appConfig;
            ToolbarConfiguration = toolbarConfig;
            PaneConfiguration = paneConfig;

            FileSystemServiceConfiguration = fileSystemServiceConfig;
        }

        [JsonProperty]
        public ApplicationConfigModel ApplicationConfiguration { get; private set; }

        [JsonProperty]
        public ToolbarConfigModel ToolbarConfiguration { get; private set; }

        [JsonProperty]
        public PaneConfigModel PaneConfiguration { get; private set; }

        [JsonProperty]
        public FileSystemServiceConfigModel FileSystemServiceConfiguration { get; private set; }
    }
}
