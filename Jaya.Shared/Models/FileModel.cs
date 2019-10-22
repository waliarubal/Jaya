using System;

namespace Jaya.Shared.Models
{
    [Serializable]
    public class FileModel : FileSystemObjectModel
    {
        public FileModel()
        {
            Type = FileSystemObjectType.File;
        }

        public string Extension
        {
            get => Get<string>();
            set => Set(value);
        }
    }
}
