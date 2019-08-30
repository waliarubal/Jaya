using Avalonia.Controls;

namespace Jaya.Ui.Models
{
    public class PaneConfigModel : ModelBase
    {
        public bool IsPreviewOrDetailsPaneVisible => IsPreviewPaneVisible || IsDetailsPaneVisible;

        public GridLength NavigationPaneWidth
        {
            get => Get<GridLength>();
            private set => Set(value);
        }

        public GridLength PreviewOrDetailsPanePaneWidth
        {
            get => Get<GridLength>();
            private set => Set(value);
        }

        public double NavigationPaneWidthPx
        {
            get => Get<double>();
            set
            {
                Set(value);
                SetPaneWidths();
            }
        }

        public double PreviewOrDetailsPanePaneWidthPx
        {
            get => Get<double>();
            set
            {
                Set(value);
                SetPaneWidths();
            }
        }

        public bool IsNavigationPaneVisible
        {
            get => Get<bool>();
            set
            {
                Set(value);
                SetPaneWidths();
            }
        }

        public bool IsPreviewPaneVisible
        {
            get => Get<bool>();
            set
            {
                if (value && IsDetailsPaneVisible)
                    IsDetailsPaneVisible = !value;

                Set(value);
                SetPaneWidths();
            }
        }

        public bool IsDetailsPaneVisible
        {
            get => Get<bool>();
            set
            {
                if (value && IsPreviewPaneVisible)
                    IsPreviewPaneVisible = !value;

                Set(value);
                SetPaneWidths();
            }
        }

        public bool IsDetailsView
        {
            get => Get<bool>();
            set
            {
                if (value && IsThumbnailView)
                    IsThumbnailView = !value;

                Set(value);
            }
        }

        public bool IsThumbnailView
        {
            get => Get<bool>();
            set
            {
                if (value && IsDetailsView)
                    IsDetailsView = !value;

                Set(value);
            }
        }

        void SetPaneWidths()
        {
            RaisePropertyChanged(nameof(IsPreviewOrDetailsPaneVisible));

            if (IsPreviewOrDetailsPaneVisible)
                PreviewOrDetailsPanePaneWidth = new GridLength(PreviewOrDetailsPanePaneWidthPx, GridUnitType.Pixel);
            else
                PreviewOrDetailsPanePaneWidth = new GridLength(0, GridUnitType.Auto);

            if (IsNavigationPaneVisible)
                NavigationPaneWidth = new GridLength(NavigationPaneWidthPx, GridUnitType.Pixel);
            else
                NavigationPaneWidth = new GridLength(0, GridUnitType.Auto);
        }
    }
}
