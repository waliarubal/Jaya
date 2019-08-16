using System.Collections.Generic;

namespace Jaya.Ui.Models
{
    public class DirectoryModel : FileSystemObjectModel
    {
        public DirectoryModel(bool isDrive = false)
        {
            Type = isDrive ? FileSystemObjectType.Drive : FileSystemObjectType.Directory;
        }

        public IList<DirectoryModel> Directories
        {
            get => Get<IList<DirectoryModel>>();
            set => Set(value);
        }

        public IList<FileModel> Files
        {
            get => Get<IList<FileModel>>();
            set => Set(value);
        }
    }
}
