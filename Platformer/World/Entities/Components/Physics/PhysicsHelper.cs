using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Platformer.Math;

namespace Platformer.World.Entities.Components.Physics
{
    public static class PhysicsHelper
    {
        #region Methods
        /// <summary>
        /// Measures the total force.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public static Vector2 MeasureForce(Entity entity)
        {
            Vector2 force = Vector2.Zero;
            foreach (EntityComponent component in entity.Components)
            {
                if (component is IForceComponent)
                {
                    IForceComponent entityForce = component as IForceComponent;
                    force += entityForce.Force;
                }
            }

            return force;
        }
        #endregion
    }
}
