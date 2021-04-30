// <copyright file="Bullet.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace BlackMatter.Model
{
    using System.Windows.Threading;
    using Newtonsoft.Json;

    /// <summary>
    /// bullet class.
    /// </summary>
    public class Bullet : GameObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Bullet"/> class.
        /// </summary>
        /// <param name="x">init x.</param>
        /// <param name="y">init y.</param>
        /// <param name="isCollided">init isCollided.</param>
        public Bullet(double x, double y, bool isCollided = false)
            : base(x, y)
        {
            this.IsCollided = isCollided;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Bullet"/> class.
        /// </summary>
        /// <param name="x">init x.</param>
        /// <param name="y">init y.</param>
        /// <param name="width">init width.</param>
        /// <param name="height">init height.</param>
        [JsonConstructor]
        public Bullet(double x, double y, int width, int height)
            : base(x, y, width, height)
        {
        }

        /// <summary>
        /// Gets or sets a value indicating whether IsCollided.
        /// </summary>
        public bool IsCollided { get; set; }

        /// <summary>
        /// Gets or sets a timer.
        /// </summary>
        public DispatcherTimer Timer { get; set; }
    }
}