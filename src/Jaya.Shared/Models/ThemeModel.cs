using Avalonia.Markup.Xaml.Styling;
using Avalonia.Styling;
using Jaya.Shared.Base;
using System;
using System.Collections.Generic;

namespace Jaya.Shared.Models
{
    public class ThemeModel : ModelBase
    {
        public ThemeModel(string name, params Uri[] themeStyleUris)
        {
            Name = name;

            var styles = new List<IStyle>();
            foreach (var styleUri in themeStyleUris)
            {
                var style = new StyleInclude(styleUri) { Source = styleUri };
                styles.Add(style);
            }
            Styles = styles;
        }

        public string Name
        {
            get => Get<string>();
            private set => Set(value);
        }

        public IList<IStyle> Styles
        {
            get => Get<IList<IStyle>>();
            private set => Set(value);
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
