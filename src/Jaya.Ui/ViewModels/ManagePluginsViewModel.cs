//
// Copyright (c) Rubal Walia. All rights reserved.
// Licensed under the 3-Clause BSD license. See LICENSE file in the project root for full license information.
//
using Jaya.Shared.Base;
using Jaya.Shared.Services;
using Jaya.Ui.Services;
using System;
using System.Collections.Generic;

namespace Jaya.Ui.ViewModels
{
    public class ManagePluginsViewModel : ViewModelBase
    {
        object _configurationEditor;

        public IEnumerable<IProviderService> Plugins => GetService<ProviderService>().Providers;

        public IProviderService SelectedPlugin
        {
            get => Get<IProviderService>();
            set
            {
                if (value == null)
                    ConfigurationEditor = null;

                if (Set(value))
                {
                    if (value.ConfigurationEditorType == null)
                        ConfigurationEditor = null;
                    else
                        ConfigurationEditor = Activator.CreateInstance(value.ConfigurationEditorType);
                }

                RaisePropertyChanged(nameof(IsPluginConfigurable));
            }
        }

        public bool IsPluginConfigurable => ConfigurationEditor != null;

        public object ConfigurationEditor
        {
            get => _configurationEditor;
            private set
            {
                _configurationEditor = value;
                RaisePropertyChanged();
            }
        }
    }
}
