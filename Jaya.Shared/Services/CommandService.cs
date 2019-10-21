
namespace Jaya.Shared.Services
{
    public sealed class CommandService: IService
    {
        public CommandService()
        {
            EventAggregator = new EventAggregator();
        }

        public EventAggregator EventAggregator { get; }

        public string Name => nameof(CommandService);
    }
}
