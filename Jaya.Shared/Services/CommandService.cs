
using System.Composition;

namespace Jaya.Shared.Services
{
    [Export(nameof(CommandService), typeof(IService))]
    [Shared]
    public sealed class CommandService: IService
    {
        EventAggregator _eventAggregator;

        public EventAggregator EventAggregator
        {
            get
            {
                if (_eventAggregator == null) { }
                    _eventAggregator = new EventAggregator();

                return _eventAggregator;
            }
        }
    }
}
