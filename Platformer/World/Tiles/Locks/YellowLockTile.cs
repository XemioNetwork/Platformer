using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Platformer.World.Entities.Items;
using Platformer.Rendering;

namespace Platformer.World.Tiles.Locks
{
    public class YellowLockTile : LockTile
    {
        #region Properties
        /// <summary>
        /// Gets the tile texture.
        /// </summary>
        public override ITexture Texture
        {
            get { return Art.LockRed; }
        }
        /// <summary>
        /// Gets the type of the item that is needed to open the lock.
        /// </summary>
        public override Type KeyType
        {
            get { return typeof(YellowKey); }
        }
        #endregion
    }
}
