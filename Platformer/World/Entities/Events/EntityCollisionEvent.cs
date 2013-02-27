using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Platformer.Events;
using Platformer.Math.Collision;

namespace Platformer.World.Entities.Events
{
    public class EntityCollisionEvent : Event, ICollisionEvent
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="EntityCollisionEvent"/> class.
        /// </summary>
        public EntityCollisionEvent(Entity entity, Entity collidingEntity, CollisionResult result)
        {
            this.Entity = entity;
            this.CollidingEntity = collidingEntity;
            this.Result = result;
        }
        #endregion
        
        #region Properties
        /// <summary>
        /// Gets the entity.
        /// </summary>
        public Entity Entity { get; private set; }
        /// <summary>
        /// Gets the colliding entity.
        /// </summary>
        public Entity CollidingEntity { get; private set; }
        /// <summary>
        /// Gets the result.
        /// </summary>
        public CollisionResult Result { get; private set; }
        #endregion
    }
}
