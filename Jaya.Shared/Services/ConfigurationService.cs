using Jaya.Shared.Base;
using Newtonsoft.Json;
using System;
using System.Composition;
using System.IO;

namespace Jaya.Shared.Services
{
    [Export(nameof(ConfigurationService), typeof(IService))]
    [Shared]
    public sealed class ConfigurationService: IService
    {
        readonly string _configurationFilePathFormat;

        public ConfigurationService()
        {
            ConfigurationDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Jaya");
            _configurationFilePathFormat = Path.Combine(ConfigurationDirectory, "config_{0}.json");
        }

        ~ConfigurationService()
        {

        }

        public string ConfigurationDirectory { get; }

        public T Get<T>(string key = null) where T : ConfigModelBase
        {
            var type = typeof(T);

            if (string.IsNullOrEmpty(key))
                key = GetUsableKey(type);

            var fileInfo = new FileInfo(string.Format(_configurationFilePathFormat, key));
            if (fileInfo.Exists)
            {
                using (var reader = File.OpenText(fileInfo.FullName))
                {
                    var serializer = new JsonSerializer { Formatting = Formatting.None };
                    return serializer.Deserialize(reader, type) as T;
                }
            }
            else
                return default;
        }

        public T GetOrDefault<T>(string key = null) where T : ConfigModelBase
        {
            var config = Get<T>(key);
            if (config == default(T))
                config = ConfigModelBase.Empty<T>();

            return config;
        }

        public void Set<T>(T value, string key = null)
        {
            var type = typeof(T);

            if (string.IsNullOrEmpty(key))
                key = GetUsableKey(type);

            // create configuration directory if missing
            var fileInfo = new FileInfo(string.Format(_configurationFilePathFormat, key));
            if (!fileInfo.Directory.Exists)
                Directory.CreateDirectory(fileInfo.DirectoryName);

            using (var writer = File.CreateText(fileInfo.FullName))
            {
                var serializer = new JsonSerializer { Formatting = Formatting.None };
                serializer.Serialize(writer, value, type);
            }
        }

        string GetUsableKey(Type type)
        {
            var name = type.Name;

            var invalidChars = new char[] { Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar };
            foreach (var invalidChar in invalidChars)
                name = name.Replace(invalidChar, '_');

            return name;
        }

    }
}
