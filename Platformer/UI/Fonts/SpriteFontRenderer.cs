﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Platformer.Math;
using Platformer.Rendering;
using Platformer.Components;

namespace Platformer.UI.Fonts
{
    public static class SpriteFontRenderer
    {
        #region Methods
        /// <summary>
        /// Renders the specified text.
        /// </summary>
        /// <param name="font">The font.</param>
        /// <param name="value">The value.</param>
        /// <param name="position">The position.</param>
        public static void Render(SpriteFont font, string value, Vector2 position)
        {
            IRenderManager renderManager = ComponentManager.Instance.GetComponent<IRenderManager>();
            if (renderManager != null)
            {
                string[] lines = value.Split('\n');
                Vector2 currentPosition = position;

                foreach (string line in lines)
                {
                    int maximumHeight = int.MinValue;

                    foreach (char character in line)
                    {
                        int index = (int)character;

                        ITexture texture = font.Textures[index];
                        if (texture == null)
                        {
                            throw new InvalidOperationException(
                                "Cannot render character " + character + " (" + index.ToString() + ").");
                        }

                        renderManager.Render(
                            texture,
                            new Rectangle(
                                currentPosition.X,
                                currentPosition.Y,
                                texture.Width,
                                texture.Height));

                        currentPosition.X += texture.Width + font.Kerning;
                        currentPosition.X += character == ' ' ? font.Spacing : 0;

                        if (texture.Height > maximumHeight)
                        {
                            maximumHeight = texture.Height;
                        }
                    }

                    currentPosition.X = position.X;
                    currentPosition += new Vector2(0, maximumHeight + font.Kerning + 2);
                }
            }
        }
        #endregion
    }
}
