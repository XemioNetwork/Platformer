using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Platformer.Math;
using Platformer.World.Entities;

namespace Platformer.World
{
    public class Camera
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="Camera"/> class.
        /// </summary>
        public Camera()
        {
            this.Position = Vector2.Zero;
            this.Speed = 0.15f;
        }
        #endregion

        #region Fields
        private Entity _focusedEntity;
        #endregion

        #region Properties
        /// <summary>
        /// Gets a value indicating whether this camera is on target.
        /// </summary>
        public bool OnTarget
        {
            get { return (this.Position - this.Offset).Length < 0.4f; }
        }
        /// <summary>
        /// Gets the camera offset.
        /// </summary>
        public Vector2 Offset { get; set; }
        /// <summary>
        /// Gets or sets the position.
        /// </summary>
        public Vector2 Position { get; set; }
        /// <summary>
        /// Gets or sets the speed.
        /// </summary>
        public float Speed { get; set; }
        #endregion

        #region Methods
        /// <summary>
        /// Focuses the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public void Focus(Entity entity)
        {
            this._focusedEntity = entity;
        }
        /// <summary>
        /// Unfocuses the object that was used to move the camera.
        /// </summary>
        public void Unfocus()
        {
            this._focusedEntity = null;
        }
        /// <summary>
        /// Handles a game tick.
        /// </summary>
        /// <param name="elapsed">The elapsed.</param>
        public void Tick(float elapsed)
        {
            if (this._focusedEntity != null)
            {
                this.Position = this._focusedEntity.Position;
            }

            this.Offset += (this.Position - this.Offset) * this.Speed;
        }
        #endregion
    }
}
