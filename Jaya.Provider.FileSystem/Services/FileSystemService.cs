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

        public FileSystemService()
        {
            Name = "File System";
            ImagePath = "avares://Jaya.Provider.FileSystem/Assets/Images/Computer-32.png";
            Description = "View your local drives, inspect their properties and play with directories & files stored within them.";
            IsRootDrive = true;
            ConfigurationEditorType = typeof(ConfigurationView);
        }

        public override async Task<DirectoryModel> GetDirectoryAsync(AccountModelBase account, DirectoryModel directory = null)
        {
            var model = GetFromCache(account, directory);
            if (model != null)
                return model;

            return await Task.Run(() =>
            {
                model = new DirectoryModel();

                if (string.IsNullOrEmpty(directory.Path))
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

                    AddToCache(account, model);
                    return model;
                }

                DirectoryInfo info = new DirectoryInfo(directory.Path);
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
                        var dir = new DirectoryModel();
                        dir.Name = directoryInfo.Name;
                        dir.Path = directoryInfo.FullName;
                        dir.Created = directoryInfo.CreationTime;
                        dir.Modified = directoryInfo.LastWriteTime;
                        dir.Accessed = directoryInfo.LastAccessTime;
                        dir.IsHidden = directoryInfo.Attributes.HasFlag(FileAttributes.Hidden);
                        dir.IsSystem = directoryInfo.Attributes.HasFlag(FileAttributes.System);
                        model.Directories.Add(dir);
                    }
                }
                catch (UnauthorizedAccessException)
                {

                }

                AddToCache(account, model);
                return model;
            });
        }

        protected override Task<AccountModelBase> AddAccountAsync(AccountModelBase account = null)
        {
            throw new NotImplementedException();
        }

        protected override Task<bool> RemoveAccountAsync(AccountModelBase account)
        {
            throw new NotImplementedException();
        }

        public override async Task<IEnumerable<AccountModelBase>> GetAccountsAsync()
        {
            var providers = new List<AccountModelBase>
            {
                new AccountModel()
            };

            return await Task.Run(() => providers);
        }

        public override Task FormatAsync(AccountModelBase account, DirectoryModel directory = null)
        {
            throw new NotImplementedException();
        }
    }
}
