using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Platformer.World.Entities.Components;
using Platformer.World.Entities.Animation;
using Platformer.Math;
using Platformer.Events;
using Platformer.World.Entities.Events;
using Platformer.Math.Collision;
using Platformer.World.Entities.Items;

namespace Platformer.World.Entities.Mobs
{
    public class Slime : Mob
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="Slime"/> class.
        /// </summary>
        public Slime()
        {
            this.Animations.Add(new EntityAnimation("Left", 50, Art.Slime));
            this.Animations.Add(new EntityAnimation("Right", 50, Art.Slime));
            this.Animations.Add(new EntityAnimation("Idle", 50, Art.Slime[0]));

            this.Animations.Add(new EntityAnimation("JumpLeft", 50, Art.Slime[0]));
            this.Animations.Add(new EntityAnimation("JumpRight", 50, Art.Slime[0]));

            int coins = Shared.Random.Next(1, 4);
            for (int i = 0; i < coins; i++)
            {
                this.DroppedItems.Add(new Coin());
            }

            CollidableComponent collidableComponent = this.GetComponent<CollidableComponent>();
            collidableComponent.BoundingBox = new Math.Rectangle(0, 0, 43, 28);

            LifeComponent lifeComponent = this.GetComponent<LifeComponent>();
            lifeComponent.Set(4);
        }
        #endregion

        #region Fields
        private float _elapsed;
        #endregion

        #region Properties
        /// <summary>
        /// Gets a value indicating whether this <see cref="Mob"/> is friendly.
        /// </summary>
        public override bool Friendly
        {
            get { return false; }
        }
        /// <summary>
        /// Gets the jump acceleration.
        /// </summary>
        public override float JumpAcceleration
        {
            get { return 1f; }
        }
        /// <summary>
        /// Gets the jump strength.
        /// </summary>
        public override float JumpVelocity
        {
            get { return 3; }
        }
        /// <summary>
        /// Gets the jump delay.
        /// </summary>
        public override float JumpDelay
        {
            get { return 2000; }
        }
        #endregion

        #region Methods
        /// <summary>
        /// Handls an entity event.
        /// </summary>
        /// <param name="entityEvent">The event.</param>
        public override void HandleEvent(Event entityEvent)
        {
            EntityCollisionEvent collisionEvent = entityEvent as EntityCollisionEvent;
            if (collisionEvent != null)
            {
                if (collisionEvent.CollidingEntity == this &&
                    collisionEvent.Result.Direction == Direction.Bottom)
                {
                    this.Destroy();
                }
            }
            base.HandleEvent(entityEvent);
        }
        /// <summary>
        /// Handles a game tick.
        /// </summary>
        /// <param name="elapsed">The elapsed.</param>
        public override void Tick(float elapsed)
        {
            this._elapsed += elapsed;

            if (this._elapsed >= 1500)
            {
                if (this.Grounded)
                {
                    this.Left();
                    this.Jump();
                }
                else
                {
                    this.Accelerate(new Vector2(-0.1f, 0), new Vector2(-0.5f, 0));
                    this.Decelerate();

                    this._elapsed = 0;
                }
            }

            base.Tick(elapsed);
        }
        #endregion
    }
}
