using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Platformer.Scenes;
using Platformer.Rendering;
using Platformer.Math;
using System.Windows.Forms;
using Platformer.Components;
using Platformer.Game;
using Platformer.Common;
using Platformer.World.TileEngine;
using Platformer.World.Entities;
using Platformer.World;
using Platformer.Input;
using Platformer.Common.Randomization;
using Platformer.World.Entities.Particles;
using Platformer.World.Entities.Components;
using Platformer.World.Entities.Background;
using System.Threading;
using Platformer.UI.Fonts;
using Platformer.Plugins;

namespace Platformer.Scenes
{
    public class TestScene : Scene
    {
        #region Constructors
        public TestScene()
        {

        }
        #endregion

        #region Fields
        private Level _level;

        private SpriteFont _font;
        private SpriteFont _shadowFont;
        #endregion

        #region Methods
        /// <summary>
        /// GAME OVER!!!1
        /// </summary>
        private void GameOver()
        {
            SceneManager sceneManager = ComponentManager.Instance.GetComponent<SceneManager>();
            sceneManager.RemoveCurrent();
            sceneManager.Add(new GameOverScene());
        }
        /// <summary>
        /// Loads the scene content.
        /// </summary>
        public override void LoadContent()
        {
            this._font = SpriteFont.Load("font_main", Properties.Resources.ResourceManager);
            this._shadowFont = SpriteFont.Load("font_shadow", Properties.Resources.ResourceManager);
            
            PluginLoader<Level> levelLoader = new PluginLoader<Level>();
            levelLoader.LoadPlugins("levels");

            this._level = levelLoader.Plugins.FirstOrDefault();
            this._level.Start();
        }
        /// <summary>
        /// Handles the tick event that causes the game to update.
        /// </summary>
        /// <param name="elapsed">The elapsed.</param>
        public override void Tick(float elapsed)
        {            
            this._level.Tick(elapsed);

            if (this._level.Player.Level == null &&
                this._level.Camera.OnTarget)
            {
                this.GameOver();
            }

            base.Tick(elapsed);
        }
        /// <summary>
        /// Handles the render event that causes the game to redraw.
        /// </summary>
        /// <param name="elapsed">The elapsed.</param>
        public override void Render(float elapsed)
        {
            Vector2 screenOffset = new Vector2(280, 210);

            this.RenderManager.Clear(new Color(208, 244, 247));
            this.RenderManager.Offset(screenOffset - this._level.Camera.Offset);

            this._level.Render(elapsed);

            this.RenderManager.Offset(new Vector2(0, 0));

            LifeComponent lifeComponent = this._level.Player.GetComponent<LifeComponent>();
            GoldComponent goldComponent = this._level.Player.GetComponent<GoldComponent>();
            int coinY = Shared.ScreenHeight - 45;

            this.RenderManager.Render(
                Art.Coin, new Rectangle(15, coinY, Art.Coin.Width, Art.Coin.Height));

            this._font.Render(goldComponent.Count.ToString(), new Vector2(55, coinY + 5));

            for (int i = 0; i < lifeComponent.Lives; i += 2)
            {
                ITexture texture = Art.Heart;
                Vector2 inset = Vector2.Zero;

                if (lifeComponent.Health <= i)
                {
                    texture = Art.HeartEmpty;
                }

                if (i == lifeComponent.Health - 1 &&
                    lifeComponent.Health % 2 != 0)
                {
                    texture = Art.HeartHalf;
                    this.RenderManager.Render(Art.HeartEmpty,
                        new Rectangle(
                            (i * 0.5f) * (Art.Heart.Width + 4) + 10,
                            10,
                            Art.Heart.Width,
                            Art.Heart.Height));
                }

                this.RenderManager.Render(texture,
                    new Rectangle(
                        (i * 0.5f) * (Art.Heart.Width + 4) + 10 + inset.X * 0.5f,
                        10 + inset.Y * 0.5f,
                        Art.Heart.Width - inset.X,
                        Art.Heart.Height - inset.Y));
            }
            
            string fps = string.Format(
                "FPS: {0}",
                ComponentManager.Instance.GetComponent<GameLoop>().FramesPerSecond);

            this._shadowFont.Render(fps, new Vector2(11, 61));
            this._font.Render(fps, new Vector2(10, 60));
            
            base.Render(elapsed);
        }
        #endregion
    }
}
