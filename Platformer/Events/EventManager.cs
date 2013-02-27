using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Platformer.Components;

namespace Platformer.Events
{
    public class EventManager : IComponent
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="EventManager"/> class.
        /// </summary>
        public EventManager()
        {
            this._subscribers = new Dictionary<Type, SubscriberCollection>();
        }
        #endregion

        #region Fields
        private Dictionary<Type, SubscriberCollection> _subscribers;
        #endregion

        #region Methods
        /// <summary>
        /// Initializes the specified type.
        /// </summary>
        /// <param name="type">The type.</param>
        private void Initialize(Type type)
        {
            if (this._subscribers[type] == null)
            {
                this._subscribers[type] = new SubscriberCollection();
            }
        }
        /// <summary>
        /// Sends the specified event through the event manager.
        /// </summary>
        /// <param name="eventInstance">The event instance.</param>
        public void Send(Event eventInstance)
        {
            Type eventType = eventInstance.GetType();
            this.Initialize(eventType);

            SubscriberCollection collection = this._subscribers[eventType];
            foreach (IEventSubscriber subscriber in collection)
            {
                subscriber.Handle(eventInstance);
            }
        }
        /// <summary>
        /// Registers the subscriber to the specified event.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="subscriber">The subscriber.</param>
        public void Subscribe(Type type, IEventSubscriber subscriber)
        {
            this.Initialize(type);
            this._subscribers[type].Add(subscriber);
        }
        /// <summary>
        /// Registers the subscriber to the specified event.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="subscriber">The subscriber.</param>
        public void Subscribe<T>(IEventSubscriber<T> subscriber) where T : Event
        {
            this.Subscribe(typeof(T), subscriber);
        }
        #endregion
    }
}
