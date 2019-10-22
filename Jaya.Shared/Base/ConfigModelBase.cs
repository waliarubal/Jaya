using Newtonsoft.Json;
using System;

namespace Jaya.Shared.Base
{
    [JsonObject(MemberSerialization.OptIn)]
    [Serializable]
    public abstract class ConfigModelBase: ModelBase
    {

    }
}
