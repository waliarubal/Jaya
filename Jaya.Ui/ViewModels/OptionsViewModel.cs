using Avalonia.ThemeManager;
using Jaya.Shared.Base;
using System.Collections.Generic;

namespace Jaya.Ui.ViewModels
{
    public class OptionsViewModel: ViewModelBase
    {
        public IList<ITheme> Themes => App.ThemeSelector.Themes;
    }
}
