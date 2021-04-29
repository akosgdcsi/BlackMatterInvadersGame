// <copyright file="GameModel.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace BlackMatter.Model
{
    using System.Collections.Generic;
    using BlackMatter.Model.Interfaces;

    /// <summary>
    /// this is gam model class.
    /// </summary>
    public class GameModel : IGameModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GameModel"/> class.
        /// </summary>
        /// <param name="player">init a player.</param>
        /// <param name="enemies">init enemies.</param>
        /// <param name="playerBullets">init playerbullets.</param>
        /// <param name="enemyBullets">init enemybullets.</param>
        /// <param name="wave">init wave.</param>
        public GameModel(Player player, List<Enemy> enemies, List<Bullet> playerBullets, List<Bullet> enemyBullets, int wave)
        {
            this.Player = player;
            this.Enemies = enemies;
            this.PlayerBullets = playerBullets;
            this.EnemyBullets = enemyBullets;
            this.Wave = wave;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GameModel"/> class.
        /// </summary>
        public GameModel()
        {
        }

        /// <summary>
        /// Gets the game width.
        /// </summary>
        public static double GameWidth { get { return 800; } }

        /// <summary>
        /// Gets the game width.
        /// </summary>
        public static double GameHeight { get { return 800; } }

        /// <inheritdoc/>
        public Player Player { get; set; }

        /// <inheritdoc/>
        public List<Enemy> Enemies { get; set; }

        /// <inheritdoc/>
        public List<Bullet> PlayerBullets { get; set; }

        /// <inheritdoc/>
        public List<Bullet> EnemyBullets { get; set; }

        /// <inheritdoc/>
        public int Wave { get; set; }

        /// <inheritdoc/>
        public int Enemiesinthiswave { get; set; }

        /// <inheritdoc/>
        public int Score { get; set; }
    }
}