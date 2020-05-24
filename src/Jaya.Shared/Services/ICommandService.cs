namespace Jaya.Shared.Services
{
    public interface ICommandService: IService
    {
        EventAggregator EventAggregator { get;}
    }
}