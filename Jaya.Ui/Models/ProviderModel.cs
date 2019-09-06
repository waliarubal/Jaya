using Jaya.Ui.Base;
using System;
using System.Threading.Tasks;

namespace Jaya.Ui.Models
{
    public class ProviderModel: ModelBase
    {
        public ProviderModel(string name, ProviderServiceBase service)
        {
            Id = Guid.NewGuid();
            Name = name;
            Service = service;
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

        public ProviderServiceBase Service
        {
            get => Get<ProviderServiceBase>();
            private set => Set(value);
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

        public async void GetDirectory(string path = null)
        {
            Directory = await Service.GetDirectoryAsync(this, path);
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
