using Avalonia.Markup.Xaml.Styling;
using Avalonia.Styling;
using Jaya.Shared.Base;
using System;
using System.Collections.Generic;

namespace Jaya.Shared.Models
{
    public class ThemeModel: ModelBase
    {
        public ThemeModel(string name, Uri[] themeUris)
        {
            Name = name;

            Styles = new List<IStyle>();
            foreach (Uri themeUri in themeUris)
            {
                Styles.Add(new StyleInclude(themeUri)
                {
                    Source = themeUri
                });
            }
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
