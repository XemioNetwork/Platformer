using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Platformer.Events;

namespace Platformer.World.Entities.Events
{
    public class EntityDiedEvent : Event
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="EntityDiedEvent"/> class.
        /// </summary>
        public EntityDiedEvent(Entity entity)
        {
            this.Entity = entity;
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets the entity.
        /// </summary>
        public Entity Entity { get; private set; }
        #endregion
    }
}
