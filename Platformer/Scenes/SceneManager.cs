using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Platformer.Game;
using Platformer.Components;
using Platformer.Plugins;
using Platformer.Rendering;

namespace Platformer.Scenes
{
    [Plugin]
    public class SceneManager : ISceneManager, IConstructable
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="SceneManager"/> class.
        /// </summary>
        public SceneManager()
        {
            this._scenes = new List<Scene>();
        }
        #endregion

        #region Fields
        private List<Scene> _scenes;
        #endregion

        #region Properties
        /// <summary>
        /// Gets the render manager.
        /// </summary>
        public IRenderManager RenderManager
        {
            get { return ComponentManager.Instance.GetComponent<IRenderManager>(); }
        }
        #endregion

        #region Methods
        /// <summary>
        /// Adds the specified scene.
        /// </summary>
        /// <param name="scene">The scene.</param>
        public void Add(Scene scene)
        {
            scene.SceneManager = this;
            scene.LoadContent();

            this._scenes.Add(scene);
        }
        /// <summary>
        /// Removes the specified scene.
        /// </summary>
        /// <param name="scene">The scene.</param>
        public void Remove(Scene scene)
        {
            this._scenes.Remove(scene);
        }
        /// <summary>
        /// Removes the current.
        /// </summary>
        public void RemoveCurrent()
        {
            this._scenes.Remove(this._scenes.FirstOrDefault());
        }
        #endregion

        #region IGameLoopHandler Member
        /// <summary>
        /// Handles the tick event that causes the game to update.
        /// </summary>
        /// <param name="elapsed">The elapsed.</param>
        public void Tick(float elapsed)
        {
            for (int i = this._scenes.Count - 1; i >= 0; i--)
            {
                Scene scene = this._scenes[i];
                if (scene.Active)
                {
                    scene.Tick(elapsed);
                }
            }
        }
        /// <summary>
        /// Handles the render event that causes the game to redraw.
        /// </summary>
        /// <param name="elapsed">The elapsed.</param>
        public void Render(float elapsed)
        {
            for (int i = this._scenes.Count - 1; i >= 0; i--)
            {
                Scene scene = this._scenes[i];
                if (scene.Visible)
                {
                    scene.Render(elapsed);
                }
            }

            if (this.RenderManager != null)
            {
                this.RenderManager.Present();
            }
        }
        #endregion

        #region IConstructable Member
        /// <summary>
        /// Constructs this instance.
        /// </summary>
        public void Construct()
        {
            GameLoop loop = ComponentManager.Instance.GetComponent<GameLoop>();
            loop.Subscribe(this);
        }
        #endregion
    }
}
