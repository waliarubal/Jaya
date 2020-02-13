using Avalonia.ThemeManager;
using Jaya.Shared.Base;
using Jaya.Ui.Models;
using Jaya.Ui.Services;
using System.Collections.Generic;

namespace Jaya.Ui.ViewModels
{
    public class OptionsViewModel: ViewModelBase
    {
        readonly SharedService _shared;

        public OptionsViewModel()
        {
            _shared = GetService<SharedService>();
        }

        public IList<ITheme> Themes => App.ThemeSelector.Themes;

        public ApplicationConfigModel ApplicationConfig => _shared.ApplicationConfiguration;

        public PaneConfigModel PaneConfig => _shared.PaneConfiguration;
    }
}
