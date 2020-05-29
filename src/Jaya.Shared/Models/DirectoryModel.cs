//
// Copyright (c) Rubal Walia. All rights reserved.
// Licensed under the 3-Clause BSD license. See LICENSE file in the project root for full license information.
//
using System.Collections.Generic;

namespace Jaya.Shared.Models
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
            set
            {
                Set(value);
                RaisePropertyChanged(nameof(Children));
            }
        }

        public IList<FileModel> Files
        {
            get => Get<IList<FileModel>>();
            set
            {
                Set(value);
                RaisePropertyChanged(nameof(Children));
            }
        }

        public IEnumerable<FileSystemObjectModel> Children
        {
            get
            {
                var children = new List<FileSystemObjectModel>();
                if (Directories != null)
                    children.AddRange(Directories);
                if(Files != null)
                    children.AddRange(Files);

                return children;
            }
        }
    }
}
