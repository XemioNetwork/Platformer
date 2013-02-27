using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Platformer.Common;
using Platformer.Common.Linkers;

namespace Platformer.World.TileEngine
{
    public class TileManager
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="TileManager"/> class.
        /// </summary>
        public TileManager()
        {
            this._tiles = new AutomaticLinker<int, Tile>();
        }
        #endregion

        #region Fields
        private GenericLinker<int, Tile> _tiles;
        #endregion

        #region Singleton
        /// <summary>
        /// Gets the singleton instance.
        /// </summary>
        public static TileManager Instance
        {
            get { return Singleton<TileManager>.Value; }
        }
        #endregion

        #region Methods
        /// <summary>
        /// Resolves the specified tile ID.
        /// </summary>
        /// <param name="tileID">The tile ID.</param>
        public Tile Resolve(int tileID)
        {
            return this._tiles.Resolve(tileID);
        }
        /// <summary>
        /// Resolves the specified tile name.
        /// </summary>
        /// <param name="tileName">Name of the tile.</param>
        /// <returns></returns>
        public Tile Resolve(string tileName)
        {
            return this._tiles.FirstOrDefault(tile => tile.GetType().Name == tileName);
        }
        /// <summary>
        /// Resolves a tile by its type.
        /// </summary>
        public Tile Resolve<T>() where T : Tile
        {
            return this._tiles.FirstOrDefault(tile => tile is T);
        }
        #endregion
    }
}
