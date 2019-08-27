using Avalonia;
using Avalonia.Data.Converters;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using Jaya.Ui.Models;
using System;
using System.Globalization;

namespace Jaya.Ui.Converters
{
    public class FileSystemObjectToImageConverter : IValueConverter
    {
        const string IMAGE_PATH_FORMAT = "avares://Jaya.Ui/Assets/Images/{0}{1}.png";

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var fso = value as FileSystemObjectModel;
            if (fso == null)
                return null;

            var iconSize = 48;
            if (parameter != null)
                iconSize = int.Parse(parameter as string);

            Uri uri;
            switch (fso.Type)
            {
                case FileSystemObjectType.Drive:
                    uri = new Uri(string.Format(IMAGE_PATH_FORMAT, "Hdd-", iconSize), UriKind.RelativeOrAbsolute);
                    break;

                case FileSystemObjectType.Directory:
                    uri = new Uri(string.Format(IMAGE_PATH_FORMAT, "Folder-", iconSize), UriKind.RelativeOrAbsolute);
                    break;

                case FileSystemObjectType.File:
                    uri = new Uri(string.Format(IMAGE_PATH_FORMAT, "File-", iconSize), UriKind.RelativeOrAbsolute);
                    break;

                default:
                    return null;
            }

            var assets = AvaloniaLocator.Current.GetService<IAssetLoader>();
            return new Bitmap(assets.Open(uri));
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
