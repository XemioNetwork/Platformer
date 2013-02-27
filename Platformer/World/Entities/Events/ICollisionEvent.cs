using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Platformer.Math.Collision;

namespace Platformer.World.Entities.Events
{
    public interface ICollisionEvent
    {
        /// <summary>
        /// Gets the result.
        /// </summary>
        CollisionResult Result { get; }
    }
}
