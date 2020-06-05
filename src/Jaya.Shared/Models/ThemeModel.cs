using Avalonia.Markup.Xaml.Styling;
using Avalonia.Styling;
using Jaya.Shared.Base;
using System;

namespace Jaya.Shared.Models
{
    public class ThemeModel: ModelBase
    {
        public ThemeModel(string name, Uri themeUri)
        {
            Name = name;
            Style = new StyleInclude(themeUri)
            {
                Source = themeUri
            };
        }

        public string Name
        {
            get => Get<string>();
            private set => Set(value);
        }

        public IStyle Style
        {
            get => Get<IStyle>();
            private set => Set(value);
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
