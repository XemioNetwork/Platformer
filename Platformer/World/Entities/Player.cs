using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Platformer.World.Entities.Animation;
using Platformer.Math;
using Platformer.Math.Collision;
using Platformer.World.Entities.Particles;
using Platformer.World.Entities.Components;
using Platformer.World.Entities.Components.Physics;
using Platformer.World.Entities.Mobs;
using Platformer.Common.Randomization;
using Platformer.World.TileEngine;
using Platformer.Scenes;
using Platformer.Components;

namespace Platformer.World.Entities
{
    public class Player : Mob
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="Player"/> class.
        /// </summary>
        public Player()
        {
            this.Animations.Add(new EntityAnimation("Idle", 25, Art.Hero));

            this.Animations.Add(new EntityAnimation("Right", 25, Art.HeroRight));
            this.Animations.Add(new EntityAnimation("Left", 25, Art.HeroLeft));
            this.Animations.Add(new EntityAnimation("JumpLeft", 25, Art.HeroJumpLeft));
            this.Animations.Add(new EntityAnimation("JumpRight", 25, Art.HeroJumpRight));

            this.Components.Add(new InputComponent(this));
            this.Components.Add(new GoldComponent(this));

            CollidableComponent collidableComponent = this.GetComponent<CollidableComponent>();
            collidableComponent.BoundingBox = new Rectangle(10, 24, 48, 68);

            LifeComponent lifeComponent = this.GetComponent<LifeComponent>();
            lifeComponent.Set(6);

            this.Components.Add(new InventoryComponent(this));
        }
        #endregion

        #region Fields
        private Camera _levelCamera;
        #endregion

        #region Methods
        /// <summary>
        /// Destroys this entity and removes it from the current level.
        /// </summary>
        public override void Destroy()
        {
            this._levelCamera = this.Level.Camera;
            this._levelCamera.Unfocus();

            base.Destroy();
        }
        /// <summary>
        /// Handles a game tick.
        /// </summary>
        /// <param name="elapsed">The elapsed.</param>
        public override void Tick(float elapsed)
        {
            if (this.Position.Y > (this.Level.Height + 4) * Tile.Height)
            {
                this.Destroy();
            }

            base.Tick(elapsed);
        }
        #endregion
    }
}
