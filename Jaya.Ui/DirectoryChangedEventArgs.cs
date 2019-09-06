using Jaya.Ui.Base;
using Jaya.Ui.Models;
using System;

namespace Jaya.Ui
{
    public class DirectoryChangedEventArgs: EventArgs
    {
        public DirectoryChangedEventArgs(ProviderServiceBase service, ProviderModel provider, DirectoryModel directory, NavigationDirection direction = NavigationDirection.Unknown)
        {
            Service = service;
            Provider = provider;
            Directory = directory;
            Direction = direction;
        }

        public NavigationDirection Direction { get; }

        public ProviderServiceBase Service { get; }

        public ProviderModel Provider { get; }

        public DirectoryModel Directory { get; }
    }
}
