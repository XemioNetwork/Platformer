using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Platformer.World.TileEngine;
using Platformer.World.Entities;
using Platformer.Rendering;
using Platformer.Components;
using Platformer.Math.Collision;
using Platformer.World.Entities.Components;
using Platformer.Math;
using Platformer.World.Entities.Events;

namespace Platformer.World
{
    public abstract class Level
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="Level"/> class.
        /// </summary>
        public Level(int width, int height)
        {
            this.Collision = new LevelCollision(this);

            this.Width = width;
            this.Height = height;

            this.ClearLevel();
        }
        #endregion
        
        #region Properties
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public abstract string Name { get; }
        /// <summary>
        /// Gets the spawn location.
        /// </summary>
        public virtual Vector2 SpawnLocation
        {
            get { return Vector2.Zero; }
        }
        /// <summary>
        /// Gets the preview location.
        /// </summary>
        public virtual Vector2 PreviewLocation
        {
            get { return Vector2.Zero; }
        }
        /// <summary>
        /// Gets the collision.
        /// </summary>
        public LevelCollision Collision { get; private set; }
        /// <summary>
        /// Gets the camera.
        /// </summary>
        public Camera Camera { get; private set; }
        /// <summary>
        /// Gets the player.
        /// </summary>
        public Player Player { get; private set; }
        /// <summary>
        /// Gets the tile layers.
        /// </summary>
        public TileMap[] TileLayer { get; private set; }
        /// <summary>
        /// Gets the entities.
        /// </summary>
        public List<Entity> Entities { get; private set; }
        /// <summary>
        /// Gets the width.
        /// </summary>
        public int Width { get; private set; }
        /// <summary>
        /// Gets the height.
        /// </summary>
        public int Height { get; private set; }
        #endregion

        #region Methods
        /// <summary>
        /// Adds the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public void Add(Entity entity)
        {
            this.Entities.Add(entity);
            entity.Level = this;
        }
        /// <summary>
        /// Removes the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public void Remove(Entity entity)
        {
            this.Entities.Remove(entity);
            entity.Level = null;
        }
        /// <summary>
        /// Clears the level.
        /// </summary>
        private void ClearLevel()
        {
            this.TileLayer = new TileMap[4];
            for (int i = 0; i < this.TileLayer.Length; i++)
            {
                this.TileLayer[i] = new TileMap(this.Width, this.Height);
            }

            this.Entities = new List<Entity>();
        }
        /// <summary>
        /// Resets the level.
        /// </summary>
        public void ResetLevel()
        {
            this.ClearLevel();
            this.BuildLevel();
        }
        /// <summary>
        /// Builds the level.
        /// </summary>
        protected virtual void BuildLevel()
        {
        }
        /// <summary>
        /// Starts this level.
        /// </summary>
        public void Start()
        {
            this.Start(true);
        }
        /// <summary>
        /// Starts this level.
        /// </summary>
        /// <param name="createPlayer">if set to <c>true</c> create player.</param>
        public void Start(bool createPlayer)
        {
            this.ResetLevel();
            this.Camera = new Camera();

            if (createPlayer)
            {
                this.Player = new Player();

                this.Camera.Focus(this.Player);
                this.Player.Position = this.SpawnLocation;

                this.Add(this.Player);
            }
        }
        /// <summary>
        /// Handles a game tick.
        /// </summary>
        /// <param name="elapsed">The elapsed.</param>
        public void Tick(float elapsed)
        {
            for (int i = this.Entities.Count - 1; i >= 0; i--)
            {
                this.Entities[i].Tick(elapsed);
            }

            this.Collision.Tick(elapsed);
            this.Camera.Tick(elapsed);
        }
        /// <summary>
        /// Handles a game render request.
        /// </summary>
        /// <param name="elapsed">The elapsed.</param>
        public void Render(float elapsed)
        {
            IEnumerator<Entity> renderQueue = this.Entities
                .OrderBy(entity => entity.LayerIndex)
                .GetEnumerator();

            IRenderManager renderManager = ComponentManager.Instance.GetComponent<IRenderManager>();
            Vector2 center = new Vector2(Shared.ScreenWidth * 0.5f, Shared.ScreenHeight * 0.5f);

            renderManager.Offset(-this.Camera.Offset + center);
            
            for (int i = 0; i < this.TileLayer.Length; i++)
            {
                while (renderQueue.Current == null || renderQueue.Current.LayerIndex == i)
                {
                    if (!renderQueue.MoveNext()) 
                        break;

                    if (renderQueue.Current.Visible)
                    {
                        renderQueue.Current.Render(elapsed);
                    }
                }

                this.TileLayer[i].Render(elapsed);
            }
        }
        #endregion
    }
}
