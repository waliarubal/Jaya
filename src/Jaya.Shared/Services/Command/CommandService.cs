namespace Jaya.Shared.Services
{
    public sealed class CommandService: ICommandService
    {
        public CommandService()
        {
            EventAggregator = new EventAggregator();
        }

        public EventAggregator EventAggregator { get; }
    }
}
