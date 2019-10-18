using Jaya.Provider.FileSystem.Models;
using Jaya.Provider.FileSystem.Views;
using Jaya.Shared.Base;
using Jaya.Shared.Models;
using Jaya.Shared.Services;
using System;
using System.Collections.Generic;
using System.IO;

namespace Jaya.Provider.FileSystem.Services
{
    public class FileSystemService : ProviderServiceBase
    {
        public FileSystemService()
        {
            Name = "File System";
            ImagePath = "avares://Jaya.Provider.FileSystem/Assets/Images/Computer-32.png";
            Description = "View your local drives, inspect their properties and play with directories & files stored within them.";
            IsRootDrive = true;
            ConfigurationEditorType = typeof(ConfigurationView);

            Configuration = ConfigurationService.Get<FileSystemConfigModel>();
            if (Configuration == null)
                Configuration = new FileSystemConfigModel { IsProtectedFileVisible = false };
        }

        protected override ProviderModel GetDefaultProvider()
        {
            var provider = new ProviderModel(Environment.MachineName, this);
            provider.ImagePath = "avares://Jaya.Provider.FileSystem/Assets/Images/Computer-32.png";
            return provider;
        }

        protected override DirectoryModel GetDirectory(ProviderModel provider, string path = null)
        {
            var model = GetFromCache(provider, path);
            if (model != null)
                return model;
            else
                model = new DirectoryModel();

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

                AddToCache(provider, model);
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
            try
            {
                foreach (var fileInfo in info.GetFiles())
                {
                    var file = new FileModel();
                    if (string.IsNullOrEmpty(fileInfo.Extension))
                        file.Name = fileInfo.Name;
                    else
                    {
                        file.Name = fileInfo.Name.Replace(fileInfo.Extension, string.Empty);
                        file.Extension = fileInfo.Extension.Substring(1).ToLowerInvariant();
                    }
                    file.Path = fileInfo.FullName;
                    file.Size = fileInfo.Length;
                    file.Created = fileInfo.CreationTime;
                    file.Modified = fileInfo.LastWriteTime;
                    file.Accessed = fileInfo.LastAccessTime;
                    file.IsHidden = fileInfo.Attributes.HasFlag(FileAttributes.Hidden);
                    model.Files.Add(file);
                }
            }
            catch (UnauthorizedAccessException)
            {

            }


            model.Directories = new List<DirectoryModel>();
            try
            {
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
            }
            catch (UnauthorizedAccessException)
            {

            }

            AddToCache(provider, model);
            return model;
        }
    }
}
