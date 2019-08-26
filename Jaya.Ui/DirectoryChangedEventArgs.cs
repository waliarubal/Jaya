using Jaya.Ui.Models;
using Jaya.Ui.Services.Providers;
using System;

namespace Jaya.Ui
{
    public class DirectoryChangedEventArgs: EventArgs
    {
        public DirectoryChangedEventArgs(IProviderService service, ProviderModel provider, DirectoryModel directory)
        {
            Service = service;
            Provider = provider;
            Directory = directory;
        }

        public IProviderService Service { get; }

        public ProviderModel Provider { get; }

        public DirectoryModel Directory { get; }
    }
}
