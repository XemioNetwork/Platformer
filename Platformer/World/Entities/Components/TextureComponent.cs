using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Platformer.Rendering;
using Platformer.Math;

namespace Platformer.World.Entities.Components
{
    public class TextureComponent : EntityComponent
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="TextureComponent"/> class.
        /// </summary>
        /// <param name="entity"></param>
        public TextureComponent(Entity entity) : base(entity)
        {
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the texture.
        /// </summary>
        public ITexture Texture { get; set; }
        /// <summary>
        /// Gets or sets the texture offset.
        /// </summary>
        public Vector2 Offset { get; set; }
        #endregion

        #region Methods
        /// <summary>
        /// Handles a game render request.
        /// </summary>
        /// <param name="elapsed">The elapsed.</param>
        public override void Render(float elapsed)
        {
            this.Entity.RenderManager.Render(
                this.Texture,
                this.Entity.Position + this.Entity.Offset + this.Offset);
        }
        #endregion
    }
}
