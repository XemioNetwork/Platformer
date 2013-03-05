using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Platformer.Rendering;
using Platformer.World.Entities.Items;

namespace Platformer.World.Tiles.Locks
{
    public class GreenLockTile : LockTile
    {
        #region Properties
        /// <summary>
        /// Gets the tile texture.
        /// </summary>
        public override ITexture Texture
        {
            get { return Art.LockGreen; }
        }
        /// <summary>
        /// Gets the type of the item that is needed to open the lock.
        /// </summary>
        public override Type KeyType
        {
            get { return typeof(GreenKey); }
        }
        #endregion
    }
}
