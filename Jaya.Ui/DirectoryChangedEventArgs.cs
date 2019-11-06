using Jaya.Shared.Base;
using Jaya.Shared.Models;
using System;

namespace Jaya.Ui
{
    public enum NavigationDirection : byte
    {
        Backward,
        Forward,
        Unknown
    }

    public class DirectoryChangedEventArgs : EventArgs
    {
        public DirectoryChangedEventArgs(ProviderServiceBase service, ProviderAccountModelBase provider, DirectoryModel directory, NavigationDirection direction = NavigationDirection.Unknown)
        {
            Service = service;
            Provider = provider;
            Directory = directory;
            Direction = direction;
        }

        public NavigationDirection Direction { get; }

        public ProviderServiceBase Service { get; }

        public ProviderAccountModelBase Provider { get; }

        public DirectoryModel Directory { get; }

        public override int GetHashCode()
        {
            return Direction.GetHashCode() + Service.GetHashCode() + Directory.GetHashCode();
        }

        public DirectoryChangedEventArgs Clone(NavigationDirection direction)
        {
            return new DirectoryChangedEventArgs(Service, Provider, Directory, direction);
        }
    }
}
