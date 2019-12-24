using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using Avalonia.Styling;
using System;
using System.IO;

namespace Jaya.Shared.Controls
{
    public class StyledWindow : Window, IStyleable
    {
        const string DEFAULT_ICON = "avares://Jaya.Shared/Assets/Logo.ico";

        public static readonly StyledProperty<object> HeaderContentProperty;
        public static readonly StyledProperty<bool> IsModalProperty;
        Button _closeButton, _minimizeButton, _maximizeButton;
        Image _icon;
        bool _isTemplateApplied;

        static StyledWindow()
        {
            HeaderContentProperty = AvaloniaProperty.Register<StyledWindow, object>(nameof(HeaderContent));
            IsModalProperty = AvaloniaProperty.Register<StyledWindow, bool>(nameof(IsModal));
        }

        protected override void OnTemplateApplied(TemplateAppliedEventArgs e)
        {
            base.OnTemplateApplied(e);

            SetupWindowEdge(e, "PART_RightGrip", StandardCursorType.RightSide, WindowEdge.East);
            SetupWindowEdge(e, "PART_LeftGrip", StandardCursorType.LeftSide, WindowEdge.West);
            SetupWindowEdge(e, "PART_TopGrip", StandardCursorType.TopSide, WindowEdge.North);
            SetupWindowEdge(e, "PART_BottomGrip", StandardCursorType.BottomSide, WindowEdge.South);
            SetupWindowEdge(e, "PART_TopLeftGrip", StandardCursorType.TopLeftCorner, WindowEdge.NorthWest);
            SetupWindowEdge(e, "PART_TopRightGrip", StandardCursorType.TopRightCorner, WindowEdge.NorthEast);
            SetupWindowEdge(e, "PART_BottomLeftGrip", StandardCursorType.BottomLeftCorner, WindowEdge.SouthWest);
            SetupWindowEdge(e, "PART_BottomRightGrip", StandardCursorType.BottomRightCorner, WindowEdge.SouthEast);

            GetControl<Border>(e, "PART_TitleBar").PointerPressed += (sender, args) => PlatformImpl?.BeginMoveDrag(args);

            _closeButton = GetControl<Button>(e, "PART_Close");
            _closeButton.Click += (object sender, RoutedEventArgs arg) => Close();

            var isNotModal = !IsModal;

            _minimizeButton = GetControl<Button>(e, "PART_Minimize");
            _minimizeButton.IsVisible = isNotModal;
            _minimizeButton.Click += delegate { WindowState = WindowState.Minimized; };

            _maximizeButton = GetControl<Button>(e, "PART_Maximize");
            _maximizeButton.IsVisible = isNotModal;
            _maximizeButton.Click += delegate { WindowState = WindowState == WindowState.Maximized ? WindowState.Normal : WindowState.Maximized; };

            _icon = GetControl<Image>(e, "PART_Icon");
            if (Icon == null)
            {
                var uri = new Uri(DEFAULT_ICON, UriKind.Absolute);
                var assets = AvaloniaLocator.Current.GetService<IAssetLoader>();
                using (var stream = assets.Open(uri))
                {
                    stream.Seek(0, SeekOrigin.Begin);
                    _icon.Source = new Bitmap(stream);
                }

                Icon = new WindowIcon(_icon.Source);
            }
            else
                _icon.Source = GetIcon(Icon);

            _isTemplateApplied = true;
        }

        protected override void OnPropertyChanged(AvaloniaPropertyChangedEventArgs e)
        {
            base.OnPropertyChanged(e);

            if (!_isTemplateApplied)
                return;

            switch (e.Property.Name)
            {
                case nameof(Icon):
                    _icon.Source = GetIcon(Icon);
                    break;
            }
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

                var inverseValue = !value;

                ShowInTaskbar = inverseValue;
                WindowStartupLocation = inverseValue ? WindowStartupLocation.CenterScreen : WindowStartupLocation.CenterOwner;

                if (!_isTemplateApplied)
                    return;

                _minimizeButton.IsVisible = inverseValue;
                _maximizeButton.IsVisible = inverseValue;
            }
        }

        Type IStyleable.StyleKey => typeof(StyledWindow);

        Bitmap GetIcon(WindowIcon icon)
        {
            if (icon == null)
            {
                var uri = new Uri(DEFAULT_ICON, UriKind.Absolute);
                var assets = AvaloniaLocator.Current.GetService<IAssetLoader>();
                using (var stream = assets.Open(uri))
                {
                    stream.Seek(0, SeekOrigin.Begin);
                    return new Bitmap(stream);
                }
            }
            else
            {
                using (var stream = new MemoryStream())
                {
                    icon.Save(stream);
                    stream.Seek(0, SeekOrigin.Begin);
                    return new Bitmap(stream);
                }
            }
        }

        void SetupWindowEdge(TemplateAppliedEventArgs e, string name, StandardCursorType cursor, WindowEdge edge)
        {
            var control = GetControl<Border>(e, name);
            control.Cursor = new Cursor(cursor);
            control.PointerPressed += (sender, args) => PlatformImpl?.BeginResizeDrag(edge, args); ;
        }

        T GetControl<T>(TemplateAppliedEventArgs e, string name) where T : Control
        {
            return e.NameScope.Find<T>(name);
        }
    }
}
