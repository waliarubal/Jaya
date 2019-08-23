using Jaya.Ui.Models;

namespace Jaya.Ui.Services
{
    public class ConfigurationService
    {
        public ConfigurationService()
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
                IsPreviewPaneVisible = false
            };
            ApplicationConfiguration = new ApplicationConfigModel();
        }

        #region properties

        public ToolbarConfigModel ToolbarConfiguration { get; private set; }

        public PaneConfigModel PaneConfiguration { get; private set; }

        public ApplicationConfigModel ApplicationConfiguration { get; private set; }

        #endregion

    }
}
