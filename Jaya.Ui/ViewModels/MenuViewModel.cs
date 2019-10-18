using Jaya.Shared.Base;
using Jaya.Ui.Models;
using Jaya.Ui.Services;
using System.Windows.Input;

namespace Jaya.Ui.ViewModels
{
    public class MenuViewModel : ViewModelBase
    {
        readonly ConfigurationService _configService;
        ICommand _openWindow;

        public MenuViewModel()
        {
            _configService = GetService<ConfigurationService>();
        }

        public ToolbarConfigModel ToolbarConfig => _configService.ToolbarConfiguration;

        public PaneConfigModel PaneConfig => _configService.PaneConfiguration;

        public ApplicationConfigModel ApplicationConfig => _configService.ApplicationConfiguration;

        public ICommand OpenWindowCommand
        {
            get
            {
                if (_openWindow == null)
                    _openWindow = GetService<NavigationService>().OpenWindowCommand;

                return _openWindow;
            }
        }

    }
}
