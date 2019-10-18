using Jaya.Shared.Base;
using Jaya.Ui.Models;
using Jaya.Ui.Services;
using System.Windows.Input;

namespace Jaya.Ui.ViewModels
{
    public class ToolbarViewModel : ViewModelBase
    {
        readonly SharedService _shared;
        ICommand _openWindow;

        public ToolbarViewModel()
        {
            _shared = GetService<SharedService>();
        }

        public ToolbarConfigModel ToolbarConfig => _shared.ToolbarConfiguration;

        public PaneConfigModel PaneConfig => _shared.PaneConfiguration;

        public ApplicationConfigModel ApplicationConfig => _shared.ApplicationConfiguration;

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
