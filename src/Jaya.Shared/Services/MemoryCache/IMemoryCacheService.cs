namespace Jaya.Shared.Services
{
    public interface IMemoryCacheService : IService
    {
        bool TryGetValue<T>(object key, out T result);

        void Set<T>(object key, T value);
    }
}