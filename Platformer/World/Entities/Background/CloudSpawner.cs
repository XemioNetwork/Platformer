using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Platformer.Math;
using Platformer.World.TileEngine;
using Platformer.World.Entities.Particles;

namespace Platformer.World.Entities.Background
{
    public class CloudSpawner : Entity
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="CloudSpawner"/> class.
        /// </summary>
        public CloudSpawner()
        {
            this.MaxClouds = 32;
            //TODO: base spawner class.
        }
        #endregion

        #region Fields
        private float _elapsed;
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the max clouds.
        /// </summary>
        public int MaxClouds { get; set; }
        #endregion

        #region Methods
        /// <summary>
        /// Handles a game tick.
        /// </summary>
        /// <param name="elapsed">The elapsed.</param>
        public override void Tick(float elapsed)
        {
            this._elapsed += elapsed;

            int clouds = this.Level.Entities.Count(
                entity => entity is Cloud);

            if (clouds < this.MaxClouds && this._elapsed >= 200)
            {
                Vector2 position = new Vector2(
                    Shared.Random.Next(
                        this.Level.Width * Tile.Width + 512,
                        this.Level.Width * Tile.Height + 1024),
                    Shared.Random.Next(-256, this.Level.Height * Tile.Height));

                this._elapsed = 0;
                this.Spawn(new Cloud { Position = position });
            }

            base.Tick(elapsed);
        }
        #endregion
    }
}
