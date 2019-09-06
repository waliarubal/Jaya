using Jaya.Ui.Base;
using Jaya.Ui.Models;
using System;

namespace Jaya.Ui
{
    public class DirectoryChangedEventArgs: EventArgs
    {
        public DirectoryChangedEventArgs(ProviderServiceBase service, ProviderModel provider, DirectoryModel directory)
        {
            Service = service;
            Provider = provider;
            Directory = directory;
        }

        public ProviderServiceBase Service { get; }

        public ProviderModel Provider { get; }

        public DirectoryModel Directory { get; }
    }
}
