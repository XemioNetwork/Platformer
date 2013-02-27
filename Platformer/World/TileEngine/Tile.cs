using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Platformer.Rendering;
using Platformer.Common.Linkers;
using Platformer.Math;
using Platformer.Components;
using Platformer.World.Entities;

namespace Platformer.World.TileEngine
{
    public abstract class Tile : ILinkable<int>
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="Tile"/> class.
        /// </summary>
        public Tile()
        {
        }
        #endregion

        #region Static Properties
        /// <summary>
        /// Gets the tile width.
        /// </summary>
        public const int Width = 70;
        /// <summary>
        /// Gets the tile height.
        /// </summary>
        public const int Height = 70;
        #endregion

        #region Properties
        /// <summary>
        /// Gets a value indicating whether this <see cref="Tile"/> is walkable.
        /// </summary>
        public virtual bool Walkable
        {
            get { return true; }
        }
        /// <summary>
        /// Gets the tile friction.
        /// </summary>
        public virtual float Friction
        {
            get { return 0.0f; }
        }
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
        /// <summary>
        /// Gets the tile texture.
        /// </summary>
        public virtual ITexture Texture
        {
            get { return null; }
        }
        #endregion

        #region Methods
        /// <summary>
        /// Called when the tile collides with an entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <param name="reference">The reference.</param>
        public virtual void Collide(Entity entity, TileReference reference)
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
        /// <param name="reference">The reference.</param>
        public virtual void Render(TileReference reference)
        {
        }
        #endregion

        #region ILinkable<int> Member
        /// <summary>
        /// Gets the identifier.
        /// </summary>
        public virtual int Identifier
        {
            get 
            {
                Type type = this.GetType();
                string name = type.FullName;

                return name.GetHashCode();
            }
        }
        #endregion
    }
}
