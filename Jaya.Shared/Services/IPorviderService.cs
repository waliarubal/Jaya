using Jaya.Shared.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Jaya.Shared.Services
{
    public interface IProviderService: IDisposable
    {
        bool IsRootDrive { get; }

        string Name { get; }

        string Description { get; }

        string ImagePath { get; }

        Type ConfigurationEditorType { get; }

        Task<IEnumerable<ProviderModel>> GetProvidersAsync();

        Task<DirectoryModel> GetDirectoryAsync(ProviderModel provider, string path = null);
    }
}
