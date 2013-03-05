using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Platformer.World;
using Platformer.Common.Randomization;
using Platformer.World.Entities.Mobs;
using Platformer.Math;
using Platformer.World.Tiles;

namespace Platformer.Scenes.Menu
{
    /// <summary>
    /// Implementation for the menu level.
    /// </summary>
    public class MenuLevel : Level
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="MenuLevel"/> class.
        /// </summary>
        public MenuLevel() : base(30, 20)
        {
        }
        #endregion

        #region Level Member
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public override string Name
        {
            get { return "Menulevel"; }
        }
        /// <summary>
        /// Builds the level.
        /// </summary>
        protected override void BuildLevel()
        {
            IRandom r = new RandomProxy(this.Name);

            for (int x = 0; x < this.Width; x++)
            {
                int height = (int)(x * 0.2f) + 3;

                for (int y = height; y < this.Height; y++)
                {
                    if (y == height)
                    {
                        this.TileLayer[1].SetTile<GrassTile>(x, y);
                    }
                    else
                    {
                        this.TileLayer[1].SetTile<AirTile>(x, y);
                    }
                }
            }

            this.Add(new SlimeSpawner
            {
                Position = new Vector2(70 * 26, 70 * 7),
                MaximumEntities = 1,
                SpawnDelay = 100,
                SpawnRadius = 2000,
                Visible = false
            });
        }
        #endregion
    }
}
