// <copyright file="GameObject.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace BlackMatter.Model
{
    using System.Windows;

    /// <summary>
    /// this is game object file.
    /// </summary>
    public abstract class GameObject
    {
        /// <summary>
        /// creats a hitbox.
        /// </summary>
        public Rect hitbox;

        /// <summary>
        /// Initializes a new instance of the <see cref="GameObject"/> class.
        /// </summary>
        /// <param name="x">init x.</param>
        /// <param name="y">init y.</param>
        /// <param name="width">init width.</param>
        /// <param name="height">init height.</param>
        public GameObject(double x, double y, int width, int height)
        {
            this.hitbox = new Rect((int)x, (int)y, width, height);
            this.X = x;
            this.Y = y;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GameObject"/> class.
        /// </summary>
        /// <param name="x">init x.</param>
        /// <param name="y">init y.</param>
        public GameObject(double x, double y)
        {
            this.X = x;
            this.Y = y;
        }

        /// <summary>
        /// Gets or sets an x cord.
        /// </summary>
        public double X { get; set; }

        /// <summary>
        /// Gets or sets an y cord.
        /// </summary>
        public double Y { get; set; }

        /// <summary>
        /// makes cillision.
        /// </summary>
        /// <param name="other">init a game object.</param>
        /// <returns>with true or false.</returns>
        public bool Collide(GameObject other)
        {
            return this.hitbox.IntersectsWith(other.hitbox);
        }
    }
}