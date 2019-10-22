using Jaya.Shared.Base;
using Jaya.Shared.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Jaya.Shared.Contracts
{
    public interface IJayaPlugin
    {
        bool IsRootDrive { get; }

        string Name { get; }

        string Description { get; }

        string ImagePath { get; }

        Type ConfigurationEditorType { get; }

        ConfigModelBase Configuration { get; }
        DirectoryModel GetDirectory(ProviderModel provider, string path = null);
        ProviderModel GetDefaultProvider();
    }
}
