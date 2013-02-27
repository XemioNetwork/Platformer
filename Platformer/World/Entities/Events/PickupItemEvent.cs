using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Platformer.World.Entities.Items;
using Platformer.Events;

namespace Platformer.World.Entities.Events
{
    public class PickupItemEvent : Event
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="PickupItemEvent"/> class.
        /// </summary>
        public PickupItemEvent(Entity entity, Item item)
        {
            this.Entity = entity;
            this.Item = item;
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets the entity.
        /// </summary>
        public Entity Entity { get; private set; }
        /// <summary>
        /// Gets the item.
        /// </summary>
        public Item Item { get; private set; }
        #endregion
    }
}
