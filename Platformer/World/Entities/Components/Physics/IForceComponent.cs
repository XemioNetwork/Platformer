using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Platformer.Math;

namespace Platformer.World.Entities.Components.Physics
{
    public interface IForceComponent
    {
        /// <summary>
        /// Gets the force.
        /// </summary>
        Vector2 Force { get; }
    }
}
