//
// Copyright (c) Rubal Walia. All rights reserved.
// Licensed under the 3-Clause BSD license. See LICENSE file in the project root for full license information.
//
namespace Jaya.Shared.Models
{
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
