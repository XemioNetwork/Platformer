using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Platformer.Events
{
    public class SubscriberCollection : List<IEventSubscriber>
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="SubscriberCollection"/> class.
        /// </summary>
        public SubscriberCollection()
        {
        }
        #endregion
    }
}
