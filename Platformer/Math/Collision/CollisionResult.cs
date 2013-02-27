using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Platformer.Math.Collision
{
    public struct CollisionResult
    {
        #region Properties
        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="CollisionResult"/> is intersecting.
        /// </summary>
        public bool Intersecting { get; set; }
        /// <summary>
        /// Gets or sets the minimum translation.
        /// </summary>
        public Vector2 MinimumTranslation { get; set; }
        /// <summary>
        /// Gets or sets the direction.
        /// </summary>
        public Direction Direction { get; set; }
        #endregion
    }
}
