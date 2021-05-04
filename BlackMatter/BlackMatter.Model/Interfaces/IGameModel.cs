// <copyright file="IGameModel.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace BlackMatter.Model.Interfaces
{
    using System.Collections.Generic;

    /// <summary>
    /// Interface of gamemodel.
    /// </summary>
    public interface IGameModel
    {
        /// <summary>
        /// Gets or sets a Player.
        /// </summary>
        Player Player { get; set; }

        /// <summary>
        /// Gets or sets the enemeies.
        /// </summary>
        List<Enemy> Enemies { get; set; }

        /// <summary>
        /// Gets or sets the Player bullets.
        /// </summary>
        List<Bullet> PlayerBullets { get; set; }

        /// <summary>
        /// Gets or sets the Player bullets.
        /// </summary>
        List<Bullet> EnemyBullets { get; set; }

        /// <summary>
        /// Gets the game width.
        /// </summary>
        static double GameWidth { get { return 800; } }

        /// <summary>
        /// Gets the game height.
        /// </summary>
        public static double GameHeight { get { return 800; } }

        /// <summary>
        /// Gets or sets the wave number.
        /// </summary>
        public int Wave { get; set; }

        /// <summary>
        /// Gets or sets the enemies in the wave.
        /// </summary>
        public int Enemiesinthiswave { get; set; }

        /// <summary>
        /// Gets or sets the score.
        /// </summary>
        public int Score { get; set; }
    }
}