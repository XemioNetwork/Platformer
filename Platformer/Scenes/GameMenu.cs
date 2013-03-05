using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using Platformer.World;
using Platformer.World.Entities.Mobs;
using Platformer.Math;
using Platformer.World.Entities.Puzzle;
using Platformer.World.Entities.Items;
using Platformer.World.Entities;
using Platformer.World.Tiles;
using Platformer.Common.Randomization;
using Platformer.Scenes.Menu;

namespace Platformer.Scenes
{
    using MenuItem = Platformer.Scenes.Menu.MenuItem;
    using Platformer.Input;
    using Platformer.Components;
    using Platformer.Plugins;

    public class GameMenu : Scene
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="GameMenu"/> class.
        /// </summary>
        public GameMenu()
        {
            this._items = new List<MenuItem>();
            this._levels = new List<Level>();
        }
        #endregion

        #region Fields
        private Level _level;
        private KeyboardState _lastState;

        private List<MenuItem> _items;

        private int _selectedIndex = 0;
        private int _scrollIndex = 0;

        private int _tickCount = 0;

        private List<Level> _levels;
        #endregion

        #region Methods
        /// <summary>
        /// Loads the scene content.
        /// </summary>
        public override void LoadContent()
        {
            this._level = new MenuLevel();
            this._level.Start(false);

            this._level.Camera.Position = new Vector2(30, 200);
            
            PluginLoader<Level> loader = new PluginLoader<Level>();
            loader.LoadPlugins(@".\levels\");

            foreach (Level level in loader.Plugins)
            {
                MenuItem item = new MenuItem();
                item.Text = level.Name;

                item.OnTrigger = () =>
                {
                    PlatformerGame game = new PlatformerGame(level);

                    this.SceneManager.Add(game);
                    this.SceneManager.Remove(this);
                };

                this._items.Add(item);
                this._levels.Add(level);
            }
        }
        /// <summary>
        /// Handles the tick event that causes the game to update.
        /// </summary>
        /// <param name="elapsed">The elapsed.</param>
        public override void Tick(float elapsed)
        {
            KeyListener keyboard = ComponentManager.Instance.GetComponent<KeyListener>();
            this._level.Tick(elapsed);

            Entity slime = this._level.Entities.LastOrDefault(
                entity => entity is Slime && entity.Position.Y < 1000);

            this._level.Camera.Speed = 0.02f;
            this._level.Camera.Focus(slime);

            if (this._level != this._levels[this._selectedIndex])
            {
                this._level = this._levels[this._selectedIndex];
                this._level.Start(false);

                this._level.Camera.Position = this._level.PreviewLocation;
            }

            if (keyboard.IsKeyDown(Keys.Down) && this._lastState.IsKeyUp(Keys.Down))
            {
                this._selectedIndex++;
                if (this._selectedIndex >= this._items.Count)
                {
                    this._selectedIndex = 0;
                }
            }
            if (keyboard.IsKeyDown(Keys.Up) && this._lastState.IsKeyUp(Keys.Up))
            {
                this._selectedIndex--;
                if (this._selectedIndex < 0)
                {
                    this._selectedIndex = this._items.Count - 1;
                }
            }

            for (int i = 0; i < this._items.Count; i++)
            {
                if (i == this._selectedIndex && keyboard.IsKeyDown(Keys.Enter))
                {
                    this._items[i].Trigger();
                }
            }

            this._lastState = keyboard.State;
        }
        /// <summary>
        /// Handles the render event that causes the game to redraw.
        /// </summary>
        /// <param name="elapsed">The elapsed.</param>
        public override void Render(float elapsed)
        {
            this.RenderManager.Clear(new Color(208, 244, 247));
            this._level.Render(elapsed);

            this.RenderManager.Offset(Vector2.Zero);
            this.RenderManager.Render(Art.Clear, Vector2.Zero);
            this.RenderManager.Render(Art.Clear, Vector2.Zero);

            this.RenderManager.Render(Art.Crate, new Vector2(520, 330));
            this.RenderManager.Render(Art.Hero, new Vector2(520, 240));

            Art.FontGameOver.Render("Platformer", new Vector2(25, 60));
            KeyListener keyboard = ComponentManager.Instance.GetComponent<KeyListener>();

            for (int i = this._scrollIndex; i < 8 && i < this._items.Count; i++)
            {
                string prefix = string.Empty;
                bool isSelected = (i == this._selectedIndex);

                string message = this._items[i].Text;
                Vector2 position = new Vector2(25, 200 + i * 30);

                if (isSelected) prefix = "> ";
                if (!isSelected) position += new Vector2(25, 0);

                Art.FontWhite.Render(prefix + message, position + new Vector2(0, 1));
                for (int j = 0; j < (isSelected ? 2 : 1); j++)
                {
                    Art.FontBlack.Render(prefix + message, position);
                }
            }

            Art.FontWhite.Render("© 2013 Chris Köbke, kenney.nl", new Vector2(175, 481));
            Art.FontBlack.Render("© 2013 Chris Köbke, kenney.nl", new Vector2(175, 480));
        }
        #endregion
    }
}
