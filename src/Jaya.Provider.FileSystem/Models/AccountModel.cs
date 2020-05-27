//
// Copyright (c) Rubal Walia. All rights reserved.
// Licensed under the 3-Clause BSD license. See LICENSE file in the project root for full license information.
//
using Jaya.Shared.Models;
using System;

namespace Jaya.Provider.FileSystem.Models
{
    public class AccountModel: AccountModelBase
    {
        public AccountModel() : base(Environment.MachineName, Environment.MachineName)
        {
            
        }
    }
}
