using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Collections;

namespace Platformer.Math.Collision
{
    public class Polygon : IEnumerable<Vector2>
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="Polygon"/> class.
        /// </summary>
        public Polygon()
        {
            this._vertices = new List<Vector2>();
            this._edges = new List<Vector2>();
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="Polygon"/> class.
        /// </summary>
        /// <param name="vertices">The vertices.</param>
        public Polygon(IEnumerable<Vector2> vertices)
        {
            this._vertices = vertices.ToList();
            this._edges = new List<Vector2>();

            this.BuildEdges();
        }
        /// <summary>
        /// Initializes a new rectangular instance of the <see cref="Polygon"/> class.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        public Polygon(int x, int y, int width, int height) : this()
        {
            this._vertices.Add(new Vector2(x, y));
            this._vertices.Add(new Vector2(x + width, y));
            this._vertices.Add(new Vector2(x + width, y + height));
            this._vertices.Add(new Vector2(x, y + height));

            this.BuildEdges();
        }
        #endregion

        #region Fields
        private List<Vector2> _vertices;
        private List<Vector2> _edges;
        #endregion

        #region Properties
        /// <summary>
        /// Gets the bounds.
        /// </summary>
        public Rectangle Bounds
        {
            get
            {
                float minX = float.MaxValue;
                float maxX = float.MinValue;
                float minY = float.MaxValue;
                float maxY = float.MinValue;

                foreach (Vector2 vector in this._vertices)
                {
                    if (vector.X < minX) minX = vector.X;
                    if (vector.X > maxX) maxX = vector.X;
                    if (vector.Y < minY) minY = vector.Y;
                    if (vector.Y > maxY) maxY = vector.Y;
                }

                return new Rectangle(minX, minY, maxX - minX, maxY - minY);
            }
        }
        /// <summary>
        /// Gets or sets the <see cref="Platformer.Math.Vector2"/> at the specified index.
        /// </summary>
        public Vector2 this[int index]
        {
            get { return this._vertices[index]; }
            set { this._vertices[index] = value; }
        }
        /// <summary>
        /// Gets the center point.
        /// </summary>
        public Vector2 Center
        {
            get
            {
                Vector2 total = Vector2.Zero;
                for (int i = 0; i < this._vertices.Count; i++)
                {
                    total += this._vertices[i];
                }

                total *= 1.0f / this._vertices.Count;
                return total;
            }
        }
        /// <summary>
        /// Gets the vertices.
        /// </summary>
        internal List<Vector2> Vertices
        {
            get { return this._vertices; }
        }
        /// <summary>
        /// Gets the edges.
        /// </summary>
        internal List<Vector2> Edges
        {
            get { return this._edges; }
        }
        #endregion

        #region Methods
        /// <summary>
        /// Builds the edges.
        /// </summary>
        private void BuildEdges()
        {
            this._edges.Clear();
            for (int i = 0; i < this._vertices.Count; i++)
            {
                Vector2 a = this._vertices[i + 1 >= this._vertices.Count ? 0 : i + 1];
                Vector2 b = this._vertices[i];

                //Due to floating point values we had to round
                //the specified vector fixing a bug.
                Vector2 edge = Vector2.Round(a - b);

                this._edges.Add(edge);
            }
        }
        /// <summary>
        /// Adds the specified vector to the polygon.
        /// </summary>
        /// <param name="vector">The vector.</param>
        public void Add(Vector2 vector)
        {
            this._vertices.Add(vector);
            this.BuildEdges();
        }
        /// <summary>
        /// Removes the specified vector to the polygon.
        /// </summary>
        /// <param name="vector">The vector.</param>
        public void Remove(Vector2 vector)
        {
            this._vertices.Remove(vector);
            this.BuildEdges();
        }
        #endregion

        #region IEnumerable<Vector2> Member
        /// <summary>
        /// Gets the enumerator.
        /// </summary>
        public IEnumerator<Vector2> GetEnumerator()
        {
            return this._vertices.GetEnumerator();
        }
        #endregion

        #region IEnumerable Member
        /// <summary>
        /// Gets the enumerator.
        /// </summary>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
        #endregion
    }
}
