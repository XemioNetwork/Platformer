using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Platformer.Rendering;
using Platformer.World.TileEngine;

namespace Platformer.World.Tiles
{
    public class GrassTile : StaticTile
    {
        #region Constructors
        public GrassTile()
        {

        }
        #endregion

        #region Fields

        #endregion

        #region Properties
        /// <summary>
        /// Gets the tile texture.
        /// </summary>
        public override ITexture Texture
        {
            get { return Art.Ground; }
        }
        #endregion
    }
}
