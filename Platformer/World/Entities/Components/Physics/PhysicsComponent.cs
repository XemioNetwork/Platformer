using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Platformer.Math;

namespace Platformer.World.Entities.Components.Physics
{
    public class PhysicsComponent : EntityComponent, IForceComponent
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="PhysicsComponent"/> class.
        /// </summary>
        /// <param name="entity"></param>
        public PhysicsComponent(Entity entity) : this(entity, 0.06f)
        {
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="PhysicsComponent"/> class.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <param name="friction">The friction.</param>
        public PhysicsComponent(Entity entity, float friction) : base(entity)
        {
            this.Friction = friction;
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the acceleration.
        /// </summary>
        public Vector2 Acceleration { get; set; }
        /// <summary>
        /// Gets or sets the velocity.
        /// </summary>
        public Vector2 Velocity { get; set; }
        /// <summary>
        /// Gets or sets the friction.
        /// </summary>
        public float Friction { get; set; }
        #endregion

        #region Methods
        /// <summary>
        /// Handles a game tick.
        /// </summary>
        /// <param name="elapsed">The elapsed.</param>
        public override void Tick(float elapsed)
        {
            this.Velocity += this.Acceleration;

            this.Entity.Position += this.Velocity;

            this.Velocity *= 1 - this.Friction;
            this.Acceleration *= 1 - this.Friction;
        }
        #endregion

        #region IForce Member
        /// <summary>
        /// Gets the force.
        /// </summary>
        public Vector2 Force
        {
            get { return this.Velocity; }
        }
        #endregion
    }
}
