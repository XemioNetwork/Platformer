using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Platformer.Events
{
    public interface IEventSubscriber
    {
        /// <summary>
        /// Handles the specified event.
        /// </summary>
        /// <param name="eventInstance">The event.</param>
        void Handle(Event eventInstance);
    }
    public interface IEventSubscriber<T> : IEventSubscriber where T : Event
    {
        /// <summary>
        /// Requests the subscriber to handle a specific event.
        /// </summary>
        /// <param name="eventInstance">The event.</param>
        new void Handle(T eventInstance);
    }
}
