using Avalonia;

namespace Jaya.Shared.Services
{
    public sealed class CommandService
    {
        readonly Subscription<byte> _onSimpleCommand;

        public CommandService()
        {
            EventAggregator = new EventAggregator();
            _onSimpleCommand = EventAggregator.Subscribe<byte>(SimpleCommandAction);
        }

        ~CommandService()
        {
            EventAggregator.UnSubscribe(_onSimpleCommand);
        }

        public EventAggregator EventAggregator { get; }

        void SimpleCommandAction(byte type)
        {
            switch (type)
            {
                case 0:
                    Application.Current.Exit();
                    break;
            }
        }
    }
}
