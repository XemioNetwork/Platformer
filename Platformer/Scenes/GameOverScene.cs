using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Platformer.UI.Fonts;
using Platformer.Math;
using Platformer.Input;
using Platformer.Components;
using System.Windows.Forms;

namespace Platformer.Scenes
{
    public class GameOverScene : Scene
    {
        #region Fields
        private int _gameOverIndex;
        private int _pressEnterIndex;

        private float _elapsed;
        #endregion

        #region Properties
        /// <summary>
        /// Gets the last state.
        /// </summary>
        public KeyboardState LastState { get; private set; }
        #endregion

        #region Methods
        /// <summary>
        /// Handles the tick event that causes the game to update.
        /// </summary>
        /// <param name="elapsed">The elapsed.</param>
        public override void Tick(float elapsed)
        {
            KeyListener listener = ComponentManager.Instance.GetComponent<KeyListener>();
            if (listener.IsKeyDown(Keys.Enter) && this.LastState.IsKeyUp(Keys.Enter))
            {
                this.SceneManager.Add(new TestScene());
                this.SceneManager.Remove(this);
            }

            this._elapsed += elapsed;

            while (this._elapsed > 20)
            {
                if (this._gameOverIndex < "GAME OVER".Length)
                    this._gameOverIndex++;
                if (this._pressEnterIndex < "Press Enter to respawn".Length)
                    this._pressEnterIndex += 2;

                this._elapsed -= 20;
            }

            this.LastState = listener.State;
            base.Tick(elapsed);
        }
        /// <summary>
        /// Handles the render event that causes the game to redraw.
        /// </summary>
        /// <param name="elapsed">The elapsed.</param>
        public override void Render(float elapsed)
        {
            this.RenderManager.Clear(new Color(208, 244, 247));

            string gameOver = "GAME OVER";
            string pressEnter = "Press Enter to respawn";

            Vector2 fontSize = Art.FontGameOver.MeasureString(gameOver);
            Vector2 fontSizeB = Art.FontBlack.MeasureString(pressEnter);

            Vector2 gameOverPosition = new Vector2(
                Shared.ScreenWidth * 0.5f - fontSize.X * 0.5f - 20,
                Shared.ScreenHeight * 0.5f - fontSize.Y * 0.5f - 45 - this._gameOverIndex);

            Art.FontGameOver.Render(new string(gameOver.Take(this._gameOverIndex).ToArray()), gameOverPosition);
            Art.FontBlack.Render(new string(pressEnter.Take(this._pressEnterIndex).ToArray()),
                gameOverPosition + new Vector2(fontSizeB.X * 0.5f, fontSize.Y + this._pressEnterIndex));

            base.Render(elapsed);
        }
        #endregion
    }
}
