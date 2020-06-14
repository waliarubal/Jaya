using Avalonia;
using Avalonia.Controls;
using Avalonia.Styling;
using Jaya.Shared.Base;
using Jaya.Shared.Models;
using System;
using System.Collections.Generic;

namespace Jaya.Shared
{
    public sealed class ThemeManager: ModelBase
    {
        static readonly object _syncLock;
        static ThemeManager _instance;
        readonly List<ThemeModel> _themes;
        readonly List<Window> _windows;
        ThemeModel _previousTheme = null;

        static ThemeManager()
        {
            _syncLock = new object();
        }

        private ThemeManager()
        {
            _windows = new List<Window>();

            _themes = new List<ThemeModel>
            {
                new ThemeModel("Light", new Uri[]
                {
                    new Uri("avares://Avalonia.Themes.Default/Accents/BaseLight.xaml"),
                    new Uri("avares://Jaya.Shared/Styles/Accents/BaseLight.xaml")
                }),
                new ThemeModel("Dark", new Uri[]
                {
                    new Uri("avares://Avalonia.Themes.Default/Accents/BaseDark.xaml"),
                    new Uri("avares://Jaya.Shared/Styles/Accents/BaseDark.xaml")
                })
            };

            SelectedTheme = _themes[0];
        }

        public static ThemeManager Instance
        {
            get
            {
                lock (_syncLock)
                {
                    if (_instance == null)
                        _instance = new ThemeManager();

                    return _instance;
                }
            }
        }

        public IEnumerable<ThemeModel> Themes => _themes;

        public ThemeModel SelectedTheme
        {
            get => Get<ThemeModel>();
            set
            {
                if (Design.IsDesignMode)
                    return;

                if (!Set(value) || value.Styles.Count == 0)
                    return;


                List<IStyle> styles = new List<IStyle>();


                styles.AddRange(Application.Current.Styles);
                /*for (int i = Application.Current.Styles.Count - 1; i >= 0; i--)
                    Application.Current.Styles.RemoveAt(i);*/
                Application.Current.Styles.Clear();

                if (_previousTheme != null)
                    styles.RemoveRange(0, _previousTheme.Styles.Count);

                styles.InsertRange(0, SelectedTheme.Styles);

                Application.Current.Styles.AddRange(styles);
                //Application.Current.Styles.Add(SelectedTheme.Style);

                _previousTheme = SelectedTheme;

                foreach (var window in _windows)
                {
                    window.Styles.Clear();
                    foreach (IStyle style in SelectedTheme.Styles)
                        window.Styles.Add(style);
                }
            }

        }

        public void EnableTheme(Window window)
        {
            if (SelectedTheme != null && SelectedTheme.Styles.Count > 0)
                foreach (IStyle style in SelectedTheme.Styles)
                    window.Styles.Add(style);

            window.Opened += (sender, e) =>
            {
                _windows.Add(window);

                if (SelectedTheme != null && SelectedTheme.Styles.Count > 0)
                    foreach (IStyle style in SelectedTheme.Styles)
                        window.Styles.Add(style);
            };

            window.Closing += (sender, e) =>
            {
                _windows.Remove(window);
            };
        }
    }
}
