//
// Copyright (c) Rubal Walia. All rights reserved.
// Licensed under the 3-Clause BSD license. See LICENSE file in the project root for full license information.
//
using Jaya.Shared;
using Jaya.Shared.Services;
using System.Collections.Generic;

namespace Jaya.Ui.Services
{
    public sealed class ProviderService : IService
    {
        public ProviderService()
        {
            Providers = ServiceLocator.Instance.GetProviders();
        }

        public IEnumerable<IProviderService> Providers { get; }
    }
}
