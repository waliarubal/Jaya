using Jaya.Shared.Base;
using Newtonsoft.Json;
using System;
using System.Composition;
using System.IO;

namespace Jaya.Shared.Services
{
    [Export(nameof(ConfigurationService), typeof(IService))]
    public sealed class ConfigurationService: IService
    {
        readonly string _configurationFilePathFormat;

        public ConfigurationService()
        {
            _configurationFilePathFormat = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Jaya", "config_{0}.json");
        }

        ~ConfigurationService()
        {

        }

        public T Get<T>(string key = null) where T : ConfigModelBase
        {
            var type = typeof(T);

            if (string.IsNullOrEmpty(key))
                key = type.Name;

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
                key = type.Name;

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

    }
}
