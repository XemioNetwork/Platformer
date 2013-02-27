using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Platformer.Common.Randomization
{
    public interface ISeedable
    {
        /// <summary>
        /// Gets or sets the seed.
        /// </summary>
        int Seed { get; set; }
    }
}
