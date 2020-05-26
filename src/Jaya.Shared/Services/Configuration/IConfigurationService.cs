using Jaya.Shared.Base;

namespace Jaya.Shared.Services
{
    public interface IConfigurationService: IService
    {
        string ConfigurationDirectory { get; }

        T Get<T>(string key = null) where T : ConfigModelBase;

        T GetOrDefault<T>(string key = null) where T : ConfigModelBase;

        void Set<T>(T value, string key = null);
        
    }
}