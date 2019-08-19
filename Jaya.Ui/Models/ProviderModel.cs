using Jaya.Ui.Services.Providers;
using System;

namespace Jaya.Ui.Models
{
    public class ProviderModel: ModelBase
    {
        public ProviderModel(string name, IProviderService service)
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

        public IProviderService Service
        {
            get => Get<IProviderService>();
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

        public void GetDirectory(string path = null)
        {
            Directory = Service.GetDirectory(this, path);
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
