using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Platformer.Math;
using Platformer.World;
using Platformer.Rendering;
using Platformer.Components;
using Platformer.World.Entities;
using Platformer.World.Entities.Components;
using Platformer.Events;

namespace Platformer.World.Entities
{
    public class Entity
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="Entity"/> class.
        /// </summary>
        public Entity()
        {
            this.Components = new List<EntityComponent>();
            this.LayerIndex = 1;
            this.Visible = true;
        }
        #endregion

        #region Fields
        private Vector2 _position;
        #endregion

        #region Events
        public event EventHandler PositionChanged;
        #endregion

        #region Properties
        /// <summary>
        /// Gets the components.
        /// </summary>
        public List<EntityComponent> Components { get; private set; }
        /// <summary>
        /// Gets or sets the position.
        /// </summary>
        public Vector2 Position 
        {
            get { return this._position; }
            set
            {
                this._position = value;
                if (this.PositionChanged != null)
                {
                    this.PositionChanged(this, EventArgs.Empty);
                }
            }
        }
        /// <summary>
        /// Gets or sets the index of the layer.
        /// </summary>
        public int LayerIndex { get; set; }
        /// <summary>
        /// Gets or sets the level.
        /// </summary>
        public Level Level { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="Entity"/> is visible.
        /// </summary>
        public bool Visible { get; set; }
        /// <summary>
        /// Gets the offset.
        /// </summary>
        public virtual Vector2 Offset
        {
            get { return Vector2.Zero; }
        }
        /// <summary>
        /// Gets the render manager.
        /// </summary>
        public IRenderManager RenderManager
        {
            get { return ComponentManager.Instance.GetComponent<IRenderManager>(); }
        }
        #endregion

        #region Component Methods
        /// <summary>
        /// Gets the specified component.
        /// </summary>
        public T GetComponent<T>() where T : EntityComponent
        {
            return this.Components.FirstOrDefault(component => component is T) as T;
        }
        /// <summary>
        /// Determines whether this instance has a specified component.
        /// </summary>
        public bool HasComponent<T>() where T : EntityComponent
        {
            return this.Components.Any(component => component is T);
        }
        #endregion

        #region Level Methods
        /// <summary>
        /// Spawns the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public void Spawn(Entity entity)
        {
            if (this.Level != null)
            {
                this.Level.Add(entity);
            }
        }
        /// <summary>
        /// Destroys this entity and removes it from the current level.
        /// </summary>
        public virtual void Destroy()
        {
            if (this.Level != null)
            {
                this.Level.Remove(this);
            }
        }
        #endregion

        #region Methods
        /// <summary>
        /// Sends the specified event to the entity.
        /// </summary>
        /// <param name="entityEvent">The event.</param>
        public void Send(Event entityEvent)
        {
            foreach (EntityComponent component in this.Components)
            {
                component.HandleEvent(entityEvent);
            }

            this.HandleEvent(entityEvent);
        }
        /// <summary>
        /// Handls an entity event.
        /// </summary>
        /// <param name="entityEvent">The event.</param>
        public virtual void HandleEvent(Event entityEvent)
        {
        }
        /// <summary>
        /// Simulates the ticks.
        /// </summary>
        /// <param name="count">The count.</param>
        public void SimulateTicks(int count, float elapsed)
        {
            for (int i = 0; i < count; i++)
            {
                this.Tick(elapsed);
            }
        }
        /// <summary>
        /// Handles a game tick.
        /// </summary>
        /// <param name="elapsed">The elapsed.</param>
        public virtual void Tick(float elapsed)
        {
            foreach (EntityComponent component in this.Components)
            {
                component.Tick(elapsed);
            }
        }
        /// <summary>
        /// Handles a game render request.
        /// </summary>
        /// <param name="elapsed">The elapsed.</param>
        public virtual void Render(float elapsed)
        {
            foreach (EntityComponent component in this.Components)
            {
                component.Render(elapsed);
            }
        }
        #endregion
    }
}
