using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Platformer.Common.Randomization;
using Platformer.Math;
using Platformer.World.Entities.Components;
using Platformer.World.Entities.Components.Physics;

namespace Platformer.World.Entities.Particles
{
    public class DustParticleEmitter : EntitySpawner
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="DustParticleEmitter"/> class.
        /// </summary>
        public DustParticleEmitter()
        {
            this.VelocityMultiplier = 0.5f;
            this.ExpansionStrength = 4;

            this.Sustainability = 100;
            this.FadeInTime = 20;
            this.FadeOutTime = 200;

            this.SpawnDelay = 50;
            this.Enabled = true;

            this.MinimumRadius = 20;
            this.MaximumRadius = 20;

            this.LifeTime = -1;
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the expansion strength.
        /// </summary>
        public int ExpansionStrength { get; set; }
        /// <summary>
        /// Gets or sets the sustainability.
        /// </summary>
        public float Sustainability { get; set; }
        /// <summary>
        /// Gets or sets the fade out time.
        /// </summary>
        public float FadeOutTime { get; set; }
        /// <summary>
        /// Gets or sets the fade in time.
        /// </summary>
        public float FadeInTime { get; set; }
        /// <summary>
        /// Gets or sets the minimum radius.
        /// </summary>
        public int MinimumRadius { get; set; }
        /// <summary>
        /// Gets or sets the maximum radius.
        /// </summary>
        public int MaximumRadius { get; set; }
        /// <summary>
        /// Gets or sets the wind velocity.
        /// </summary>
        public Vector2 WindVelocity { get; set; }
        /// <summary>
        /// Gets or sets the velocity multiplier.
        /// </summary>
        public float VelocityMultiplier { get; set; }
        #endregion

        #region Methods
        /// <summary>
        /// Creates an entity.
        /// </summary>
        public override Entity CreateEntity()
        {
            Particle particle = new DustParticleGray();
            particle.Radius = Shared.Random.Next(this.MinimumRadius, this.MaximumRadius);
            particle.Position = this.Position;

            PhysicsComponent component = new PhysicsComponent(particle);
            particle.Components.Add(component);

            component.Velocity = new Vector2(
                Shared.Random.NextFloat() * 2 - 1.0f,
                Shared.Random.NextFloat() * 2 - 1.0f) * Shared.Random.Next(1, 1 + this.ExpansionStrength) * this.VelocityMultiplier;

            component.Velocity += this.WindVelocity * MathHelper.Max(1.0f, Shared.Random.NextFloat() + 0.4f);
            component.Friction = 0.0f;

            particle.Sustainability = this.Sustainability;
            particle.FadeOutTime = this.FadeOutTime;
            particle.FadeInTime = this.FadeInTime;

            return particle;
        }
        #endregion
    }
}
