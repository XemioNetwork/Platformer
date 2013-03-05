using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Platformer.World.TileEngine;
using Platformer.World.Entities.Particles;
using Platformer.World.Entities;
using Platformer.Math;
using Platformer.World.Entities.Items;
using Platformer.World.Entities.Puzzle;
using Platformer.World.Tiles;
using Platformer.World.Tiles.Locks;
using Platformer.World.Entities.Mobs;

namespace Platformer.World.Levels
{
    public class TestLevel : Level
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="TestLevel"/> class.
        /// </summary>
        public TestLevel() : base(60, 10)
        {
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public override string Name
        {
            get { return "TestLevel"; }
        }
        /// <summary>
        /// Gets the preview location.
        /// </summary>
        public override Vector2 PreviewLocation
        {
            get { return new Vector2(400, 240); }
        }
        #endregion

        #region Methods
        /// <summary>
        /// Builds the level.
        /// </summary>
        protected override void BuildLevel()
        {
            for (int x = 0; x < this.Width; x++)
            {
                for (int y = 5; y < this.Height; y++)
                {
                    if (y == 5)
                    {
                        this.TileLayer[1].SetTile<GrassTile>(x, y);
                    }
                    else
                    {
                        this.TileLayer[1].SetTile<DirtTile>(x, y);
                    }
                }

                if (x == 6)
                {
                    this.TileLayer[1].SetTile<BlueLockTile>(x, 4);
                }
            }

            this.Add(new TextEntity("Use the key to open the lock.", Art.FontBlack, Art.FontWhite)
            {
                Position = new Vector2(70 * 3, 200)
            });

            this.Add(new TextEntity("Push the crate around using arrow keys.", Art.FontBlack, Art.FontWhite)
            {
                Position = new Vector2(70 * 12, 200)
            });

            this.Add(new TextEntity("Platforms can be pretty useful.", Art.FontBlack, Art.FontWhite)
            {
                Position = new Vector2(70 * 22, 200)
            });

            this.Add(new BlueKey { Position = new Vector2(70 * 5, 200) });
            this.Add(new Crate { Position = new Vector2(70 * 15, 200) });

            this.Add(new Platform
            {
                Position = new Vector2(70 * 24, 150),
                Direction = new Vector2(0, 1),
                Distance = 200
            });
        }
        #endregion
    }
}
