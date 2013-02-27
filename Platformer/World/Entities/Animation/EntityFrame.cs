using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Platformer.Rendering;
using Platformer.Math;

namespace Platformer.World.Entities.Animation
{
    public class EntityFrame
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="EntityFrame"/> class.
        /// </summary>
        public EntityFrame()
        {
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="EntityFrame"/> class.
        /// </summary>
        /// <param name="texture">The texture.</param>
        public EntityFrame(ITexture texture)
        {
            this.Texture = texture;
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="EntityFrame"/> class.
        /// </summary>
        /// <param name="texture">The texture.</param>
        /// <param name="offset">The offset.</param>
        public EntityFrame(ITexture texture, Vector2 offset) : this(texture)
        {
            this.Offset = offset;
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the offset.
        /// </summary>
        public Vector2 Offset { get; set; }
        /// <summary>
        /// Gets or sets the texture.
        /// </summary>
        public ITexture Texture { get; set; }
        #endregion
    }
}
