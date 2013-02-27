using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Resources;
using System.Drawing;
using System.Drawing.Text;
using System.Drawing.Imaging;
using Platformer.Rendering;
using Platformer.Math;
using Platformer.Components;

namespace Platformer.UI.Fonts
{
    using Rectangle = Platformer.Math.Rectangle;
    using Drawing = System.Drawing;

    public class SpriteFont
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="SpriteFont"/> class.
        /// </summary>
        public SpriteFont() : this(0)
        {
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="SpriteFont"/> class.
        /// </summary>
        /// <param name="kerning">The kerning.</param>
        public SpriteFont(int kerning)
        {
            this.Kerning = kerning;
            this.Spacing = 15;

            this.Textures = new ITexture[byte.MaxValue];
            this.FontUnit = new InternalFontUnit();
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the kerning.
        /// </summary>
        public int Kerning { get; set; }
        /// <summary>
        /// Gets or sets the width of the space character.
        /// </summary>
        public int Spacing { get; set; }
        /// <summary>
        /// Gets the textures.
        /// </summary>
        public ITexture[] Textures { get; private set; }
        /// <summary>
        /// Gets the font unit.
        /// </summary>
        internal InternalFontUnit FontUnit { get; set; }
        #endregion

        #region Static Methods
        /// <summary>
        /// Creates a sprite font from a specified file.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <returns></returns>
        public static SpriteFont Load(string fileName)
        {
            using (FileStream stream = File.OpenRead(fileName))
            {
                return SpriteFont.Load(stream);
            }
        }
        /// <summary>
        /// Creates a sprite font from the specified stream.
        /// </summary>
        /// <param name="stream">The stream.</param>
        public static SpriteFont Load(Stream stream)
        {
            using (BinaryReader reader = new BinaryReader(stream))
            {
                int kerning = reader.ReadInt32();
                int spacing = reader.ReadInt32();

                InternalFontUnit fontUnit = new InternalFontUnit();
                fontUnit.Deserialize(stream);

                SpriteFont spriteFont = SpriteFontGenerator.Create(fontUnit.Data);
                spriteFont.Kerning = kerning;
                spriteFont.Spacing = spacing;
                spriteFont.FontUnit = fontUnit;

                return spriteFont;
            }
        }
        /// <summary>
        /// Creates a sprite font from a specified file.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <returns></returns>
        public static SpriteFont Load(string name, ResourceManager resourceManager)
        {
            byte[] resourceData = (byte[])resourceManager.GetObject(name);
            using (MemoryStream stream = new MemoryStream(resourceData))
            {
                return SpriteFont.Load(stream);
            }
        }
        #endregion

        #region Methods
        /// <summary>
        /// Measures the specified string.
        /// </summary>
        /// <param name="value">The value.</param>
        public Vector2 MeasureString(string value)
        {
            return this.MeasureString(value.Split('\n'));
        }
        /// <summary>
        /// Measures the specified string.
        /// </summary>
        /// <param name="lines">The lines.</param>
        public Vector2 MeasureString(string[] lines)
        {
            Vector2 result = Vector2.Zero;

            foreach (string value in lines)
            {
                float x = 0;
                float y = int.MinValue;

                foreach (char character in value)
                {
                    x += this.Textures[(int)character].Width;
                    x += this.Kerning;

                    if (character == ' ')
                    {
                        x += this.Spacing;
                    }

                    if (y < this.Textures[(int)character].Height)
                    {
                        y = this.Textures[(int)character].Height;
                    }
                }

                result += new Vector2(x, y + this.Kerning);
            }

            return result;
        }
        /// <summary>
        /// Renders the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        public void Render(string value, Vector2 position)
        {
            SpriteFontRenderer.Render(this, value, position);
        }
        #endregion

        #region Serialization Methods
        /// <summary>
        /// Saves the sprite font to specified stream.
        /// </summary>
        /// <param name="stream">The stream.</param>
        public void Save(Stream stream)
        {
            using (BinaryWriter writer = new BinaryWriter(stream))
            {
                writer.Write(this.Kerning);
                writer.Write(this.Spacing);

                this.FontUnit.Serialize(stream);
            }
        }
        #endregion
    }
}
