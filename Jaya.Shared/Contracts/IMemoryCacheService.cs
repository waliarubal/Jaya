using System;
using System.Collections.Generic;
using System.Text;

namespace Jaya.Shared.Contracts
{
    public interface IMemoryCacheService
    {
        bool TryGetValue<T>(object key, out T result);
        void Set<T>(object key, T value);
        long Count { get; }
    }
}
