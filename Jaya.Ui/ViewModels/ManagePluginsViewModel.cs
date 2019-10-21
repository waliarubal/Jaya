using Jaya.Shared.Base;
using Jaya.Ui.Services;
using System;
using System.Collections.Generic;

namespace Jaya.Ui.ViewModels
{
    public class ManagePluginsViewModel: ViewModelBase
    {
        public IEnumerable<ProviderServiceBase> Plugins => GetService<ProviderService>().Services as IEnumerable<ProviderServiceBase>;

        public ProviderServiceBase SelectedPlugin
        {
            get => Get<ProviderServiceBase>();
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
