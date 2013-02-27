using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Platformer.Math;
using Platformer.Math.Collision;
using Platformer.Components;

namespace Platformer.World.Entities.Components
{
    public class CollidableComponent : EntityComponent
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="CollidableComponent"/> class.
        /// </summary>
        /// <param name="entity"></param>
        public CollidableComponent(Entity entity) : this(entity, Rectangle.Empty)
        {
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="CollidableComponent"/> class.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <param name="boundingBox">The bounding box.</param>
        public CollidableComponent(Entity entity, Rectangle boundingBox) : this(entity, boundingBox, true)
        {
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="CollidableComponent"/> class.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <param name="boundingBox">The bounding box.</param>
        public CollidableComponent(Entity entity, Rectangle boundingBox, bool separation) : base(entity)
        {
            this.BoundingBox = boundingBox;
            this.Seperatation = separation;

            this.Entity.PositionChanged += new EventHandler(Entity_PositionChanged);
            this.ExcludedTypes = new List<Type>();
        }
        #endregion
        
        #region Properties
        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="CollidableComponent"/> is dirty.
        /// </summary>
        public bool Dirty { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="CollidableComponent"/> is seperatating the colliding entities.
        /// </summary>
        public bool Seperatation { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="CollidableComponent"/> is static.
        /// </summary>
        public bool Static { get; set; }
        /// <summary>
        /// Gets the excluded types.
        /// </summary>
        public List<Type> ExcludedTypes { get; private set; }
        /// <summary>
        /// Gets or sets the bounding box.
        /// </summary>
        public Rectangle BoundingBox { get; set; }
        #endregion

        #region Methods
        /// <summary>
        /// Handles a game render request.
        /// </summary>
        /// <param name="elapsed">The elapsed.</param>
        public override void Render(float elapsed)
        {
            DebugComponent debugComponent = ComponentManager.Instance.GetComponent<DebugComponent>();

            if (debugComponent != null && debugComponent.Debug)
            {
                this.Entity.RenderManager.Render(
                    Art.Rectangle,
                    this.BoundingBox + this.Entity.Position);
            }
        }
        #endregion

        #region Event Handlers
        /// <summary>
        /// Handles the PositionChanged event of the Entity.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void Entity_PositionChanged(object sender, EventArgs e)
        {
            this.Dirty = true;
        }
        #endregion
    }
}
