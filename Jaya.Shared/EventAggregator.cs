using System;
using System.Collections;
using System.Collections.Generic;

namespace Jaya.Shared
{
    /// <summary>
    /// Taken from https://www.c-sharpcorner.com/UploadFile/pranayamr/publisher-or-subscriber-pattern-with-event-or-delegate-and-e/
    /// </summary>
    public sealed class EventAggregator
    {
        readonly Dictionary<Type, IList> _subscribers;

        public EventAggregator()
        {
            _subscribers = new Dictionary<Type, IList>();
        }

        public void Publish<TMessageType>(TMessageType message)
        {
            Type type = typeof(TMessageType);
            if (_subscribers.ContainsKey(type))
            {
                foreach (Subscription<TMessageType> action in _subscribers[type])
                {
                    action.Action(message);
                }
            }
        }

        public Subscription<TMessageType> Subscribe<TMessageType>(Action<TMessageType> action)
        {
            Type type = typeof(TMessageType);
            var actionDetail = new Subscription<TMessageType>(action, this);

            if (!_subscribers.TryGetValue(type, out IList actionList))
            {
                actionList = new List<Subscription<TMessageType>>();
                actionList.Add(actionDetail);
                _subscribers.Add(type, actionList);
            }
            else
            {
                actionList.Add(actionDetail);
            }

            return actionDetail;
        }

        public void UnSubscribe<TMessageType>(Subscription<TMessageType> subscription)
        {
            Type type = typeof(TMessageType);
            if (_subscribers.ContainsKey(type))
            {
                _subscribers[type].Remove(subscription);
            }
        }
    }
}
