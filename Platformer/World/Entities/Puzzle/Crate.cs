using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Platformer.World.Entities.Components;
using Platformer.World.Entities.Components.Physics;
using Platformer.Math;
using Platformer.Rendering;

namespace Platformer.World.Entities.Puzzle
{
    public class Crate : Entity
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="Crate"/> class.
        /// </summary>
        public Crate()
        {
            this.Components.Add(new GravityComponent(this));
            this.Components.Add(new PhysicsComponent(this));

            this.Components.Add(new CollidableComponent(this, new Rectangle(0, 0, 70, 70)));
        }
        #endregion
        
        #region Properties
        /// <summary>
        /// Gets the texture.
        /// </summary>
        public ITexture Texture
        {
            get { return Art.Crate; }
        }
        #endregion

        #region Methods
        /// <summary>
        /// Handles a game render request.
        /// </summary>
        /// <param name="elapsed">The elapsed.</param>
        public override void Render(float elapsed)
        {
            this.RenderManager.Render(this.Texture,
                new Rectangle(
                    0, 0, this.Texture.Width, this.Texture.Height) + this.Position + this.Offset);

            base.Render(elapsed);
        }
        #endregion
    }
}
