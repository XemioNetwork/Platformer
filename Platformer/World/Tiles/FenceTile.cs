using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Platformer.Rendering;
using Platformer.Math;
using Platformer.World.TileEngine;

namespace Platformer.World.Tiles
{
    public class FenceTile : StaticTile
    {
        #region Properties
        /// <summary>
        /// Gets the tile texture.
        /// </summary>
        public override ITexture Texture
        {
            get { return Art.Fence; }
        }
        /// <summary>
        /// Gets a value indicating whether this <see cref="Tile"/> is walkable.
        /// </summary>
        public override bool Walkable
        {
            get { return true; }
        }
        /// <summary>
        /// Gets the offset.
        /// </summary>
        public override Vector2 Offset
        {
            get { return new Vector2(0, 14); }
        }
        #endregion
    }
}
