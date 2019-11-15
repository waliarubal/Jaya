using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Input;
using Avalonia.Media;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using Avalonia.Styling;
using System;
using System.IO;

namespace Jaya.Shared
{
    public class StyledWindow : Window, IStyleable
    {
        const string DEFAULT_ICON = "avares://Jaya.Shared/Assets/Logo.ico";
        const string MAXIMIZE_PATH_DATA = "M28,2h-6c-1.104,0-2,0.896-2,2s0.896,2,2,2h1.2l-4.6,4.601C18.28,10.921,18,11.344,18,12c0,1.094,0.859,2,2,2  c0.641,0,1.049-0.248,1.4-0.6L26,8.8V10c0,1.104,0.896,2,2,2s2-0.896,2-2V4C30,2.896,29.104,2,28,2z M12,18  c-0.641,0-1.049,0.248-1.4,0.6L6,23.2V22c0-1.104-0.896-2-2-2s-2,0.896-2,2v6c0,1.104,0.896,2,2,2h6c1.104,0,2-0.896,2-2  s-0.896-2-2-2H8.8l4.6-4.601C13.72,21.079,14,20.656,14,20C14,18.906,13.141,18,12,18z";
        const string RESTORE_PATH_DATA = "M70,0H29.9C24.4,0,20,4.4,20,9.9V50c0,5.5,4.5,10,10,10h40c5.5,0,10-4.5,10-10V10C80,4.5,75.5,0,70,0z M70,50H30V10h40V50z    M10,40H0v30c0,5.5,4.5,10,10,10h30V70H10V40z";

        public static StyledProperty<object> HeaderContentProperty;
        public static StyledProperty<bool> IsModalProperty;
        Button _closeButton, _minimizeButton, _maximizeButton;
        Image _icon;
        //Avalonia.Controls.Shapes.Path _maximizeIcon;
        bool _isTemplateApplied;

        static StyledWindow()
        {
            HeaderContentProperty = AvaloniaProperty.Register<StyledWindow, object>(nameof(HeaderContent));
            IsModalProperty = AvaloniaProperty.Register<StyledWindow, bool>(nameof(HeaderContent));
            PseudoClass<StyledWindow, WindowState>(WindowStateProperty, (WindowState state) => state == WindowState.Maximized, "maximized");
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
            _closeButton.Click += delegate { Close(); };

            var isNotModal = !IsModal;

            _minimizeButton = GetControl<Button>(e, "PART_Minimize");
            _minimizeButton.IsVisible = isNotModal;
            _minimizeButton.Click += delegate { WindowState = WindowState.Minimized; };

            _maximizeButton = GetControl<Button>(e, "PART_Maximize");
            _maximizeButton.IsVisible = isNotModal;
            _maximizeButton.Click += delegate { WindowState = WindowState == WindowState.Maximized ? WindowState.Normal : WindowState.Maximized; };

            //_maximizeIcon = GetControl<Avalonia.Controls.Shapes.Path>(e, "PART_MaximizeIcon");

            _icon = GetControl<Image>(e, "PART_Icon");
            _icon.Source = GetIcon(Icon);

            _isTemplateApplied = true;
        }

        protected override void OnPropertyChanged(AvaloniaPropertyChangedEventArgs e)
        {
            base.OnPropertyChanged(e);

            if (!_isTemplateApplied)
                return;

            switch(e.Property.Name)
            {
                case nameof(Icon):
                    _icon.Source = GetIcon(Icon);
                    break;

                //case nameof(WindowState):
                //    if (e.NewValue.Equals(WindowState.Maximized))
                //    {
                //        _maximizeIcon.Data = PathGeometry.Parse(RESTORE_PATH_DATA);
                //        _maximizeButton.SetValue(ToolTip.TipProperty, "Restore");
                //    }
                //    else
                //    {
                //        _maximizeIcon.Data = PathGeometry.Parse(MAXIMIZE_PATH_DATA);
                //        _maximizeButton.SetValue(ToolTip.TipProperty, "Maximize");
                //    }
                //    break;
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
            control.PointerPressed += (sender, args) => PlatformImpl?.BeginResizeDrag(edge, args);;
        }

        T GetControl<T>(TemplateAppliedEventArgs e, string name) where T : Control
        {
            return e.NameScope.Find<T>(name);
        }
    }
}
