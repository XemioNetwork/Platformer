using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Platformer.Math;
using Platformer.Common;
using Platformer.Rendering;
using Platformer.World.Tiles;

namespace Platformer.World.TileEngine
{
    public class TileReference
    {
        #region Constructors
        /// <summary>
        /// Prevents a default instance of the <see cref="TileReference"/> class from being created.
        /// </summary>
        private TileReference()
        {
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="TileReference"/> class.
        /// </summary>
        public TileReference(TileMap map, int x, int y)
        {
            this.Map = map;
            this.X = x;
            this.Y = y;
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="TileReference"/> class.
        /// </summary>
        /// <param name="map">The map.</param>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <param name="tile">The tile.</param>
        public TileReference(TileMap map, int x, int y, Tile tile) : this(map, x, y)
        {
            this.Tile = tile;
        }
        #endregion

        #region Static Fields
        private static TileReference _empty = new TileReference();
        #endregion

        #region Static Properties
        /// <summary>
        /// Gets an empty tile reference.
        /// </summary>
        public static TileReference Empty
        {
            get { return TileReference._empty; }
        }
        #endregion

        #region Properties
        public TileMap Map { get; private set; }
        /// <summary>
        /// Gets the X coordinate.
        /// </summary>
        public int X { get; private set; }
        /// <summary>
        /// Gets the Y coordinate.
        /// </summary>
        public int Y { get; private set; }
        /// <summary>
        /// Gets or sets the tile.
        /// </summary>
        public Tile Tile { get; set; }
        #endregion

        #region Methods
        /// <summary>
        /// Destroys this instance.
        /// </summary>
        public void Destroy()
        {
            this.Map.SetTile<AirTile>(this.X, this.Y);
        }
        /// <summary>
        /// Renders the specified tile instance.
        /// </summary>
        /// <param name="elapsed">The elapsed.</param>
        public void Render(float elapsed)
        {
            if (this.Tile != null)
            {
                this.Tile.Render(this);
            }
        }
        /// <summary>
        /// Calculates the tile destination.
        /// </summary>
        public Rectangle CalculateDestination()
        {
            return new Rectangle(
                this.X * Tile.Width + this.Tile.Offset.X,
                this.Y * Tile.Height + this.Tile.Offset.Y,
                Tile.Width,
                Tile.Height);
        }
        #endregion
    }
}
