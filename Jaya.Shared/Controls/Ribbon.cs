using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Styling;
using System;
using System.Windows.Input;

namespace Jaya.Shared.Controls
{
    /// <summary>
    /// Refer https://www.powerworld.com/WebHelp/Content/MainDocumentation_HTML/Ribbons.htm for details.
    /// </summary>
    public class Ribbon : TabControl, IStyleable
    {
        public static readonly DirectProperty<Ribbon, bool> IsExpandedProperty;
        public static readonly DirectProperty<Ribbon, ICommand> HelpButtonCommandProperty;

        Button _toggleButton;
        ICommand _helpCommand;
        bool _isExpanded;
        ISelectable _selectedTab;

        static Ribbon()
        {
            IsExpandedProperty = AvaloniaProperty.RegisterDirect<Ribbon, bool>(nameof(IsExpanded), o => o.IsExpanded, (o, v) => o.IsExpanded = v, true);
            HelpButtonCommandProperty = AvaloniaProperty.RegisterDirect<Ribbon, ICommand>(nameof(HelpButtonCommand), o => o.HelpButtonCommand, (o, v) => o.HelpButtonCommand = v);
        }

        public bool IsExpanded
        {
            get => _isExpanded;
            internal set => SetAndRaise(IsExpandedProperty, ref _isExpanded, value);
        }

        public ICommand HelpButtonCommand
        {
            get => _helpCommand;
            set => SetAndRaise(HelpButtonCommandProperty, ref _helpCommand, value);
        }

        protected override void OnPropertyChanged(AvaloniaPropertyChangedEventArgs e)
        {
            switch(e.Property.Name)
            {
                case nameof(SelectedItem):
                    if (e.NewValue == null || IsExpanded)
                        break;

                    IsExpanded = true;
                    break;
            }

            base.OnPropertyChanged(e);
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
