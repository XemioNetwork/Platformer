using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Platformer.Rendering;
using Platformer.Math;
using Platformer.World.Entities.Components;

namespace Platformer.World.Entities.Particles
{
    public abstract class Particle : Entity
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="Particle"/> class.
        /// </summary>
        public Particle()
        {
        }
        #endregion

        #region Fields
        private float _elapsed;
        #endregion

        #region Properties
        /// <summary>
        /// Gets the radius.
        /// </summary>
        public float Radius { get; set; }
        /// <summary>
        /// Gets or sets the fade time.
        /// </summary>
        public float FadeInTime { get; set; }
        /// <summary>
        /// Gets or sets the sustainability.
        /// </summary>
        public float Sustainability { get; set; }
        /// <summary>
        /// Gets or sets the fade time.
        /// </summary>
        public float FadeOutTime { get; set; }
        /// <summary>
        /// Gets or sets the texture.
        /// </summary>
        public abstract ITexture Texture { get; }
        #endregion

        #region Methods
        /// <summary>
        /// Handles a game tick.
        /// </summary>
        /// <param name="elapsed">The elapsed.</param>
        public override void Tick(float elapsed)
        {
            this._elapsed += elapsed;
            base.Tick(elapsed);
        }
        /// <summary>
        /// Handles a game render request.
        /// </summary>
        /// <param name="elapsed">The elapsed.</param>
        public override void Render(float elapsed)
        {
            float radius = this.Radius;
            if (this._elapsed < this.FadeInTime)
            {
                float percentage = this._elapsed / MathHelper.Max(1, this.FadeInTime);
                radius = this.Radius * percentage;
            }
            if (this._elapsed > this.Sustainability + this.FadeInTime)
            {
                float percentage = (this._elapsed - (this.Sustainability + this.FadeInTime)) / MathHelper.Max(1, this.FadeOutTime);
                radius = this.Radius - percentage * this.Radius;

                if (percentage >= 1.0f)
                {
                    this.Destroy();
                }
            }

            this.RenderManager.Render(
                this.Texture,
                new Rectangle(-radius, -radius, radius * 2, radius * 2) + this.Position + this.Offset,
                new Rectangle(0, 0, this.Texture.Width, this.Texture.Height));

            base.Render(elapsed);
        }
        #endregion
    }
}
