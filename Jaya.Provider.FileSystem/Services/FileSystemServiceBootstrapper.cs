using Jaya.Shared.Services;
using Microsoft.Extensions.DependencyInjection;
using Prise.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;

namespace Jaya.Provider.FileSystem.Services
{
    [PluginBootstrapper(PluginType = typeof(FileSystemService))]
    public class FileSystemServiceBootstrapper : IPluginBootstrapper
    {
        public IServiceCollection Bootstrap(IServiceCollection services)
        {
            return services;
        }
    }
}
