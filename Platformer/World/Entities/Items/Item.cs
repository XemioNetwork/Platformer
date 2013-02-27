using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Platformer.World.Entities.Components;
using Platformer.World.Entities.Events;
using Platformer.Rendering;
using Platformer.Math;
using Platformer.World.Entities.Components.Physics;
using Platformer.Events;

namespace Platformer.World.Entities.Items
{
    public abstract class Item : Entity
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="Item"/> class.
        /// </summary>
        public Item()
        {
            this.Components.Add(new CollidableComponent(this, new Rectangle(0, 0, 1, 1), false));
            this.Components.Add(new GravityComponent(this));
        }
        #endregion

        #region Fields
        private float _yOffset;
        private float _elapsed;
        #endregion

        #region Properties
        /// <summary>
        /// Gets the texture.
        /// </summary>
        public virtual ITexture Texture
        {
            get { return null; }
        }
        #endregion

        #region Methods
        /// <summary>
        /// Lets the entity pickup this item.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public virtual void Pickup(Entity entity)
        {
            InventoryComponent inventory = entity.GetComponent<InventoryComponent>();
            inventory.Items.Add(this);

            entity.Send(new PickupItemEvent(entity, this));

            this.Destroy();
        }
        /// <summary>
        /// Determines whether the specified entity can pickup this item.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public virtual bool CanPickup(Entity entity)
        {
            return entity.HasComponent<InventoryComponent>();
        }
        /// <summary>
        /// Uses this item.
        /// </summary>
        public virtual void Use()
        {
        }
        /// <summary>
        /// Handles an entity event.
        /// </summary>
        /// <param name="e">The event.</param>
        public override void HandleEvent(Event e)
        {
            EntityCollisionEvent collisionEvent = e as EntityCollisionEvent;
            if (collisionEvent != null)
            {
                Entity entity = collisionEvent.Entity;
                if (entity == this)
                {
                    entity = collisionEvent.CollidingEntity;
                }

                if (this.CanPickup(entity))
                {
                    this.Pickup(entity);
                }
            }
        }
        /// <summary>
        /// Handles a game tick.
        /// </summary>
        /// <param name="elapsed">The elapsed.</param>
        public override void Tick(float elapsed)
        {
            this._elapsed += elapsed;
            this._yOffset = MathHelper.Sin(this._elapsed / 300.0f) * 8;

            CollidableComponent component = this.GetComponent<CollidableComponent>();
            if (component.BoundingBox.Width != this.Texture.Width ||
                component.BoundingBox.Height != this.Texture.Height)
            {
                component.BoundingBox = new Rectangle(
                    0, 0, this.Texture.Width, this.Texture.Height);
            }

            base.Tick(elapsed);
        }
        /// <summary>
        /// Handles a game render request.
        /// </summary>
        /// <param name="elapsed">The elapsed.</param>
        public override void Render(float elapsed)
        {
            this.RenderManager.Render(this.Texture,
                new Rectangle(
                    this.Position.X,
                    this.Position.Y - 8 + this._yOffset, 
                    this.Texture.Width,
                    this.Texture.Height));

            base.Render(elapsed);
        }
        #endregion
    }
}
