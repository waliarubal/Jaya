using Jaya.Shared.Base;
using Jaya.Shared.Contracts;
using Jaya.Shared.Services;
using Jaya.Shared.Services.Contracts;
using Jaya.Ui.Services;
using System;
using System.Collections.Generic;

namespace Jaya.Ui.ViewModels
{
    public class ManagePluginsViewModel: ViewModelBase
    {
        public IEnumerable<IJayaPlugin> Plugins => GetService<IPluginProvider>().GetPlugins();

        public IJayaPlugin SelectedPlugin
        {
            get => Get<IJayaPlugin>();
            set
            {
                if (Set(value))
                {
                    if (value == null || value.ConfigurationEditorType == null)
                        ConfigurationEditor = null;
                    else
                        ConfigurationEditor = Activator.CreateInstance(value.ConfigurationEditorType);

                    RaisePropertyChanged(nameof(IsPluginConfigurable));
                }
            }
        }

        public bool IsPluginConfigurable => ConfigurationEditor != null;

        public object ConfigurationEditor
        {
            get => Get<object>();
            private set => Set(value);
        }
    }
}
