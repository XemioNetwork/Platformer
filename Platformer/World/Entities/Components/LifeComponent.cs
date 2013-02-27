using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Platformer.Math;
using Platformer.World.Entities.Events;
using Platformer.World.Entities.Components.Physics;

namespace Platformer.World.Entities.Components
{
    public class LifeComponent : EntityComponent
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="LifeComponent"/> class.
        /// </summary>
        /// <param name="entity"></param>
        public LifeComponent(Entity entity, int lives) : base(entity)
        {
            this.Set(lives);
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the health.
        /// </summary>
        public int Health { get; set; }
        /// <summary>
        /// Gets or sets the lives.
        /// </summary>
        public int Lives { get; set; }
        #endregion

        #region Methods
        /// <summary>
        /// Sets the specified lives.
        /// </summary>
        /// <param name="lives">The lives.</param>
        public void Set(int lives)
        {
            this.Health = lives;
            this.Lives = lives;
        }
        /// <summary>
        /// Damages the specified entity.
        /// </summary>
        /// <param name="damage">The damage.</param>
        public void Damage(int damage, Vector2 attackVector)
        {
            PhysicsComponent physics = this.Entity.GetComponent<PhysicsComponent>();
            if (physics != null)
            {
                physics.Acceleration = attackVector;
            }

            this.Health -= damage;
            if (this.Health <= 0)
            {
                this.Health = 0;

                this.Entity.Send(new EntityDiedEvent(this.Entity));
                this.Entity.Destroy();
            }
        }
        #endregion
    }
}
