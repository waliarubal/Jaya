//
// Copyright (c) Rubal Walia. All rights reserved.
// Licensed under the 3-Clause BSD license. See LICENSE file in the project root for full license information.
//
using Jaya.Shared.Base;
using Jaya.Ui.Models;
using Jaya.Ui.Services;
using System.Windows.Input;
using Jaya.Shared;

namespace Jaya.Ui.ViewModels
{
    public class MenuViewModel : ViewModelBase
    {
        readonly SharedService _shared;
        ICommand _openWindow;

        public MenuViewModel()
        {
            _shared = GetService<SharedService>();
            SimpleCommand = new RelayCommand<byte>(_shared.SimpleCommandAction);
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
