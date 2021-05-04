// <copyright file="Highscore.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace BlackMatter.Model
{
    /// <summary>
    /// Highscore class.
    /// </summary>
    public class Highscore
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Highscore"/> class.
        /// </summary>
        /// <param name="name">init name.</param>
        /// <param name="score">init score.</param>
        public Highscore(string name, int score)
        {
            this.Name = name;
            this.Score = score;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Highscore"/> class.
        /// </summary>
        public Highscore()
        {
        }

        /// <summary>
        /// Gets or sets a name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets a score.
        /// </summary>
        public int Score { get; set; }
    }
}