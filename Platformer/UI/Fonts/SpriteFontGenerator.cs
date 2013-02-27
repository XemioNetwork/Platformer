using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Drawing;
using System.Drawing.Text;
using Platformer.Rendering;
using Platformer.Components;
using System.Drawing.Imaging;

namespace Platformer.UI.Fonts
{
    using Drawing = System.Drawing;

    public static class SpriteFontGenerator
    {
        #region Methods
        /// <summary>
        /// Creates a sprite font for the specified font family.
        /// </summary>
        /// <param name="fontFamily">The font family.</param>
        /// <param name="size">The size.</param>
        /// <param name="color">The color.</param>
        public static SpriteFont Create(string fontFamily, int size)
        {
            return SpriteFontGenerator.Create(fontFamily, size, Color.White);
        }
        /// <summary>
        /// Returns a sprite font for the specified font.
        /// </summary>
        /// <param name="font">The font.</param>
        public static SpriteFont Create(Font font)
        {
            return SpriteFontGenerator.Create(font, Color.White);
        }
        /// <summary>
        /// Creates a sprite font for the specified font family.
        /// </summary>
        /// <param name="fontFamily">The font family.</param>
        /// <param name="size">The size.</param>
        /// <param name="color">The color.</param>
        public static SpriteFont Create(string fontFamily, int size, Color color)
        {
            return SpriteFontGenerator.Create(new Font(fontFamily, size), color);
        }
        /// <summary>
        /// Returns a sprite font for the specified font.
        /// </summary>
        /// <param name="font">The font.</param>
        /// <param name="color">The color.</param>
        public static SpriteFont Create(Font font, Color color)
        {
            Graphics graphics = Graphics.FromHwnd(Shared.Handle);

            Brush brush = new SolidBrush(
                Drawing.Color.FromArgb(color.A, color.R, color.G, color.B));

            Bitmap[] bitmaps = new Bitmap[byte.MaxValue];

            for (int i = 31; i < 253; i++)
            {
                char character = (char)i;
                string current = character.ToString();

                SizeF size = graphics.MeasureString(current, font);

                Bitmap letterMap = new Bitmap(
                    (int)size.Width,
                    (int)size.Height,
                    PixelFormat.Format32bppPArgb);

                using (Graphics letterGraphics = Graphics.FromImage(letterMap))
                {
                    letterGraphics.TextRenderingHint = TextRenderingHint.SingleBitPerPixelGridFit;
                    letterGraphics.DrawString(current, font, brush, PointF.Empty);

                    bitmaps[i] = letterMap;
                }
            }

            return SpriteFontGenerator.Create(bitmaps);
        }
        /// <summary>
        /// Creates a new spritefont.
        /// </summary>
        /// <param name="data">The data.</param>
        public static SpriteFont Create(Bitmap[] data)
        {
            SpriteFont spriteFont = new SpriteFont();
            ITextureFactory factory = ComponentManager.Instance.GetComponent<ITextureFactory>();

            for (int i = 0; i < data.Length; i++)
            {
                if (data[i] == null) continue;

                using (MemoryStream stream = new MemoryStream())
                {
                    if (data[i] != null)
                    {
                        data[i].Save(stream, ImageFormat.Png);
                        stream.Seek(0, SeekOrigin.Begin);

                        spriteFont.Textures[i] = factory.CreateTexture(stream.ToArray());
                        spriteFont.FontUnit.Data[i] = data[i];
                    }
                }
            }

            return spriteFont;
        }
        #endregion
    }
}
