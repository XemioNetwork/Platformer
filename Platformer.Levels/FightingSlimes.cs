using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Platformer.World;
using Platformer.World.Entities;
using Platformer.World.Tiles.Locks;
using Platformer.Math;
using Platformer.World.Entities.Items;
using Platformer.World.Tiles;
using Platformer.World.Entities.Mobs;

namespace Platformer.Levels
{
    public class FightingSlimes : Level
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="FightingSlimes"/> class.
        /// </summary>
        public FightingSlimes() : base(40, 20)
        {
        }
        #endregion

        #region Level Member
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public override string Name
        {
            get { return "02. Fighting Slimes"; }
        }
        /// <summary>
        /// Gets the camera start position.
        /// </summary>
        public override Vector2 CameraStartPosition
        {
            get { return new Vector2(300, 0); }
        }
        /// <summary>
        /// Gets the preview location.
        /// </summary>
        public override Vector2 PreviewLocation
        {
            get { return new Vector2(500, 0); }
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
                for (int y = 1; y < this.Height; y++)
                {
                    if (y == 1)
                    {
                        this.TileLayer[1].SetTile<GrassTile>(x, y);
                    }
                    else
                    {
                        this.TileLayer[1].SetTile<DirtTile>(x, y);
                    }
                }
            }

            this.Add(new TextEntity("Watch out! Slimes are dangerous.", Art.FontBlack, Art.FontWhite)
            {
                Position = new Vector2(70 * 5, -80)
            });

            this.Add(new SlimeSpawner { Position = new Vector2(490, 0) });
        }
        #endregion
    }
}
