using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Platformer.Events;
using Platformer.World.TileEngine;
using Platformer.Math.Collision;

namespace Platformer.World.Entities.Events
{
    public class TileCollisionEvent : Event, ICollisionEvent
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="TileCollisionEvent"/> class.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <param name="reference">The reference.</param>
        /// <param name="result">The result.</param>
        public TileCollisionEvent(Entity entity, TileReference reference, CollisionResult result)
        {
            this.Entity = entity;
            this.Reference = reference;
            this.Result = result;
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets the entity.
        /// </summary>
        public Entity Entity { get; private set; }
        /// <summary>
        /// Gets the reference.
        /// </summary>
        public TileReference Reference { get; private set; }
        /// <summary>
        /// Gets the result.
        /// </summary>
        public CollisionResult Result { get; private set; }
        #endregion
    }
}
