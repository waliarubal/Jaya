using Jaya.Shared.Base;
using Newtonsoft.Json;
using System;

namespace Jaya.Shared.Models
{
    [JsonObject(MemberSerialization.OptIn)]
    public abstract class AccountModelBase: ModelBase
    {
        public AccountModelBase(string name)
        {
            Id = Guid.NewGuid();
            Name = name;
        }

        public Guid Id
        {
            get => Get<Guid>();
            private set => Set(value);
        }

        [JsonProperty]
        public string Name
        {
            get => Get<string>();
            private set => Set(value);
        }

        public string ImagePath
        {
            get => Get<string>();
            set => Set(value);
        }

        public bool IsEnabled
        {
            get => Get<bool>();
            set => Set(value);
        }

        public DirectoryModel Directory
        {
            get => Get<DirectoryModel>();
            private set => Set(value);
        }

        public override string ToString()
        {
            return Name;
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}
