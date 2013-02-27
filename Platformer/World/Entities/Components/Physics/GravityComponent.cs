using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Platformer.Math;
using Platformer.Events;
using Platformer.World.Entities.Events;
using Platformer.Math.Collision;

namespace Platformer.World.Entities.Components.Physics
{
    public class GravityComponent : EntityComponent, IForceComponent
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="GravityComponent"/> class.
        /// </summary>
        /// <param name="entity"></param>
        public GravityComponent(Entity entity) : base(entity)
        {
            this.Gravity = new Vector2(0, 0.35f);
        }
        #endregion

        #region Fields
        private Vector2 _remainingVelocity;
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the gravity.
        /// </summary>
        public Vector2 Gravity { get; set; }
        /// <summary>
        /// Gets or sets the velocity.
        /// </summary>
        public Vector2 Velocity { get; set; }
        #endregion

        #region Methods
        /// <summary>
        /// Handles an entity event.
        /// </summary>
        /// <param name="entityEvent">The event.</param>
        public override void HandleEvent(Event entityEvent)
        {
            if (entityEvent is ICollisionEvent)
            {
                ICollisionEvent collisionEvent = entityEvent as ICollisionEvent;
                if (collisionEvent.Result.Direction == Direction.Bottom &&
                    collisionEvent.Result.MinimumTranslation.LengthSquared > 0)
                {
                    this._remainingVelocity = this.Velocity;
                    this.Velocity = Vector2.Zero;
                }
            }
        }
        /// <summary>
        /// Handles a game tick.
        /// </summary>
        /// <param name="elapsed">The elapsed.</param>
        public override void Tick(float elapsed)
        {
            if (this.Velocity.LengthSquared == 0)
            {
                this.Entity.Position += this._remainingVelocity;
                this._remainingVelocity = Vector2.Zero;
            }

            this.Velocity += this.Gravity;
            this.Entity.Position += this.Velocity;
        }
        #endregion

        #region IForce Member
        /// <summary>
        /// Gets the direction.
        /// </summary>
        public Vector2 Force
        {
            get { return this.Velocity; }
        }
        #endregion
    }
}
