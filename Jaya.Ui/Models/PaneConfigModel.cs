namespace Jaya.Ui.Models
{
    public class PaneConfigModel : ModelBase
    {
        public bool IsNavigationPaneVisible
        {
            get => Get<bool>();
            set => Set(value);
        }

        public bool IsPreviewOrDetailsPaneVisible => IsPreviewPaneVisible || IsDetailsPaneVisible;

        public float NavigationPaneWidth
        {
            get => Get<float>();
            set => Set(value);
        }

        public float PreviewOrDetailsPanePaneWidth
        {
            get => Get<float>();
            set => Set(value);
        }

        public bool IsPreviewPaneVisible
        {
            get => Get<bool>();
            set
            {
                if (value)
                    if (IsDetailsPaneVisible)
                        IsDetailsPaneVisible = !value;

                Set(value);
                RaisePropertyChanged(nameof(IsPreviewOrDetailsPaneVisible));
            }
        }

        public bool IsDetailsPaneVisible
        {
            get => Get<bool>();
            set
            {
                if (value)
                    if (IsPreviewPaneVisible)
                        IsPreviewPaneVisible = !value;

                Set(value);
                RaisePropertyChanged(nameof(IsPreviewOrDetailsPaneVisible));
            }
        }
    }
}
