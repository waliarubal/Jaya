
using System.Composition;

namespace Jaya.Shared.Services
{
    [Export(typeof(CommandService))]
    public sealed class CommandService: IService
    {
        public CommandService()
        {
            EventAggregator = new EventAggregator();
        }

        public EventAggregator EventAggregator { get; }
    }
}
