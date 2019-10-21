
using Jaya.Shared.Contracts;
using System.Composition;

namespace Jaya.Shared.Services
{
    //[Export(typeof(IService))]
    public sealed class CommandService : ICommandService
    {
        public CommandService()
        {
            EventAggregator = new EventAggregator();
        }

        public EventAggregator EventAggregator { get; }
    }
}
