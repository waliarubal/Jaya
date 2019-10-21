using Prise.Infrastructure;

namespace Jaya.Shared.Services
{
    [Plugin(PluginType = typeof(CommandService))]
    public sealed class CommandService
    {
        public CommandService()
        {
            EventAggregator = new EventAggregator();
        }

        public EventAggregator EventAggregator { get; }
    }
}
