using Avalonia.Controls;
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

        static ThemeManager()
        {
            _syncLock = new object();
        }

        private ThemeManager()
        {
            _windows = new List<Window>();

            _themes = new List<ThemeModel>
            {
                new ThemeModel("Light", new Uri("avares://Jaya.Shared/Styles/Accents/BaseLight.xaml")),
                new ThemeModel("Dark", new Uri("avares://Jaya.Shared/Styles/Accents/BaseDark.xaml"))
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

                if (!Set(value) || value.Style == null)
                    return;

                foreach (var window in _windows)
                    window.Styles[0] = SelectedTheme.Style;
            }
        }

        public void EnableTheme(Window window)
        {
            if (SelectedTheme != null && SelectedTheme.Style != null)
                window.Styles.Add(SelectedTheme.Style);

            window.Opened += (sender, e) =>
            {
                _windows.Add(window);

                if (SelectedTheme != null && SelectedTheme.Style != null)
                    window.Styles[0] = SelectedTheme.Style;
            };

            window.Closing += (sender, e) =>
            {
                _windows.Remove(window);
            };
        }
    }
}
