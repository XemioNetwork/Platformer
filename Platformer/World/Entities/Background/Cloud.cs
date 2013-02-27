using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Platformer.World.Entities.Components;
using Platformer.World.Entities.Components.Physics;
using Platformer.Math;
using Platformer.World.TileEngine;

namespace Platformer.World.Entities.Background
{
    public class Cloud : Entity
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="Cloud"/> class.
        /// </summary>
        public Cloud()
        {
            PhysicsComponent physics = new PhysicsComponent(this);
            physics.Velocity -= new Vector2(
                Shared.Random.Next(1, 3) * 
                Shared.Random.NextFloat() + 0.6f, 0);

            physics.Friction = 0;

            TextureComponent texture = new TextureComponent(this);
            texture.Texture = Art.Clouds[Shared.Random.Next(Art.Clouds.Length)];
            
            this.Components.Add(physics);
            this.Components.Add(texture);

            this.LayerIndex = Shared.Random.NextBoolean(0.7) ? 0 : 1;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Handles a game tick.
        /// </summary>
        /// <param name="elapsed">The elapsed.</param>
        public override void Tick(float elapsed)
        {
            if (this.Position.X < -512)
            {
                this.Destroy();
            }

            base.Tick(elapsed);
        }
        #endregion
    }
}
