using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Platformer.Math;
using Platformer.World.Entities.Components.Physics;
using Platformer.World.Entities.Components;
using Platformer.World.Entities.Mobs;

namespace Platformer.World.Entities.Items
{
    public class Coin : Entity
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="Coin"/> class.
        /// </summary>
        public Coin()
        {
            this.Components.Add(new GravityComponent(this));
            this.Components.Add(new PhysicsComponent(this));

            CollidableComponent collidableComponent = new CollidableComponent(this);
            collidableComponent.BoundingBox = new Rectangle(0, 0, Art.Coin.Width, Art.Coin.Height);

            collidableComponent.ExcludedTypes.Add(typeof(Mob));
            collidableComponent.ExcludedTypes.Add(typeof(Coin));

            this.Components.Add(collidableComponent);
        }
        #endregion

        #region Fields
        private float _elapsed;
        #endregion

        #region Properties
        /// <summary>
        /// Gets a value indicating whether this <see cref="Coin"/> is collectable.
        /// </summary>
        public bool Collectable
        {
            get { return this._elapsed >= 400; }
        }
        #endregion

        #region Methods
        /// <summary>
        /// Handles a game tick.
        /// </summary>
        /// <param name="elapsed">The elapsed.</param>
        public override void Tick(float elapsed)
        {
            this._elapsed += elapsed;
            base.Tick(elapsed);
        }
        /// <summary>
        /// Handles a game render request.
        /// </summary>
        /// <param name="elapsed">The elapsed.</param>
        public override void Render(float elapsed)
        {
            this.RenderManager.Render(Art.Coin,
                new Rectangle(this.Position.X, this.Position.Y, Art.Coin.Width, Art.Coin.Height));

            base.Render(elapsed);
        }
        #endregion
    }
}
