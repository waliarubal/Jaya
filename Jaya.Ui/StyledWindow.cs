using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Input;
using Avalonia.Styling;
using System;

namespace Jaya.Ui
{
    public class StyledWindow : Window, IStyleable
    {
        public static StyledProperty<object> HeaderContentProperty;
        public static StyledProperty<bool> IsModalProperty;
        Button _closeButton, _minimizeButton, _maximizeButton;

        static StyledWindow()
        {
            HeaderContentProperty = AvaloniaProperty.Register<StyledWindow, object>(nameof(HeaderContent));
            IsModalProperty = AvaloniaProperty.Register<StyledWindow, bool>(nameof(HeaderContent));
        }

        protected override void OnTemplateApplied(TemplateAppliedEventArgs e)
        {
            base.OnTemplateApplied(e);

            SetupWindowEdge(e, "PART_RightGrip", StandardCursorType.RightSide, WindowEdge.East);
            SetupWindowEdge(e, "PART_LeftGrip", StandardCursorType.LeftSide, WindowEdge.West);
            SetupWindowEdge(e, "PART_TopGrip", StandardCursorType.TopSide, WindowEdge.North);
            SetupWindowEdge(e, "PART_BottomGrip", StandardCursorType.BottomSize, WindowEdge.South);
            SetupWindowEdge(e, "PART_TopLeftGrip", StandardCursorType.TopLeftCorner, WindowEdge.NorthWest);
            SetupWindowEdge(e, "PART_TopRightGrip", StandardCursorType.TopRightCorner, WindowEdge.NorthEast);
            SetupWindowEdge(e, "PART_BottomLeftGrip", StandardCursorType.BottomLeftCorner, WindowEdge.SouthWest);
            SetupWindowEdge(e, "PART_BottomRightGrip", StandardCursorType.BottomRightCorner, WindowEdge.SouthEast);

            GetControl<Border>(e, "PART_TitleBar").PointerPressed += delegate { PlatformImpl?.BeginMoveDrag(); };

            _closeButton = GetControl<Button>(e, "PART_Close");
            _closeButton.Click += delegate { Close(); };

            _minimizeButton = GetControl<Button>(e, "PART_Minimize");
            _minimizeButton.IsVisible = !IsModal;
            _minimizeButton.Click += delegate { WindowState = WindowState.Minimized; };

            _maximizeButton = GetControl<Button>(e, "PART_Maximize");
            _maximizeButton.IsVisible = !IsModal;
            _maximizeButton.Click += delegate { WindowState = WindowState == WindowState.Maximized ? WindowState.Normal : WindowState.Maximized; };
        }

        public object HeaderContent
        {
            get => GetValue(HeaderContentProperty);
            set => SetValue(HeaderContentProperty, value);
        }

        public bool IsModal
        {
            get => GetValue(IsModalProperty);
            set
            {
                SetValue(IsModalProperty, value);

                if (value)
                {
                    CanResize = false;
                    ShowInTaskbar = false;
                    WindowStartupLocation = WindowStartupLocation.CenterOwner;

                    if (_minimizeButton != null)
                        _minimizeButton.IsVisible = false;
                    if (_maximizeButton != null)
                        _maximizeButton.IsVisible = false;
                }
                else
                {
                    CanResize = true;
                    ShowInTaskbar = true;
                    WindowStartupLocation = WindowStartupLocation.CenterScreen;

                    if (_minimizeButton != null)
                        _minimizeButton.IsVisible = true;
                    if (_maximizeButton != null)
                        _maximizeButton.IsVisible = true;
                }
            }
        }

        Type IStyleable.StyleKey => typeof(StyledWindow);

        void SetupWindowEdge(TemplateAppliedEventArgs e, string name, StandardCursorType cursor, WindowEdge edge)
        {
            var control = GetControl<Border>(e, name);
            control.Cursor = new Cursor(cursor);
            control.PointerPressed += delegate { PlatformImpl?.BeginResizeDrag(edge); };
        }

        T GetControl<T>(TemplateAppliedEventArgs e, string name) where T : Control
        {
            return e.NameScope.Find<T>(name);
        }
    }
}
