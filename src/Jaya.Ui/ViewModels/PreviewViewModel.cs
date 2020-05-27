//
// Copyright (c) Rubal Walia. All rights reserved.
// Licensed under the 3-Clause BSD license. See LICENSE file in the project root for full license information.
//
using Jaya.Shared.Base;
using Jaya.Ui.Models;
using Jaya.Ui.Services;

namespace Jaya.Ui.ViewModels
{
    public class PreviewViewModel : ViewModelBase
    {
        readonly SharedService _shared;

        public PreviewViewModel()
        {
            _shared = GetService<SharedService>();
        }

        public PaneConfigModel PaneConfig => _shared.PaneConfiguration;
    }
}
