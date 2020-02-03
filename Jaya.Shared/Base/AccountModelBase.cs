using Jaya.Shared.Base;
using Newtonsoft.Json;

namespace Jaya.Shared.Models
{
    [JsonObject(MemberSerialization.OptIn)]
    public abstract class AccountModelBase: ModelBase
    {
        public AccountModelBase(string id, string name)
        {
            Id = id;
            Name = name;
        }

        [JsonProperty]
        public string Id
        {
            get => Get<string>();
            protected set => Set(value);
        }

        [JsonProperty]
        public string Name
        {
            get => Get<string>();
            protected set => Set(value);
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

        public override bool Equals(object obj)
        {
            var compareWith = obj as AccountModelBase;
            if (compareWith == null)
                return false;

            return compareWith.GetHashCode() == GetHashCode();
        }
    }
}
