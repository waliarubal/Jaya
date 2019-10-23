using Newtonsoft.Json;
using System;

namespace Jaya.Shared.Base
{
    [JsonObject(MemberSerialization.OptIn)]
    public abstract class ConfigModelBase : ModelBase
    {
        internal static T Empty<T>() where T : ConfigModelBase
        {
            return (T)Activator.CreateInstance<T>().Empty();
        }

        protected abstract ConfigModelBase Empty();
    }
}
