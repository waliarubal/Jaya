using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Styling;
using System;

namespace Jaya.Shared
{
    /// <summary>
    /// Refer https://www.powerworld.com/WebHelp/Content/MainDocumentation_HTML/Ribbons.htm for details.
    /// </summary>
    public class Ribbon : TabControl, IStyleable
    {
        public static readonly StyledProperty<bool> IsExpandedProperty;
        Button _toggleButton;
        ISelectable _selectedTab;

        static Ribbon()
        {
            IsExpandedProperty = AvaloniaProperty.Register<Ribbon, bool>(nameof(IsExpanded), true);
        }

        public bool IsExpanded
        {
            get => GetValue(IsExpandedProperty);
            internal set => SetValue(IsExpandedProperty, value);
        }

        Type IStyleable.StyleKey => typeof(Ribbon);

        protected override void OnTemplateApplied(TemplateAppliedEventArgs e)
        {
            base.OnTemplateApplied(e);

            _toggleButton = GetControl<Button>(e, "PART_ToggleButton");
            _toggleButton.Click += delegate
            {
                IsExpanded = !IsExpanded;

                if (IsExpanded)
                {
                    if (_selectedTab != null)
                    {
                        _selectedTab.IsSelected = true;
                        _selectedTab = null;
                    }
                }
                else
                {
                    if (SelectedItem != null)
                    {
                        _selectedTab = SelectedItem as ISelectable;
                        _selectedTab.IsSelected = false;
                    }
                }
            };
        }

        T GetControl<T>(TemplateAppliedEventArgs e, string name) where T : Control
        {
            return e.NameScope.Find<T>(name);
        }
    }
}
