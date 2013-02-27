using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Platformer.Events;

namespace Platformer.World.Entities.Components
{
    public class EntityComponent
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="EntityComponent"/> class.
        /// </summary>
        public EntityComponent(Entity entity)
        {
            this.Entity = entity;
            this.Enabled = true;
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="EntityComponent"/> is enabled.
        /// </summary>
        public bool Enabled { get; set; }
        /// <summary>
        /// Gets the entity.
        /// </summary>
        public Entity Entity { get; private set; }
        #endregion

        #region Methods
        /// <summary>
        /// Handles an entity event.
        /// </summary>
        /// <param name="entityEvent">The event.</param>
        public virtual void HandleEvent(Event entityEvent)
        {
        }
        /// <summary>
        /// Handles a game tick.
        /// </summary>
        /// <param name="elapsed">The elapsed.</param>
        public virtual void Tick(float elapsed)
        {
        }
        /// <summary>
        /// Handles a game render request.
        /// </summary>
        /// <param name="elapsed">The elapsed.</param>
        public virtual void Render(float elapsed)
        {
        }
        #endregion
    }
}
