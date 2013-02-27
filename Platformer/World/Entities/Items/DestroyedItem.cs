using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Platformer.Rendering;
using Platformer.World.Entities.Components;
using Platformer.World.Entities.Components.Physics;
using Platformer.Math;
using Platformer.Events;
using Platformer.World.Entities.Events;
using Platformer.Math.Collision;

namespace Platformer.World.Entities.Items
{
    public class DestroyedItem : Entity
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="DestroyedItem"/> class.
        /// </summary>
        /// <param name="owner">The owner.</param>
        /// <param name="item">The item.</param>
        public DestroyedItem(Entity owner, Item item)
        {
            this.Item = item;
            this.Percentage = 1.0f;

            this.Visible = true;
            this.Flickering = false;

            this.Components.Add(new GravityComponent(this));
            this.Components.Add(new CollidableComponent(this, new Rectangle(0, 0, item.Texture.Width, item.Texture.Height)));

            PhysicsComponent physics = new PhysicsComponent(this, 0.05f);
            PhysicsComponent ownerPhysics = owner.GetComponent<PhysicsComponent>();

            physics.Acceleration = new Vector2(0, -0.5f);
            physics.Velocity += new Vector2(0, -8);

            if (ownerPhysics.Velocity.X > 0)
            {
                physics.Velocity += new Vector2(-4, 0);
            }
            if (ownerPhysics.Velocity.X < 0)
            {
                physics.Velocity += new Vector2(4, 0);
            }

            this.Components.Add(physics);
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the item.
        /// </summary>
        public Item Item { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="DestroyedItem"/> is visible.
        /// </summary>
        public bool Visible { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="DestroyedItem"/> is flickering.
        /// </summary>
        public bool Flickering { get; set; }
        /// <summary>
        /// Gets or sets the percentage.
        /// </summary>
        public float Percentage { get; set; }
        #endregion

        #region Methods
        /// <summary>
        /// Handles a game tick.
        /// </summary>
        /// <param name="elapsed">The elapsed.</param>
        public override void Tick(float elapsed)
        {
            this.Percentage *= 0.98f;
            if (this.Percentage < 0.25f)
            {
                this.Destroy();
            }
            else if (this.Percentage < 0.8f)
            {
                this.Flickering = true;
            }

            CollidableComponent component = this.GetComponent<CollidableComponent>();
            if (component != null)
            {
                component.BoundingBox = new Rectangle(
                    0, 0,
                    this.Item.Texture.Width * this.Percentage,
                    this.Item.Texture.Height * this.Percentage);
            }

            if (this.Flickering)
            {
                this.Visible = !this.Visible;
            }

            base.Tick(elapsed);
        }
        /// <summary>
        /// Handles a game render request.
        /// </summary>
        /// <param name="elapsed">The elapsed.</param>
        public override void Render(float elapsed)
        {
            if (this.Visible)
            {
                this.RenderManager.Render(
                    this.Item.Texture,
                    new Rectangle(
                        this.Position.X + (this.Item.Texture.Width - this.Item.Texture.Width * this.Percentage) * 0.5f,
                        this.Position.Y + (this.Item.Texture.Height - this.Item.Texture.Height * this.Percentage) * 0.5f,
                        this.Item.Texture.Width * this.Percentage,
                        this.Item.Texture.Height * this.Percentage));
            }

            base.Render(elapsed);
        }
        #endregion
    }
}
