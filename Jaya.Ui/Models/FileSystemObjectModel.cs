using System;

namespace Jaya.Ui.Models
{
    public abstract class FileSystemObjectModel : ModelBase
    {
        public string Id
        {
            get => Get<string>();
            set => Set(value);
        }

        public string Name
        {
            get => Get<string>();
            set => Set(value);
        }

        public string Path
        {
            get => Get<string>();
            set => Set(value);
        }

        public long? Size
        {
            get => Get<long?>();
            set => Set(value);
        }

        public FileSystemObjectType Type
        {
            get => Get<FileSystemObjectType>();
            protected set => Set(value);
        }

        public DateTime? Created
        {
            get => Get<DateTime?>();
            set => Set(value);
        }

        public DateTime? Accessed
        {
            get => Get<DateTime?>();
            set => Set(value);
        }

        public DateTime? Modified
        {
            get => Get<DateTime?>();
            set => Set(value);
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
