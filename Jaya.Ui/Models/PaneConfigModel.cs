using Avalonia.Controls;
using Jaya.Shared.Base;
using Newtonsoft.Json;

namespace Jaya.Ui.Models
{
    public class PaneConfigModel : ConfigModelBase
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

        [JsonProperty]
        public double NavigationPaneWidthPx
        {
            get => Get<double>();
            set
            {
                Set(value);
                SetPaneWidths();
            }
        }

        [JsonProperty]
        public double PreviewOrDetailsPanePaneWidthPx
        {
            get => Get<double>();
            set
            {
                Set(value);
                SetPaneWidths();
            }
        }

        [JsonProperty]
        public bool IsNavigationPaneVisible
        {
            get => Get<bool>();
            set
            {
                Set(value);
                SetPaneWidths();
            }
        }

        [JsonProperty]
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

        [JsonProperty]
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

        [JsonProperty]
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

        [JsonProperty]
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

        [JsonProperty]
        public bool IsStatusBarVisible
        {
            get => Get<bool>();
            set => Set(value);
        }

        [JsonProperty]
        public bool IsRibbonVisible
        {
            get => Get<bool>();
            set => Set(value);
        }

        [JsonProperty]
        public bool IsRibbonCollapsed
        {
            get => Get<bool>();
            set => Set(value);
        }

        protected override ConfigModelBase Empty()
        {
            return new PaneConfigModel
            {
                NavigationPaneWidthPx = 220,
                PreviewOrDetailsPanePaneWidthPx = 240,
                IsNavigationPaneVisible = true,
                IsDetailsPaneVisible = false,
                IsPreviewPaneVisible = false,
                IsDetailsView = false,
                IsThumbnailView = true,
                IsStatusBarVisible = true,
                IsRibbonVisible = false,
                IsRibbonCollapsed = false
            };
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
