using Jaya.Shared.Base;
using Jaya.Shared.Contracts;
using System;

namespace Jaya.Shared.Services.Contracts
{
    public interface IPluginProvider
    {
        IJayaPlugin[] GetPlugins();
    }
}
