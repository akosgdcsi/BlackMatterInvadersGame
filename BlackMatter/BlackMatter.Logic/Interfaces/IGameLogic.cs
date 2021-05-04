// <copyright file="IGameLogic.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace BlackMatter.Logic.Interfaces
{
    using BlackMatter.Model;
    using BlackMatter.Model.Interfaces;

    /// <summary>
    /// interface of GameLogic.
    /// </summary>
    public interface IGameLogic
    {
        /// <summary>
        /// initialize a the gamemodel.
        /// </summary>
        /// <returns>IGameModel status.</returns>
        IGameModel InitModel();

        /// <summary>
        /// resposible for moveing the player.
        /// </summary>
        /// <param name="dx">this is the distance to move.</param>
        void PlayerMove(int dx);

        /// <summary>
        /// resposible for moveing the enemy.
        /// </summary>
        void EnemyMove();

        /// <summary>
        /// creats a bullet.
        /// </summary>
        /// <returns>a bullet object.</returns>
        Bullet Shoot();

        /// <summary>
        /// moves all the player bullet on the field.
        /// </summary>
        /// <param name="bullet">get a bullet referance.</param>
        void BulletMove(ref Bullet bullet);

        /// <summary>
        /// creats a bullet for the enemy.
        /// </summary>
        void Enemyshoot();

        /// <summary>
        /// creats a bullet for the enemy.
        /// </summary>
        /// <returns>a bullett object.</returns>
        Bullet Enemyshoot2();

        /// <summary>
        /// moves all the enemy bullet on the field.
        /// </summary>
        void EnemyBulletMove();

        /// <summary>
        /// moves all the enemy bullet on the field.
        /// </summary>
        /// <param name="bullet">get a bullet referance.</param>
        void EnemyBulletMove2(ref Bullet bullet);

        /// <summary>
        /// removes an enemy from the list.
        /// </summary>
        /// <param name="enemy">deathrow enemy.</param>
        void EnemyDies(Enemy enemy);

        /// <summary>
        /// removes 1 life from player if has not got any.
        /// </summary>
        void PlayerDmg();

        /// <summary>
        /// sets the next wave.
        /// </summary>
        void NextWave();
    }
}