//
// Copyright (c) Rubal Walia. All rights reserved.
// Licensed under the 3-Clause BSD license. See LICENSE file in the project root for full license information.
//
namespace Jaya.Shared.Services
{
    public interface IMemoryCacheService : IService
    {
        bool TryGetValue<T>(object key, out T result);

        void Set<T>(object key, T value);
    }
}