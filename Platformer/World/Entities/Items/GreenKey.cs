using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Platformer.Rendering;

namespace Platformer.World.Entities.Items
{
    public class GreenKey : Item
    {
        #region Properties
        /// <summary>
        /// Gets the texture.
        /// </summary>
        public override ITexture Texture
        {
            get { return Art.KeyGreen; }
        }
        #endregion
    }
}
