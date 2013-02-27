using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Platformer.Events;
using Platformer.World.Entities.Events;
using Platformer.World.Entities.Items;

namespace Platformer.World.Entities.Components
{
    public class GoldComponent : EntityComponent
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="GoldComponent"/> class.
        /// </summary>
        /// <param name="entity"></param>
        public GoldComponent(Entity entity) : base(entity)
        {
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the count.
        /// </summary>
        public int Count { get; set; }
        #endregion

        #region Methods
        /// <summary>
        /// Handles an entity event.
        /// </summary>
        /// <param name="entityEvent">The event.</param>
        public override void HandleEvent(Event entityEvent)
        {
            EntityCollisionEvent collisionEvent = entityEvent as EntityCollisionEvent;
            if (collisionEvent != null)
            {
                Coin coin = collisionEvent.Entity as Coin;
                if (coin == null)
                {
                    coin = collisionEvent.CollidingEntity as Coin;
                }

                if (coin != null && coin.Collectable)
                {
                    coin.Destroy();
                    this.Count++;
                }
            }
        }
        #endregion
    }
}
