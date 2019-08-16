using Jaya.Ui.Models;
using System.Collections.Generic;
using System.IO;

namespace Jaya.Ui.Services.Providers
{
    public class FileSystemService : IProviderService
    {
        public FileSystemService()
        {
            Name = "Computer";
        }

        #region properties

        public bool IsRootDrive => true;

        public ProviderModel Provider { get; set; }

        public string Name { get; }

        #endregion

        public DirectoryModel GetDirectory(ProviderModel provider, string path = null)
        {
            var model = new DirectoryModel();

            if (string.IsNullOrEmpty(path))
            {
                model.Directories = new List<DirectoryModel>();
                foreach (var driveInfo in DriveInfo.GetDrives())
                {
                    if (!driveInfo.IsReady)
                        continue;

                    var drive = new DirectoryModel(true);
                    drive.Name = driveInfo.Name;
                    drive.Path = driveInfo.RootDirectory.FullName;
                    drive.Size = driveInfo.TotalSize;
                    model.Directories.Add(drive);
                }

                return model;
            }

            var info = new DirectoryInfo(path);
            model.Name = info.Name;
            model.Path = info.FullName;
            model.Created = info.CreationTime;
            model.Modified = info.LastWriteTime;
            model.Accessed = info.LastAccessTime;

            model.Files = new List<FileModel>();
            foreach (var fileInfo in info.GetFiles())
            {
                var file = new FileModel();
                file.Name = fileInfo.Name;
                file.Path = fileInfo.FullName;
                file.Created = fileInfo.CreationTime;
                file.Modified = fileInfo.LastWriteTime;
                file.Accessed = fileInfo.LastAccessTime;
                model.Files.Add(file);
            }

            model.Directories = new List<DirectoryModel>();
            foreach (var directoryInfo in info.GetDirectories())
            {
                var directory = new DirectoryModel(true);
                directory.Name = directoryInfo.Name;
                directory.Path = directoryInfo.FullName;
                directory.Created = directoryInfo.CreationTime;
                directory.Modified = directoryInfo.LastWriteTime;
                directory.Accessed = directoryInfo.LastAccessTime;
                model.Directories.Add(directory);
            }

            return model;
        }


    }
}
