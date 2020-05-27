//
// Copyright (c) Rubal Walia. All rights reserved.
// Licensed under the 3-Clause BSD license. See LICENSE file in the project root for full license information.
//
using Jaya.Shared.Base;
using Jaya.Shared.Models;

namespace Jaya.Ui.ViewModels.Windows
{
    public class HostViewModel : ViewModelBase
    {
        public WindowOptionsModel Option
        {
            get => Get<WindowOptionsModel>();
            set => Set(value);
        }
    }
}
