using Jaya.Shared.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Jaya.Shared.Contracts
{
    public interface IConfigurationService
    {
        T Get<T>(string key = null) where T : ConfigModelBase;
        void Set<T>(T value, string key = null);
    }
}
