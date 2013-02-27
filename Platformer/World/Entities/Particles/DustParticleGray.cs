using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Platformer.Rendering;

namespace Platformer.World.Entities.Particles
{
    public class DustParticleGray : Particle
    {
        #region Particle Member
        /// <summary>
        /// Gets or sets the texture.
        /// </summary>
        public override ITexture Texture
        {
            get { return Art.ParticleLightGray; }
        }
        #endregion
    }
}
