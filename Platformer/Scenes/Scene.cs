using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Platformer.Game;
using Platformer.Rendering;
using Platformer.Components;

namespace Platformer.Scenes
{
    public abstract class Scene : IGameHandler
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="Scene"/> class.
        /// </summary>
        public Scene()
        {
            this.Active = true;
            this.Visible = true;
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="Scene"/> is active.
        /// </summary>
        public bool Active { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="Scene"/> receives a render call.
        /// </summary>
        public bool Visible { get; set; }
        /// <summary>
        /// Gets the graphics provider.
        /// </summary>
        public IGraphicsProvider GraphicsProvider
        {
            get { return ComponentManager.Instance.GetComponent<IGraphicsProvider>(); }
        }
        /// <summary>
        /// Gets the render manager.
        /// </summary>
        public IRenderManager RenderManager
        {
            get { return ComponentManager.Instance.GetComponent<IRenderManager>(); }
        }
        /// <summary>
        /// Gets the texture factory.
        /// </summary>
        public ITextureFactory TextureFactory
        {
            get { return ComponentManager.Instance.GetComponent<ITextureFactory>(); }
        }
        /// <summary>
        /// Gets the scene manager.
        /// </summary>
        public ISceneManager SceneManager { get; internal set; }
        #endregion

        #region Methods
        /// <summary>
        /// Loads the scene content.
        /// </summary>
        public virtual void LoadContent()
        {
        }
        #endregion

        #region IGameLoopHandler Member
        /// <summary>
        /// Handles the tick event that causes the game to update.
        /// </summary>
        /// <param name="elapsed">The elapsed.</param>
        public virtual void Tick(float elapsed)
        {
        }
        /// <summary>
        /// Handles the render event that causes the game to redraw.
        /// </summary>
        /// <param name="elapsed">The elapsed.</param>
        public virtual void Render(float elapsed)
        {
        }
        #endregion
    }
}
