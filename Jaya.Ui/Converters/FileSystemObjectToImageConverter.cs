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
            var assets = AvaloniaLocator.Current.GetService<IAssetLoader>();

            Uri uri;
            var iconSize = 16;

            var param = parameter as string;
            if (param != null)
            {
                if (!int.TryParse(param, out iconSize))
                {
                    uri = new Uri(string.Format("avares://Jaya.Ui/Assets/Images/{0}", param), UriKind.RelativeOrAbsolute);
                    return new Bitmap(assets.Open(uri));
                }
            }

            var fso = value as FileSystemObjectModel;
            if (fso == null)
            {
                uri = new Uri(string.Format("avares://Jaya.Ui/Assets/Images/{0}", param), UriKind.RelativeOrAbsolute);
                return new Bitmap(assets.Open(uri));
            }

            if (parameter != null)
                iconSize = int.Parse(parameter as string);



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

            return new Bitmap(assets.Open(uri));
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
