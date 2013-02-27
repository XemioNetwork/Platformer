using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Platformer.UI.Fonts;
using Platformer.Math;

namespace Platformer.World.Entities
{
    public class TextEntity : Entity
    {
        #region Contsructors
        /// <summary>
        /// Initializes a new instance of the <see cref="TextEntity"/> class.
        /// </summary>
        public TextEntity()
        {
            this.ShadowOffset = new Vector2(1, 1);
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="TextEntity"/> class.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="font">The font.</param>
        public TextEntity(string text, SpriteFont font) : this()
        {
            this.Text = text;
            this.Font = font;
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="TextEntity"/> class.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="font">The font.</param>
        /// <param name="shadowFont">The shadow font.</param>
        public TextEntity(string text, SpriteFont font, SpriteFont shadowFont) : this(text, font)
        {
            this.ShadowFont = shadowFont;
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the text.
        /// </summary>
        public string Text { get; set; }
        /// <summary>
        /// Gets or sets the font.
        /// </summary>
        public SpriteFont Font { get; set; }
        /// <summary>
        /// Gets or sets the shadow font.
        /// </summary>
        public SpriteFont ShadowFont { get; set; }
        /// <summary>
        /// Gets or sets the shadow offset.
        /// </summary>
        public Vector2 ShadowOffset { get; set; }
        #endregion

        #region Methods
        /// <summary>
        /// Handles a game render request.
        /// </summary>
        /// <param name="elapsed">The elapsed.</param>
        public override void Render(float elapsed)
        {
            if (this.ShadowFont != null)
            {
                this.ShadowFont.Render(this.Text, this.Position + this.ShadowOffset);
            }
            if (this.Font != null)
            {
                this.Font.Render(this.Text, this.Position);
            }

            base.Render(elapsed);
        }
        #endregion
    }
}
