using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Platformer.Rendering;
using Platformer.Components;
using Platformer.World.Tiles;

namespace Platformer.World.TileEngine
{
    public class TileMap
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="TileMap"/> class.
        /// </summary>
        public TileMap(int width, int height)
        {
            this._tiles = new TileReference[width, height];

            this.Width = width;
            this.Height = height;

            for (int x = 0; x < this.Width; x++)
            {
                for (int y = 0; y < this.Height; y++)
                {
                    this._tiles[x, y] = new TileReference(this, x, y);
                }
            }

            this.Fill<AirTile>();
        }
        #endregion

        #region Fields
        private TileReference[,] _tiles;
        #endregion

        #region Properties
        /// <summary>
        /// Gets the width.
        /// </summary>
        public int Width { get; private set; }
        /// <summary>
        /// Gets the height.
        /// </summary>
        public int Height { get; private set; }
        #endregion

        #region Methods
        /// <summary>
        /// Sets a tile at a specified point.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        public void SetTile<T>(int x, int y) where T : Tile
        {
            this.SetTile(x, y, TileManager.Instance.Resolve<T>());
        }
        /// <summary>
        /// Sets a tile at a specified point.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <param name="tileID">The tile ID.</param>
        public void SetTile(int x, int y, int tileID)
        {
            this.SetTile(x, y, TileManager.Instance.Resolve(tileID));
        }
        /// <summary>
        /// Sets a tile at a specified point.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <param name="tile">The tile.</param>
        public void SetTile(int x, int y, Tile tile)
        {
            if (x >= 0 && x < this.Width &&
                y >= 0 && y < this.Height)
            {
                this._tiles[x, y].Tile = tile;
            }
        }
        /// <summary>
        /// Gets a tile at a specified point.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        public TileReference GetTile(int x, int y)
        {
            if (x >= 0 && x < this.Width &&
                y >= 0 && y < this.Height)
            {
                return this._tiles[x, y];
            }

            return TileReference.Empty;
        }
        /// <summary>
        /// Fills the map using the specified tile.
        /// </summary>
        /// <param name="tile">The tile.</param>
        public void Fill(Tile tile)
        {
            for (int x = 0; x < this.Width; x++)
            {
                for (int y = 0; y < this.Height; y++)
                {
                    this._tiles[x, y].Tile = tile;
                }
            }
        }
        /// <summary>
        /// Fills the map using the specified tile.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public void Fill<T>() where T : Tile
        {
            this.Fill(TileManager.Instance.Resolve<T>());
        }
        /// <summary>
        /// Renders the specified tile map.
        /// </summary>
        /// <param name="elapsed">The elapsed.</param>
        public void Render(float elapsed)
        {
            IRenderManager renderManager = ComponentManager.Instance.GetComponent<IRenderManager>();
            foreach (TileReference reference in this._tiles)
            {
                reference.Render(elapsed);
            }
        }
        #endregion
    }
}
