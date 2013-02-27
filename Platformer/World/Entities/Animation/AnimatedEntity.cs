using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Platformer.Rendering;
using Platformer.Math;

namespace Platformer.World.Entities.Animation
{
    public class AnimatedEntity : Entity
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="AnimatedEntity"/> class.
        /// </summary>
        public AnimatedEntity()
        {
            this.Animations = new List<EntityAnimation>();
        }
        #endregion

        #region Fields
        private int _animationIndex;
        #endregion

        #region Properties
        /// <summary>
        /// Gets the texture.
        /// </summary>
        public ITexture Texture
        {
            get { return this.CurrentAnimation.CurrentFrame.Texture; }
        }
        /// <summary>
        /// Gets the current animation.
        /// </summary>
        public EntityAnimation CurrentAnimation
        {
            get { return this.Animations[this._animationIndex]; }
        }
        /// <summary>
        /// Gets the animations.
        /// </summary>
        public List<EntityAnimation> Animations { get; private set; }
        #endregion

        #region Methods
        /// <summary>
        /// Plays the specified animation.
        /// </summary>
        /// <param name="name">The name.</param>
        public void PlayAnimation(string name)
        {
            if (this.CurrentAnimation == null || this.CurrentAnimation.Name != name)
            {
                this._animationIndex = this.Animations.IndexOf(
                    this.Animations.FirstOrDefault(animation => animation.Name == name));

                if (this.CurrentAnimation != null)
                {
                    this.CurrentAnimation.Reset();
                }
            }
        }
        /// <summary>
        /// Plays the default idle animation.
        /// </summary>
        public void Idle()
        {
            this.StopAnimation();
            this.PlayAnimation("Idle");
        }
        /// <summary>
        /// Stops the current animation.
        /// </summary>
        public void StopAnimation()
        {
            this._animationIndex = 0;
        }
        /// <summary>
        /// Handles a game tick.
        /// </summary>
        /// <param name="elapsed">The elapsed.</param>
        public override void Tick(float elapsed)
        {
            if (this.CurrentAnimation != null)
            {
                this.CurrentAnimation.Tick(elapsed);
            }

            base.Tick(elapsed);
        }
        /// <summary>
        /// Handles a game render request.
        /// </summary>
        /// <param name="elapsed">The elapsed.</param>
        public override void Render(float elapsed)
        {
            if (this.CurrentAnimation != null &&
                this.CurrentAnimation.CurrentFrame != null)
            {
                this.RenderManager.Render(
                    this.Texture,
                    this.Position + this.Offset + this.CurrentAnimation.CurrentFrame.Offset);
            }

            base.Render(elapsed);
        }
        #endregion
    }
}
