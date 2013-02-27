using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Platformer.Rendering;
using Platformer.Math;
using Platformer.World.Entities.Components.Physics;
using Platformer.World.Entities.Components;
using Platformer.World.TileEngine;
using Platformer.World.Tiles;

namespace Platformer.World.Entities.Puzzle
{
    public class Lock : Entity
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="Lock"/> class.
        /// </summary>
        public Lock(Level level, TileReference lockReference)
        {
            this.Level = level;
            this.Reference = lockReference;
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the texture inset.
        /// </summary>
        protected int Inset { get; set; }
        /// <summary>
        /// Gets or sets the reference.
        /// </summary>
        protected TileReference Reference { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="Lock"/> is opened.
        /// </summary>
        public bool Opened { get; set; }
        /// <summary>
        /// Gets or sets the texture.
        /// </summary>
        public ITexture Texture { get; set; }
        #endregion

        #region Methods
        /// <summary>
        /// Checks the surrounding tiles.
        /// </summary>
        private void CheckSurroundingTiles()
        {
            for (int x = -1; x <= 1; x++)
            {
                for (int y = -1; y <= 1; y++)
                {
                    if (x == y || -x == y)
                        continue;

                    TileReference reference = this.Reference.Map
                        .GetTile(this.Reference.X + x, this.Reference.Y + y);

                    if (reference != TileReference.Empty &&
                        reference.Tile is LockTile)
                    {
                        LockTile lockTile = reference.Tile as LockTile;
                        lockTile.Open(this.Level, reference);
                    }
                }
            }
        }
        /// <summary>
        /// Opens this lock.
        /// </summary>
        public void Open()
        {
            this.Opened = true;
        }
        /// <summary>
        /// Handles a game tick.
        /// </summary>
        /// <param name="elapsed">The elapsed.</param>
        public override void Tick(float elapsed)
        {
            if (this.Opened)
            {
                this.Inset += 6;
                this.CheckSurroundingTiles();

                if (this.Inset > this.Texture.Width)
                {
                    this.Destroy();
                }
            }

            base.Tick(elapsed);
        }
        /// <summary>
        /// Handles a game render request.
        /// </summary>
        /// <param name="elapsed">The elapsed.</param>
        public override void Render(float elapsed)
        {
            CollidableComponent component = this.GetComponent<CollidableComponent>();

            this.RenderManager.Render(
                this.Texture,
                new Rectangle(
                    this.Inset * 0.5f,
                    this.Inset * 0.5f,
                    MathHelper.Max(this.Texture.Width - this.Inset, 0),
                    MathHelper.Max(this.Texture.Height - this.Inset, 0))
                + this.Position + this.Offset);

            base.Render(elapsed);
        }
        #endregion
    }
}
