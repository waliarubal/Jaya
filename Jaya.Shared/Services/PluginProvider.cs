using Jaya.Shared.Contracts;
using Jaya.Shared.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Jaya.Shared.Services
{
    public class PluginProvider : IPluginProvider
    {
        private readonly IEnumerable<IJayaPlugin> plugins;

        public PluginProvider(IEnumerable<IJayaPlugin> plugins)
        {
            this.plugins = plugins;
        }

        public IJayaPlugin[] GetPlugins() => this.plugins.ToArray();
    }
}
