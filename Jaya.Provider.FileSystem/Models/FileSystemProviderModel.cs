using Jaya.Provider.FileSystem.Services;
using Jaya.Shared.Models;
using System;

namespace Jaya.Provider.FileSystem.Models
{
    public class FileSystemProviderModel: ProviderModelBase
    {
        public FileSystemProviderModel(FileSystemService service) : base(Environment.MachineName, service)
        {
            ImagePath = "avares://Jaya.Provider.FileSystem/Assets/Images/Computer-32.png";
        }
    }
}
