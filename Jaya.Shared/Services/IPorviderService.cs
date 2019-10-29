using Jaya.Shared.Base;
using Jaya.Shared.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Jaya.Shared.Services
{
    public interface IProviderService
    {
        bool IsRootDrive { get; }

        string Name { get; }

        string Description { get; }

        string ImagePath { get; }

        Type ConfigurationEditorType { get; }

        T GetConfiguration<T>() where T : ConfigModelBase;

        Task<IEnumerable<ProviderModel>> GetProvidersAsync();

        Task<DirectoryModel> GetDirectoryAsync(ProviderModel provider, string path = null);
    }
}
