using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Platformer.Plugins
{
    public interface IArtExtender
    {
        /// <summary>
        /// Loads the content.
        /// </summary>
        void LoadContent();
    }
}
