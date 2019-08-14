using Jaya.Ui.Models;

namespace Jaya.Ui.Services
{
    [Service]
    public class ConfigurationService : ServiceBase
    {
        #region properties

        public ToolbarConfigModel ToolbarConfiguration { get; private set; }

        public PaneConfigModel PaneConfiguration { get; private set; }

        public ApplicationConfigModel ApplicationConfiguration { get; private set; }

        #endregion

        public override void Start()
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
                IsNavigationPaneVisible = true,
                IsDetailsPaneVisible = false,
                IsPreviewPaneVisible = false,
                NavigationPaneWidth = 220,
                PreviewOrDetailsPanePaneWidth = 220
            };
            ApplicationConfiguration = new ApplicationConfigModel();
        }

        public override void Stop()
        {

        }

    }
}
