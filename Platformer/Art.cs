using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Drawing;
using Platformer.Rendering;
using Platformer.World.Entities.Animation;
using Platformer.UI.Fonts;
using Platformer.Properties;
using System.Drawing.Imaging;

namespace Platformer
{
    public static class Art
    {
        #region Factory
        /// <summary>
        /// Gets the factory.
        /// </summary>
        public static ITextureFactory Factory { get; private set; }
        #endregion

        #region Blocks
        /// <summary>
        /// Gets the texture for "block.png".
        /// </summary>
        public static ITexture Block { get; private set; }
        /// <summary>
        /// Gets the texture for "bonus.png".
        /// </summary>
        public static ITexture Bonus { get; private set; }
        /// <summary>
        /// Gets the texture for "ground.png".
        /// </summary>
        public static ITexture Ground { get; private set; }
        /// <summary>
        /// Gets the texture for "ground_dirt.png".
        /// </summary>
        public static ITexture GroundDirt { get; private set; }
        /// <summary>
        /// Gets the texture for "ground_rock.png".
        /// </summary>
        public static ITexture GroundRock { get; private set; }
        /// <summary>
        /// Gets the texture for "lock_blue.png".
        /// </summary>
        public static ITexture LockBlue { get; private set; }
        /// <summary>
        /// Gets the texture for "lock_red.png".
        /// </summary>
        public static ITexture LockRed { get; private set; }
        /// <summary>
        /// Gets the texture for "lock_green.png".
        /// </summary>
        public static ITexture LockGreen { get; private set; }
        /// <summary>
        /// Gets the texture for "lock_yellow.png".
        /// </summary>
        public static ITexture LockYellow { get; private set; }
        /// <summary>
        /// Gets the texture for "fence.png".
        /// </summary>
        public static ITexture Fence { get; private set; }
        /// <summary>
        /// Gets the texture for "crate.png".
        /// </summary>
        public static ITexture Crate { get; private set; }
        #endregion

        #region Entites
        /// <summary>
        /// Gets the texture for "plank.png".
        /// </summary>
        public static ITexture Plank { get; private set; }
        /// <summary>
        /// Gets the animation for "slime_normal.png".
        /// </summary>
        public static ITexture[] Slime { get; private set; }
        #endregion

        #region Characters
        /// <summary>
        /// Gets the texture for "front.png".
        /// </summary>
        public static ITexture Hero { get; private set; }
        /// <summary>
        /// Gets the texture set for the hero walking right.
        /// </summary>
        public static ITexture[] HeroRight { get; private set; }
        /// <summary>
        /// Gets the texture set for the hero walking left.
        /// </summary>
        public static ITexture[] HeroLeft { get; private set; }
        /// <summary>
        /// Gets the texture for "jump_left.png".
        /// </summary>
        public static ITexture HeroJumpLeft { get; private set; }
        /// <summary>
        /// Gets the texture for "jump_right.png".
        /// </summary>
        public static ITexture HeroJumpRight { get; private set; }
        #endregion

        #region Spawners
        /// <summary>
        /// Gets the texture for "slime_spawner.png".
        /// </summary>
        public static ITexture SlimeSpawner { get; private set; }
        #endregion

        #region Particles
        /// <summary>
        /// Gets the texture for "particle_1.png".
        /// </summary>
        public static ITexture ParticleWhite { get; private set; }
        /// <summary>
        /// Gets the texture for "particle_2.png".
        /// </summary>
        public static ITexture ParticleLightGray { get; private set; }
        /// <summary>
        /// Gets the texture for "particle_3.png".
        /// </summary>
        public static ITexture ParticleDarkGray { get; private set; }
        /// <summary>
        /// Gets the texture for "particle_4.png".
        /// </summary>
        public static ITexture ParticleGray { get; private set; }
        /// <summary>
        /// Gets the texture collection for all clouds.
        /// </summary>
        public static ITexture[] Clouds { get; private set; }
        #endregion

        #region Keys
        /// <summary>
        /// Gets the texture for "key_blue.png".
        /// </summary>
        public static ITexture KeyBlue { get; private set; }
        /// <summary>
        /// Gets the texture for "key_green.png".
        /// </summary>
        public static ITexture KeyGreen { get; private set; }
        /// <summary>
        /// Gets the texture for "key_red.png".
        /// </summary>
        public static ITexture KeyRed { get; private set; }
        /// <summary>
        /// Gets the texture for "key_yellow.png".
        /// </summary>
        public static ITexture KeyYellow { get; private set; } 
        #endregion

        #region Coins
        /// <summary>
        /// Gets the texture for "coin_gold.png".
        /// </summary>
        public static ITexture Coin { get; private set; }
        #endregion

        #region User Interface
        /// <summary>
        /// Gets the texture for "heart.png".
        /// </summary>
        public static ITexture Heart { get; private set; }
        /// <summary>
        /// Gets the texture for "heart_half.png".
        /// </summary>
        public static ITexture HeartHalf { get; private set; }
        /// <summary>
        /// Gets the texture for "heart_empty.png".
        /// </summary>
        public static ITexture HeartEmpty { get; private set; }
        #endregion

        #region Misc
        /// <summary>
        /// Gets the texture for "red.png".
        /// </summary>
        public static ITexture Rectangle { get; private set; }
        #endregion

        #region Fonts
        /// <summary>
        /// Gets the font for "font_main.font".
        /// </summary>
        public static SpriteFont FontBlack { get; private set; }
        /// <summary>
        /// Gets the font for "font_shadow.font".
        /// </summary>
        public static SpriteFont FontWhite { get; private set; }
        /// <summary>
        /// Gets the font for "font_gameover.font".
        /// </summary>
        public static SpriteFont FontGameOver { get; private set; }
        #endregion

        #region Methods
        /// <summary>
        /// Loads the texture.
        /// </summary>
        /// <param name="factory">The factory.</param>
        /// <param name="name">The name.</param>
        private static ITexture LoadTexture(string name)
        {
            return Art.LoadTexture(Art.Factory, name);
        }
        /// <summary>
        /// Loads the texture.
        /// </summary>
        /// <param name="factory">The factory.</param>
        /// <param name="name">The name.</param>
        private static ITexture LoadTexture(ITextureFactory factory, string name)
        {
            return factory.CreateTexture(name, Resources.ResourceManager);
        }
        /// <summary>
        /// Loads a set of textures.
        /// </summary>
        /// <param name="factory">The factory.</param>
        /// <param name="name">The name.</param>
        /// <param name="textureCount">The texture count.</param>
        private static ITexture[] LoadList(string name, int textureCount)
        {
            return Art.LoadList(Art.Factory, name, textureCount);
        }
        /// <summary>
        /// Loads a set of textures.
        /// </summary>
        /// <param name="factory">The factory.</param>
        /// <param name="name">The name.</param>
        /// <param name="textureCount">The texture count.</param>
        private static ITexture[] LoadList(ITextureFactory factory, string name, int textureCount)
        {
            ITexture[] textures = new ITexture[textureCount];
            for (int i = 0; i < textureCount; i++)
            {
                textures[i] = Art.LoadTexture(factory, name + "_" + (i + 1).ToString());
            }

            return textures;
        }
        /// <summary>
        /// Loads a spritesheet.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="rows">The rows.</param>
        /// <param name="columns">The columns.</param>
        /// <param name="textureCount">The texture count.</param>
        private static ITexture[] LoadSpritesheet(string name, int rows, int columns, int textureCount)
        {
            Bitmap bitmap = Resources.ResourceManager.GetObject(name) as Bitmap;
            ITexture[] textures = new ITexture[rows * columns];

            int frameWidth = bitmap.Width / columns;
            int frameHeight = bitmap.Height / rows;

            for (int y = 0; y < rows; y++)
            {
                for (int x = 0; x < columns; x++)
                {
                    Bitmap currentFrame = new Bitmap(frameWidth, frameHeight);
                    using (Graphics graphics = Graphics.FromImage(currentFrame))
                    {
                        graphics.DrawImage(bitmap, new Point(-x * frameWidth, -y * frameHeight));
                        using (MemoryStream stream = new MemoryStream())
                        {
                            currentFrame.Save(stream, ImageFormat.Png);
                            stream.Seek(0, SeekOrigin.Begin);

                            textures[y * columns + x] = Art.Factory.CreateTexture(stream.ToArray());
                        }
                    }

                }
            }

            return textures.Take(textureCount).ToArray();
        }
        /// <summary>
        /// Loads a font.
        /// </summary>
        /// <param name="name">The name.</param>
        private static SpriteFont LoadFont(string name)
        {
            return SpriteFont.Load(name, Resources.ResourceManager);
        }
        /// <summary>
        /// Loads the content.
        /// </summary>
        /// <param name="factory">The factory.</param>
        public static void LoadContent(ITextureFactory factory)
        {
            Art.Factory = factory;

            Art.Block = LoadTexture("block");
            Art.Bonus = LoadTexture("bonus");
            Art.Ground = LoadTexture("ground");
            Art.GroundDirt = LoadTexture("ground_dirt");
            Art.GroundRock = LoadTexture("ground_rock");
            Art.LockBlue = LoadTexture("lock_blue");
            Art.LockGreen = LoadTexture("lock_green");
            Art.LockRed = LoadTexture("lock_red");
            Art.LockYellow = LoadTexture("lock_yellow");
            Art.Fence = LoadTexture("fence");
            Art.Crate = LoadTexture("crate");

            Art.Plank = LoadTexture("plank");
            Art.Slime = LoadList("slime", 2);

            Art.Hero = LoadTexture("front");
            Art.HeroRight = LoadSpritesheet("walk_right", 1, 11, 11);
            Art.HeroLeft = LoadSpritesheet("walk_left", 1, 11, 11);

            Art.HeroJumpLeft = LoadTexture("jump_left");
            Art.HeroJumpRight = LoadTexture("jump_right");

            Art.Clouds = LoadList("cloud", 3);

            Art.SlimeSpawner = LoadTexture("slime_spawner");

            Art.ParticleWhite = LoadTexture("particle_1");
            Art.ParticleLightGray = LoadTexture("particle_2");
            Art.ParticleDarkGray = LoadTexture("particle_3");
            Art.ParticleGray = LoadTexture("particle_4");

            Art.KeyBlue = LoadTexture("key_blue");
            Art.KeyGreen = LoadTexture("key_green");
            Art.KeyRed = LoadTexture("key_red");
            Art.KeyYellow = LoadTexture("key_yellow");

            Art.Coin = LoadTexture("coin_gold");

            Art.Heart = LoadTexture("heart");
            Art.HeartHalf = LoadTexture("heart_half");
            Art.HeartEmpty = LoadTexture("heart_empty");

            Art.Rectangle = LoadTexture("red");

            Art.FontBlack = LoadFont("font_main");
            Art.FontWhite = LoadFont("font_shadow");
            Art.FontGameOver = LoadFont("font_gameover");
        }
        #endregion
    }
}
