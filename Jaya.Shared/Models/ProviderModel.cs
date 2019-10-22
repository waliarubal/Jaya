using Jaya.Shared.Base;
using Jaya.Shared.Contracts;
using Jaya.Shared.Services;
using System;

namespace Jaya.Shared.Models
{
    [Serializable]
    public class ProviderModel : ModelBase
    {
        public ProviderModel(string name)
        {
            Id = Guid.NewGuid();
            Name = name;
        }

        public Guid Id
        {
            get => Get<Guid>();
            private set => Set(value);
        }

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
