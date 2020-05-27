//
// Copyright (c) Rubal Walia. All rights reserved.
// Licensed under the 3-Clause BSD license. See LICENSE file in the project root for full license information.
//
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Jaya.Shared.Models;

namespace Jaya.Shared.Services
{
    public interface IProviderService
    {
        bool IsRootDrive { get; }

        string Name { get; }

        string Description { get; }

        string ImagePath { get; }

        Type ConfigurationEditorType { get; }

        Task<IEnumerable<AccountModelBase>> GetAccountsAsync();

        Task<DirectoryModel> GetDirectoryAsync(AccountModelBase account, DirectoryModel directory = null);

        Task FormatAsync(AccountModelBase account, DirectoryModel directory = null);
    }

}