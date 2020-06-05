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
            _themes = new List<ThemeModel>
            {
                new ThemeModel("Light", new Uri("avares://Jaya.Shared/Styles/Colors/BaseLight.xaml")),
                new ThemeModel("Dark", new Uri("avares://Jaya.Shared/Styles/Colors/BaseDark.xaml"))
            };

            _windows = new List<Window>();
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

        internal void EnableTheme(Window window)
        {
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
