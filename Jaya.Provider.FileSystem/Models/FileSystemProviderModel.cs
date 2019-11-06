using Jaya.Shared.Models;
using System;

namespace Jaya.Provider.FileSystem.Models
{
    public class FileSystemProviderModel: AccountModelBase
    {
        public FileSystemProviderModel() : base(Environment.MachineName, Environment.MachineName)
        {
            ImagePath = "avares://Jaya.Provider.FileSystem/Assets/Images/Computer-32.png";
        }
    }
}
