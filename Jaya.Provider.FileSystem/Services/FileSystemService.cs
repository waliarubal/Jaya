using Jaya.Provider.FileSystem.Models;
using Jaya.Provider.FileSystem.Views;
using Jaya.Shared.Base;
using Jaya.Shared.Models;
using Jaya.Shared.Services;
using System;
using System.Collections.Generic;
using System.Composition;
using System.IO;
using System.Threading.Tasks;

namespace Jaya.Provider.FileSystem.Services
{
    [Export(typeof(IProviderService))]
    [Shared]
    public class FileSystemService : ProviderServiceBase, IProviderService
    {
        ConfigModel _config;

        public FileSystemService()
        {
            Name = "File System";
            ImagePath = "avares://Jaya.Provider.FileSystem/Assets/Images/Computer-32.png";
            Description = "View your local drives, inspect their properties and play with directories & files stored within them.";
            IsRootDrive = true;
            ConfigurationEditorType = typeof(ConfigurationView);
        }

        public override ConfigModelBase Configuration
        {
            get
            {
                if (_config == null)
                    _config = ConfigurationService.GetOrDefault<ConfigModel>(Name);

                return _config;
            }
        }

        public override void SaveConfiguration()
        {
            if (_config == null)
                return;

            ConfigurationService.Set(_config, Name);
        }

        public override async Task<DirectoryModel> GetDirectoryAsync(ProviderModel provider, string path = null)
        {
            var model = GetFromCache(provider, path);
            if (model != null)
                return model;

            return await Task.Run(() =>
            {
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
                model.IsSystem = info.Attributes.HasFlag(FileAttributes.System);

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
                        file.IsSystem = fileInfo.Attributes.HasFlag(FileAttributes.System);
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
                        directory.IsSystem = directoryInfo.Attributes.HasFlag(FileAttributes.System);
                        model.Directories.Add(directory);
                    }
                }
                catch (UnauthorizedAccessException)
                {

                }

                AddToCache(provider, model);
                return model;
            });
        }

        public override async Task<IEnumerable<ProviderModel>> GetProvidersAsync()
        {
            var providers = new List<ProviderModel>
            {
                new ProviderModel(Environment.MachineName, this)
                {
                    ImagePath = "avares://Jaya.Provider.FileSystem/Assets/Images/Computer-32.png"
                }
            };

            return await Task.Run(() => providers);
        }
    }
}
