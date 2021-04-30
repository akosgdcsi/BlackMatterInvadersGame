// <copyright file="Enemy.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace BlackMatter.Model
{
    using Newtonsoft.Json;

    /// <summary>
    /// enemy class.
    /// </summary>
    public class Enemy : GameObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Enemy"/> class.
        /// </summary>
        /// <param name="x">init x.</param>
        /// <param name="y">init y.</param>
        /// <param name="isShooted">init isshooted.</param>
        public Enemy(double x, double y, bool isShooted = false)
            : base(x, y)
        {
            this.IsShooted = isShooted;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Enemy"/> class.
        /// </summary>
        /// <param name="x">init x.</param>
        /// <param name="y">init y.</param>
        /// <param name="width">init width.</param>
        /// <param name="height">init height.</param>
        [JsonConstructor]
        public Enemy(double x, double y, int width, int height)
            : base(x, y, width, height)
        {
        }

        /// <summary>
        /// Gets or sets a value indicating whether isShooted.
        /// </summary>
        public bool IsShooted { get; set; }
    }
}