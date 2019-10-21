
using System.Composition;

namespace Jaya.Shared.Services
{
    [Export(typeof(IService))]
    public sealed class CommandService: IService
    {
        [ImportingConstructor]
        public CommandService()
        {
            EventAggregator = new EventAggregator();
        }

        public EventAggregator EventAggregator { get; }

        public string Name => nameof(CommandService);
    }
}
