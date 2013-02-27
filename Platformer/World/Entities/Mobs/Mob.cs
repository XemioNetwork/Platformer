using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Platformer.World.Entities.Animation;
using Platformer.World.Entities.Components;
using Platformer.World.Entities.Components.Physics;
using Platformer.World.Entities.Events;
using Platformer.World.Entities.Particles;
using Platformer.Math;
using Platformer.Math.Collision;
using Platformer.Events;

namespace Platformer.World.Entities.Mobs
{
    public class Mob : AnimatedEntity
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="Mob"/> class.
        /// </summary>
        public Mob()
        {
            this.Visible = true;
            this.DroppedItems = new List<Entity>();

            this.Components.Add(new PhysicsComponent(this));
            this.Components.Add(new GravityComponent(this));
            this.Components.Add(new CollidableComponent(this));

            this.Components.Add(new LifeComponent(this, 0));
        }
        #endregion
        
        #region Fields
        private int _currentJumps;
        private float _flickeringTime;

        private float _elapsed;
        private bool _walked;
        #endregion

        #region Properties
        /// <summary>
        /// Gets the dropped items.
        /// </summary>
        public List<Entity> DroppedItems { get; private set; }
        /// <summary>
        /// Gets a value indicating whether this <see cref="Mob"/> is friendly.
        /// </summary>
        public virtual bool Friendly
        {
            get { return true; }
        }
        /// <summary>
        /// Gets a value indicating whether this <see cref="Mob"/> is flickering.
        /// </summary>
        public bool Invincible { get; private set; }
        /// <summary>
        /// Gets a value indicating whether this <see cref="Mob"/> is grounded.
        /// </summary>
        public bool Grounded { get; private set; }
        /// <summary>
        /// Gets a value indicating whether this entity can jump.
        /// </summary>
        public bool CanJump
        {
            get { return this._currentJumps < this.JumpCount && this._elapsed >= this.JumpDelay; }
        }
        /// <summary>
        /// Gets the jump count.
        /// </summary>
        public virtual int JumpCount
        {
            get { return 1; }
        }
        /// <summary>
        /// Gets the jump strength.
        /// </summary>
        public virtual float JumpVelocity
        {
            get { return 15; }
        }
        /// <summary>
        /// Gets the jump acceleration.
        /// </summary>
        public virtual float JumpAcceleration
        {
            get { return 20; }
        }
        /// <summary>
        /// Gets the jump delay.
        /// </summary>
        public virtual float JumpDelay
        {
            get { return 400; }
        }
        /// <summary>
        /// Gets the strength.
        /// </summary>
        public virtual int Strength
        {
            get { return 1; }
        }
        /// <summary>
        /// Gets a value indicating whether this <see cref="Mob"/> is visible.
        /// </summary>
        protected bool Visible { get; private set; }
        #endregion

        #region Methods
        /// <summary>
        /// Destroys this entity and removes it from the current level.
        /// </summary>
        public override void Destroy()
        {
            this.CreateEmitter(20);
            foreach (Entity droppedItem in this.DroppedItems)
            {
                PhysicsComponent physics = droppedItem.GetComponent<PhysicsComponent>();
                if (physics != null)
                {
                    physics.Velocity = new Vector2(
                        Shared.Random.NextFloat() * 2 - 1,
                        -2f);
                    
                    physics.Acceleration = physics.Velocity;
                }

                droppedItem.Position = this.Position;
                this.Spawn(droppedItem);
            }

            base.Destroy();
        }
        /// <summary>
        /// Handles a game tick.
        /// </summary>
        /// <param name="elapsed">The elapsed.</param>
        public override void Tick(float elapsed)
        {
            base.Tick(elapsed);

            if (!this._walked && this.Grounded)
            {
                this.Idle();
            }

            if (this.Invincible)
            {
                this.Visible = !this.Visible;
                this._flickeringTime -= elapsed;

                if (this._flickeringTime <= 0)
                {
                    this.Invincible = false;
                    this.Visible = true;
                }
            }

            this._elapsed += elapsed;
            this._walked = false;            
        }
        /// <summary>
        /// Handles a game render request.
        /// </summary>
        /// <param name="elapsed">The elapsed.</param>
        public override void Render(float elapsed)
        {
            if (this.Visible)
            {
                base.Render(elapsed);
            }
        }
        /// <summary>
        /// Handls an entity event.
        /// </summary>
        /// <param name="entityEvent">The event.</param>
        public override void HandleEvent(Event entityEvent)
        {
            if (entityEvent is ICollisionEvent)
            {
                ICollisionEvent collisionEvent = entityEvent as ICollisionEvent;
                EntityCollisionEvent entityCollisionEvent = collisionEvent as EntityCollisionEvent;

                if (entityCollisionEvent != null)
                {
                    Mob mob = entityCollisionEvent.CollidingEntity as Mob;
                    if (mob != null && mob.Friendly && !this.Friendly)
                    {
                        LifeComponent lifeComponent = mob.GetComponent<LifeComponent>();
                        int damage = this.Strength;

                        if (mob.Invincible) damage = 0;
                        else mob.Flicker(800);

                        if (lifeComponent != null && !this.Invincible)
                        {
                            lifeComponent.Damage(
                                damage,
                                new Vector2(
                                    PhysicsHelper.MeasureForce(this).X,
                                    PhysicsHelper.MeasureForce(this).Y * 0.125f) * 5);
                        }
                    }
                }

                if (collisionEvent.Result.Direction == Direction.Bottom && (entityCollisionEvent == null ||
                    entityCollisionEvent.Entity == this))
                {
                    this.Ground();
                }
            }
            base.HandleEvent(entityEvent);
        }
        /// <summary>
        /// Flickers this mob.
        /// </summary>
        public void Flicker(float time)
        {
            this.Invincible = true;
            this._flickeringTime = time;
        }
        /// <summary>
        /// Called when the entity hits the ground.
        /// </summary>
        private void Ground()
        {
            this._currentJumps = 0;
            this._elapsed = this.JumpDelay;

            this.Grounded = true;
        }
        /// <summary>
        /// Accelerates the specified entity.
        /// </summary>
        /// <param name="acceleration">The acceleration.</param>
        /// <param name="velocity">The velocity.</param>
        public void Accelerate(Vector2 acceleration, Vector2 velocity)
        {
            PhysicsComponent physics = this.GetComponent<PhysicsComponent>();
            {
                physics.Acceleration += acceleration;
                physics.Velocity += velocity;
            }
        }
        /// <summary>
        /// Decelerates this entity.
        /// </summary>
        public void Decelerate()
        {
            PhysicsComponent physics = this.GetComponent<PhysicsComponent>();
            {
                physics.Acceleration *= new Vector2(0.9f, 1.0f);
                physics.Velocity *= new Vector2(0.6f, 1.0f);
            }
        }
        /// <summary>
        /// Plays the "Left" animation and moves the character to the left.
        /// </summary>
        public void Left()
        {
            this.Accelerate(new Vector2(-0.2f, 0), new Vector2(-3.0f, 0));

            if (this.Grounded)
            {
                this._walked = true;
                this.PlayAnimation("Left");
            }
        }
        /// <summary>
        /// Plays the "Right" animation and moves the character to the right.
        /// </summary>
        public void Right()
        {
            this.Accelerate(new Vector2(0.2f, 0), new Vector2(3.0f, 0));

            if (this.Grounded)
            {
                this._walked = true;
                this.PlayAnimation("Right");
            }
        }
        /// <summary>
        /// Creates a particle emitter.
        /// </summary>
        protected void CreateEmitter()
        {
            this.CreateEmitter(0);
        }
        /// <summary>
        /// Creates the emitter.
        /// </summary>
        /// <param name="expansionStrength">The expansion strength.</param>
        protected void CreateEmitter(int expansionStrength)
        {
            DustParticleEmitter emitter = new DustParticleEmitter();
            emitter.MaximumEntities = 10;
            emitter.SpawnDelay = 0;
            emitter.FadeOutTime = 200;
            emitter.FadeInTime = 20;
            emitter.VelocityMultiplier = 0.8f;
            emitter.MinimumRadius = 20;
            emitter.MaximumRadius = 20;
            emitter.LifeTime = 200;
            emitter.ExpansionStrength = expansionStrength;

            emitter.Position = this.Position + new Vector2(
                this.Texture.Width / 2, this.Texture.Height / 2);

            this.Spawn(emitter);
        }
        /// <summary>
        /// Prepares the jump.
        /// </summary>
        private void PrepareJump()
        {
            this._elapsed = 0;

            this._walked = false;
            this.Grounded = false;
        }
        /// <summary>
        /// Resets the physics.
        /// </summary>
        private void ResetPhysics()
        {
            GravityComponent gravity = this.GetComponent<GravityComponent>();
            PhysicsComponent physics = this.GetComponent<PhysicsComponent>();
            gravity.Velocity = Vector2.Zero;

            physics.Velocity -= new Vector2(0, physics.Velocity.Y);
            physics.Acceleration -= new Vector2(0, physics.Acceleration.Y);
        }
        /// <summary>
        /// Plays the "Jump" animation and forces the character to perform a jump.
        /// </summary>
        public void Jump()
        {
            if (this.CanJump)
            {
                this._currentJumps++;

                this.PrepareJump();
                this.CreateEmitter();

                this.ResetPhysics();
                this.Accelerate(
                    new Vector2(0, -this.JumpAcceleration),
                    new Vector2(0, -this.JumpVelocity));

                PhysicsComponent physics = this.GetComponent<PhysicsComponent>();
                if (physics.Velocity.X < 0)
                    this.PlayAnimation("JumpLeft");
                else
                    this.PlayAnimation("JumpRight");
            }
        }
        #endregion
    }
}
