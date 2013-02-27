using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Platformer.Rendering;

namespace Platformer.World.Entities.Animation
{
    public class EntityAnimation
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="EntityAnimation"/> class.
        /// </summary>
        public EntityAnimation()
        {
            this.Name = string.Empty;
            this.Frames = new List<EntityFrame>();
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="EntityAnimation"/> class.
        /// </summary>
        /// <param name="frameTime">The frame time.</param>
        /// <param name="frames">The frames.</param>
        public EntityAnimation(string name, float frameTime, EntityFrame frame)
            : this(name, frameTime, new EntityFrame[] { frame })
        {
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="EntityAnimation"/> class.
        /// </summary>
        /// <param name="frameTime">The frame time.</param>
        /// <param name="frames">The frames.</param>
        public EntityAnimation(string name, float frameTime, IEnumerable<EntityFrame> frames)
            : this()
        {
            this.Name = name;
            this.FrameTime = frameTime;
            this.Frames.AddRange(frames);
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="EntityAnimation"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="frameTime">The frame time.</param>
        /// <param name="texture">The texture.</param>
        public EntityAnimation(string name, float frameTime, ITexture texture) 
            : this(name, frameTime, new EntityFrame(texture))
        {
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="EntityAnimation"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="frameTime">The frame time.</param>
        /// <param name="textures">The textures.</param>
        public EntityAnimation(string name, float frameTime, IEnumerable<ITexture> textures)
            : this()
        {
            this.Name = name;
            this.FrameTime = frameTime;

            List<EntityFrame> frames = new List<EntityFrame>();
            foreach (ITexture texture in textures)
            {
                frames.Add(new EntityFrame(texture));
            }

            this.Frames.AddRange(frames);
        }
        #endregion

        #region Fields
        private int _currentIndex;
        private float _elapsed;
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Gets or sets the frames.
        /// </summary>
        public List<EntityFrame> Frames { get; private set; }
        /// <summary>
        /// Gets or sets the frame time.
        /// </summary>
        public float FrameTime { get; set; }
        /// <summary>
        /// Gets the current frame.
        /// </summary>
        public EntityFrame CurrentFrame
        {
            get { return this.Frames[this._currentIndex]; }
        }
        #endregion

        #region Methods
        /// <summary>
        /// Resets this animation.
        /// </summary>
        public void Reset()
        {
            this._currentIndex = 0;
            this._elapsed = 0;
        }
        /// <summary>
        /// Handles a game tick.
        /// </summary>
        /// <param name="elapsed">The elapsed.</param>
        public void Tick(float elapsed)
        {
            this._elapsed += elapsed;
            while (this._elapsed >= this.FrameTime && this.FrameTime > 0)
            {
                this._elapsed -= this.FrameTime;
                this._currentIndex++;

                if (this._currentIndex >= this.Frames.Count)
                {
                    this._currentIndex = 0;
                }
            }
        }
        #endregion
    }
}
