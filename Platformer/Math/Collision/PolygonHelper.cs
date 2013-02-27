using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Platformer.Math.Collision
{
    public static class PolygonHelper
    {
        #region Methods
        /// <summary>
        /// Offsets the specified polygon.
        /// </summary>
        /// <param name="polygon">The polygon.</param>
        /// <param name="offset">The offset.</param>
        public static Polygon Offset(Polygon polygon, Vector2 offset)
        {
            List<Vector2> vertices = new List<Vector2>();
            foreach (Vector2 vector in polygon)
            {
                vertices.Add(vector + offset);
            }

            return new Polygon(vertices);
        }
        /// <summary>
        /// Projects the specified polygon to the specified axis.
        /// </summary>
        /// <param name="polygon">The polygon.</param>
        /// <param name="axis">The axis.</param>
        /// <param name="min">The min.</param>
        /// <param name="max">The max.</param>
        public static void Project(Polygon polygon, Vector2 axis, out float min, out float max)
        {
            min = float.MaxValue;
            max = float.MinValue;

            for (int i = 0; i < polygon.Vertices.Count; i++)
            {
                float currentDot = Vector2.Dot(axis, polygon[i]);
                if (currentDot < min)
                {
                    min = currentDot;
                }
                else if (currentDot > max)
                {
                    max = currentDot;
                }
            }
        }
        #endregion
    }
}
