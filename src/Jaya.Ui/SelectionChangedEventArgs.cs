//
// Copyright (c) Rubal Walia. All rights reserved.
// Licensed under the 3-Clause BSD license. See LICENSE file in the project root for full license information.
//
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

    public class SelectionChangedEventArgs : EventArgs
    {
        public SelectionChangedEventArgs(ProviderServiceBase service, AccountModelBase account, DirectoryModel directory, NavigationDirection direction = NavigationDirection.Unknown)
        {
            Service = service;
            Account = account;
            Directory = directory;
            Direction = direction;
        }

        public NavigationDirection Direction { get; }

        public ProviderServiceBase Service { get; }

        public AccountModelBase Account { get; }

        public DirectoryModel Directory { get; }

        public override int GetHashCode()
        {
            return new { Direction, Service, Account, Directory }.GetHashCode();
        }

        public SelectionChangedEventArgs Clone(NavigationDirection direction)
        {
            return new SelectionChangedEventArgs(Service, Account, Directory, direction);
        }
    }
}
