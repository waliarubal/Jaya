using Jaya.Ui.Models;
using System;
using System.Collections.Generic;
using System.IO;

namespace Jaya.Ui.Services.Providers
{
    public class FileSystemService : IProviderService
    {
        public FileSystemService()
        {
            Name = "File System";
            ImagePath = "avares://Jaya.Ui/Assets/Images/Computer-16.png";
        }

        #region properties

        public bool IsRootDrive => true;

        public ProviderModel Provider { get; set; }

        public string Name { get; }

        public string ImagePath { get; }

        #endregion

        public ProviderModel GetDefaultProvider()
        {
            var provider = new ProviderModel(Environment.MachineName, this);
            provider.ImagePath = "avares://Jaya.Ui/Assets/Images/Client-16.png";
            return provider;
        }

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
            model.IsHidden = info.Attributes.HasFlag(FileAttributes.Hidden);

            model.Files = new List<FileModel>();
            foreach (var fileInfo in info.GetFiles())
            {
                var file = new FileModel();
                file.Name = fileInfo.Name;
                file.Path = fileInfo.FullName;
                file.Created = fileInfo.CreationTime;
                file.Modified = fileInfo.LastWriteTime;
                file.Accessed = fileInfo.LastAccessTime;
                file.IsHidden = fileInfo.Attributes.HasFlag(FileAttributes.Hidden);
                model.Files.Add(file);
            }

            model.Directories = new List<DirectoryModel>();
            foreach (var directoryInfo in info.GetDirectories())
            {
                var directory = new DirectoryModel();
                directory.Name = directoryInfo.Name;
                directory.Path = directoryInfo.FullName;
                directory.Created = directoryInfo.CreationTime;
                directory.Modified = directoryInfo.LastWriteTime;
                directory.Accessed = directoryInfo.LastAccessTime;
                directory.IsHidden = directoryInfo.Attributes.HasFlag(FileAttributes.Hidden);
                model.Directories.Add(directory);
            }

            return model;
        }


    }
}
