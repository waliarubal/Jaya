using System;

namespace Jaya.Shared
{
    /// <summary>
    /// Taken from https://www.c-sharpcorner.com/UploadFile/pranayamr/publisher-or-subscriber-pattern-with-event-or-delegate-and-e/
    /// </summary>
    /// <typeparam name="TMessage"></typeparam>
    public sealed class Subscription<TMessage> : IDisposable
    {
        readonly EventAggregator _aggregator;
        bool _isDisposed;

        public Subscription(Action<TMessage> action, EventAggregator aggregator)
        {
            Action = action;
            _aggregator = aggregator;
        }

        ~Subscription()
        {
            if (!_isDisposed)
                Dispose();
        }

        public void Dispose()
        {
            _aggregator.UnSubscribe(this);
            _isDisposed = true;
        }

        public Action<TMessage> Action { get; private set; }
    }
}
