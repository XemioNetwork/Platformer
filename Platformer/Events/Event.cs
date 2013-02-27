using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Platformer.Events
{
    public abstract class Event
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="Event"/> class.
        /// </summary>
        public Event()
        {
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets an ID indicating how many handlers the event already passed.
        /// </summary>
        public int ChainID { get; private set; }
        /// <summary>
        /// Gets a value indicating whether this <see cref="Event"/> is suppressed.
        /// </summary>
        internal bool Suppressed { get; private set; }
        #endregion

        #region Methods
        /// <summary>
        /// Supresses this event.
        /// </summary>
        public void Suppress()
        {
            this.Suppressed = true;
        }
        #endregion
    }
}
