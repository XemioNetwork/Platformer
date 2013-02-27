using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Platformer.Rendering;
using Platformer.World.TileEngine;

namespace Platformer.World.Tiles
{
    public class StaticTile : Tile
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="StaticTile"/> class.
        /// </summary>
        public StaticTile()
        {
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets a value indicating whether this <see cref="Tile"/> is walkable.
        /// </summary>
        public override bool Walkable
        {
            get { return false; }
        }
        #endregion

        #region Methods
        /// <summary>
        /// Handles a game render request.
        /// </summary>
        /// <param name="renderManager">The render manager.</param>
        /// <param name="reference">The reference.</param>
        public override void Render(TileReference reference)
        {
            if (this.Texture == null)
                return;

            this.RenderManager.Render(this.Texture, reference.CalculateDestination());
        }
        #endregion
    }
}
