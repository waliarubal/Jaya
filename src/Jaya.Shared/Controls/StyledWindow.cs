//
// Copyright (c) Rubal Walia. All rights reserved.
// Licensed under the 3-Clause BSD license. See LICENSE file in the project root for full license information.
//
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

            SetupSide("TopLeft", StandardCursorType.TopLeftCorner, WindowEdge.NorthWest, ref e);
            SetupSide("TopCenter", StandardCursorType.TopSide, WindowEdge.North, ref e);
            SetupSide("TopRight", StandardCursorType.TopRightCorner, WindowEdge.NorthEast, ref e);
            SetupSide("MiddleRight", StandardCursorType.RightSide, WindowEdge.East, ref e);
            SetupSide("BottomRight", StandardCursorType.BottomRightCorner, WindowEdge.SouthEast, ref e);
            SetupSide("BottomCenter", StandardCursorType.BottomSide, WindowEdge.South, ref e);
            SetupSide("BottomLeft", StandardCursorType.BottomLeftCorner, WindowEdge.SouthWest, ref e);
            SetupSide("MiddleLeft", StandardCursorType.LeftSide, WindowEdge.West, ref e);

            Border titlebar = GetControl<Border>(e, "PART_TitleBar");
            titlebar.PointerPressed += (sender, args) => PlatformImpl?.BeginMoveDrag(args);
            titlebar.DoubleTapped += (sneder, args) =>
            {
                if (CanResize && (!IsModal))
                    WindowState = WindowState == WindowState.Maximized ? WindowState.Normal : WindowState.Maximized;
            };

            _closeButton = GetControl<Button>(e, "PART_Close");
            _closeButton.Click += (object sender, RoutedEventArgs arg) => Close();

            var isNotModal = !IsModal;

            _minimizeButton = GetControl<Button>(e, "PART_Minimize");
            _minimizeButton.IsVisible = isNotModal;
            _minimizeButton.Click += (sneder, args) => WindowState = WindowState.Minimized;

            _maximizeButton = GetControl<Button>(e, "PART_Maximize");
            _maximizeButton.IsVisible = isNotModal;
            _maximizeButton.Click += (sneder, args) => WindowState = WindowState == WindowState.Maximized ? WindowState.Normal : WindowState.Maximized;

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

        void SetupSide(string name, StandardCursorType cursor, WindowEdge edge, ref TemplateAppliedEventArgs e)
        {
            var control = e.NameScope.Get<Control>("PART_" + name + "Edge");
            control.Cursor = new Cursor(cursor);
            control.PointerPressed += (sender, ep) => BeginResizeDrag(edge, ep);
        }

        T GetControl<T>(TemplateAppliedEventArgs e, string name) where T : Control
        {
            return e.NameScope.Find<T>(name);
        }
    }
}
