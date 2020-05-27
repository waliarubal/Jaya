//
// Copyright (c) Rubal Walia. All rights reserved.
// Licensed under the 3-Clause BSD license. See LICENSE file in the project root for full license information.
//
using System.Runtime.InteropServices;

namespace Jaya.Shared.Services
{
    public interface IPlatformService : IService
    {
        void OpenBrowser(string url);

        OSPlatform GetPlatform();
    }
}