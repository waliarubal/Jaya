using Jaya.Shared.Base;
using System;

namespace Jaya.Shared.Models
{
    public enum FileSystemObjectType : byte
    {
        File,
        Directory,
        Drive
    }

    public abstract class FileSystemObjectModel : ModelBase
    {
        static readonly string[] _sizeSuffixes;

        static FileSystemObjectModel()
        {
            _sizeSuffixes = new string[] { "bytes", "KB", "MB", "GB", "TB", "PB", "EB", "ZB", "YB" };
        }

        #region properties

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
            set
            {
                if (Set(value))
                    RaisePropertyChanged(nameof(SizeString));
            }
        }

        public string SizeString
        {
            get
            {
                if (Type == FileSystemObjectType.Directory)
                    return string.Empty;

                return SizeSuffix(Size ?? 0L, 2);
            }
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

        public bool IsHidden
        {
            get => Get<bool>();
            set => Set(value);
        }

        public bool IsSystem
        {
            get => Get<bool>();
            set => Set(value);
        }

        #endregion

        public override string ToString()
        {
            return Name;
        }

        // logic taken from https://stackoverflow.com/questions/14488796/does-net-provide-an-easy-way-convert-bytes-to-kb-mb-gb-etc
        public static string SizeSuffix(long value, int decimalPlaces)
        {
            if (decimalPlaces < 0)
                throw new ArgumentOutOfRangeException(nameof(decimalPlaces));
            if (value < 0)
                return "-" + SizeSuffix(-value, decimalPlaces);
            if (value == 0)
                return string.Format("{0:n" + decimalPlaces + "} bytes", 0);

            // mag is 0 for bytes, 1 for KB, 2, for MB, etc.
            int mag = (int)Math.Log(value, 1024);

            // 1L << (mag * 10) == 2 ^ (10 * mag) 
            // [i.e. the number of bytes in the unit corresponding to mag]
            decimal adjustedSize = (decimal)value / (1L << (mag * 10));

            // make adjustment when the value is large enough that
            // it would round up to 1000 or more
            if (Math.Round(adjustedSize, decimalPlaces) >= 1000)
            {
                mag += 1;
                adjustedSize /= 1024;
            }

            return string.Format("{0:n" + decimalPlaces + "} {1}",
                adjustedSize,
                _sizeSuffixes[mag]);
        }
    }
}
