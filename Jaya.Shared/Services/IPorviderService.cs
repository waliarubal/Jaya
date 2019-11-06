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

        Task<IEnumerable<ProviderAccountModelBase>> GetAccountsAsync();

        Task<DirectoryModel> GetDirectoryAsync(ProviderAccountModelBase provider, string path = null);

        void SaveConfigurations();
    }
}
