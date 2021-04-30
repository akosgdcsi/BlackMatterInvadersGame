// <copyright file="Player.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace BlackMatter.Model
{
    using Newtonsoft.Json;

    /// <summary>
    /// player class.
    /// </summary>
    public class Player : GameObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Player"/> class.
        /// </summary>
        /// <param name="x">init x.</param>
        /// <param name="y">init y.</param>
        /// <param name="life">init life.</param>
        public Player(double x, double y, int life)
            : base(x, y)
        {
            this.Life = life;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Player"/> class.
        /// </summary>
        /// <param name="x">init x.</param>
        /// <param name="y">init y.</param>
        /// <param name="width">init width.</param>
        /// <param name="height">init height.</param>
        /// <param name="life">init life.</param>
        [JsonConstructor]
        public Player(double x, double y, int width, int height, int life)
            : base(x, y, width, height)
        {
            this.Life = life;
        }

        /// <summary>
        /// Gets or sets a Life.
        /// </summary>
        public int Life { get; set; }
    }
}