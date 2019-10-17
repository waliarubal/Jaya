using Newtonsoft.Json;

namespace Jaya.Shared.Base
{
    [JsonObject(MemberSerialization.OptIn)]
    public abstract class ConfigModelBase: ModelBase
    {

    }
}
