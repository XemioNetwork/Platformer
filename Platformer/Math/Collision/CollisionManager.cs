using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Platformer.Math.Collision
{
    public class CollisionManager
    {
        #region Methods
        /// <summary>
        /// Intersects the specified polygons and returns detailed information about the collision.
        /// </summary>
        /// <param name="a">The first polygon.</param>
        /// <param name="b">The second polygon.</param>
        /// <param name="velocity">The velocity.</param>
        public CollisionResult Intersect(Rectangle a, Rectangle b, Vector2 velocity)
        {
            CollisionResult result = new CollisionResult();
            Rectangle rectangle = a + velocity;

            if (rectangle.Intersects(b))
            {
                Rectangle overlap = rectangle.Intersect(b);

                Vector2 centerA = a.Center;
                Vector2 centerB = b.Center;

                Vector2 direction = centerA - centerB;
                direction.Normalize();

                float angle = MathHelper.ToAngle(direction);
                float degrees = MathHelper.ToDegrees(angle);

                if (degrees >= 315 || degrees < 45)
                {
                    result.MinimumTranslation = new Vector2(-overlap.Width, 0);
                    result.Direction = Direction.Left;
                }
                if (degrees >= 45 && degrees < 135)
                {
                    result.MinimumTranslation = new Vector2(0, overlap.Height);
                    result.Direction = Direction.Bottom;
                }
                if (degrees >= 135 && degrees < 225)
                {
                    result.MinimumTranslation = new Vector2(overlap.Width, 0);
                    result.Direction = Direction.Right;
                }
                if (degrees >= 225 && degrees < 315)
                {
                    result.MinimumTranslation = new Vector2(0, -overlap.Height);
                    result.Direction = Direction.Top;
                }

                result.Intersecting = true;
            }
            
            return result;
        }
        #endregion
    }
}
