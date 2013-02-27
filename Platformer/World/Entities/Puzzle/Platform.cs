using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Platformer.World.Entities.Components;
using Platformer.Math;
using Platformer.Rendering;

namespace Platformer.World.Entities.Puzzle
{
    public class Platform : Entity
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="Platform"/> class.
        /// </summary>
        public Platform()
        {
            this._traveledDistance = 0;
            this._directionMultiplier = 1;

            CollidableComponent collidableComponent = new CollidableComponent(this);
            collidableComponent.BoundingBox = new Rectangle(0, 0, Art.Plank.Width, Art.Plank.Height);
            collidableComponent.Static = true;

            this.Components.Add(collidableComponent);
        }
        #endregion

        #region Fields
        private float _traveledDistance;
        private int _directionMultiplier;
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the direction.
        /// </summary>
        public Vector2 Direction { get; set; }
        /// <summary>
        /// Gets or sets the distance.
        /// </summary>
        public float Distance { get; set; }
        /// <summary>
        /// Gets the texture.
        /// </summary>
        public ITexture Texture
        {
            get { return Art.Plank; }
        }
        #endregion

        #region Methods
        /// <summary>
        /// Handles a game tick.
        /// </summary>
        /// <param name="elapsed">The elapsed.</param>
        public override void Tick(float elapsed)
        {
            this._traveledDistance += this.Direction.Length;
            this.Position += this.Direction * this._directionMultiplier;

            if (this._traveledDistance >= this.Distance)
            {
                this._traveledDistance = 0;
                this._directionMultiplier *= -1;
            }

            base.Tick(elapsed);
        }
        /// <summary>
        /// Handles a game render request.
        /// </summary>
        /// <param name="elapsed">The elapsed.</param>
        public override void Render(float elapsed)
        {
            this.RenderManager.Render(
                this.Texture,
                new Rectangle(0, 0, this.Texture.Width, this.Texture.Height)
                + this.Position + this.Offset);

            base.Render(elapsed);
        }
        #endregion
    }
}
