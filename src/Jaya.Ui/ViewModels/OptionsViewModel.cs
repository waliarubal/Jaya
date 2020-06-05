//
// Copyright (c) Rubal Walia. All rights reserved.
// Licensed under the 3-Clause BSD license. See LICENSE file in the project root for full license information.
//
using Jaya.Shared;
using Jaya.Shared.Base;
using Jaya.Shared.Models;
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

        public IEnumerable<ThemeModel> Themes => ThemeManager.Instance.Themes;

        public ApplicationConfigModel ApplicationConfig => _shared.ApplicationConfiguration;

        public PaneConfigModel PaneConfig => _shared.PaneConfiguration;
    }
}
