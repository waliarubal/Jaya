using Avalonia;
using Avalonia.Data.Converters;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using Jaya.Ui.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace Jaya.Ui.Converters
{
    public class FileSystemObjectToImageConverter : IValueConverter
    {
        const string IMAGE_PATH_FORMAT = "avares://Jaya.Ui/Assets/Images/{0}{1}.png";
        const string FILE_PATH_FORMAT = "avares://Jaya.Ui/Assets/Images/FileExtensions/{0}";

        static readonly Dictionary<Uri, Bitmap> _imageCache;

        static FileSystemObjectToImageConverter()
        {
            _imageCache = new Dictionary<Uri, Bitmap>();
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var fso = value as FileSystemObjectModel;
            if (fso == null)
                return null;

            var iconSize = 48;
            if (parameter != null)
                iconSize = int.Parse(parameter as string);

            Uri uri;
            var assets = AvaloniaLocator.Current.GetService<IAssetLoader>();
            switch (fso.Type)
            {
                case FileSystemObjectType.Drive:
                    uri = new Uri(string.Format(IMAGE_PATH_FORMAT, "Hdd-", iconSize), UriKind.RelativeOrAbsolute);
                    break;

                case FileSystemObjectType.Directory:
                    uri = new Uri(string.Format(IMAGE_PATH_FORMAT, "Folder-", iconSize), UriKind.RelativeOrAbsolute);
                    break;

                case FileSystemObjectType.File:
                    return GetFileImage(fso as FileModel, iconSize, assets);

                default:
                    return null;
            }


            return new Bitmap(assets.Open(uri));
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        Bitmap AddOrGetFromCache(Uri uri, IAssetLoader assets, Uri fallbackUri = null)
        {
            if (_imageCache.ContainsKey(uri))
                return _imageCache[uri];

            Bitmap image;
            try
            {
                image = new Bitmap(assets.Open(uri));
                _imageCache.Add(uri, image);

            }
            catch (FileNotFoundException)
            {
                if (fallbackUri == null)
                    return null;

                if (_imageCache.ContainsKey(fallbackUri))
                    return _imageCache[fallbackUri];

                image = new Bitmap(assets.Open(fallbackUri));
                _imageCache.Add(fallbackUri, image);
            }

            return image;
        }

        Bitmap GetFileImage(FileModel fso, int iconSize, IAssetLoader assets)
        {
            var fallbackUri = new Uri(string.Format(IMAGE_PATH_FORMAT, "File-", iconSize), UriKind.RelativeOrAbsolute);

            if (string.IsNullOrEmpty(fso.Extension))
                return AddOrGetFromCache(fallbackUri, assets);

            var extensionImageFile = string.Format("{0}-{1}.png", fso.Extension, iconSize);
            var uri = new Uri(string.Format(FILE_PATH_FORMAT, extensionImageFile), UriKind.RelativeOrAbsolute);

            return AddOrGetFromCache(uri, assets, fallbackUri);
        }
    }
}
