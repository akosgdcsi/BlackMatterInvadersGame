// <copyright file="GameLogic.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace BlackMatter.Logic
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using BlackMatter.Logic.Interfaces;
    using BlackMatter.Model;
    using BlackMatter.Model.Interfaces;
    using BlackMatter.Repository;

    /// <summary>
    /// this is gamelogic class.
    /// </summary>
    public class GameLogic : IGameLogic
    {
        private Random rnd = new Random();
        private IGameModel model;
        private double space;

        /// <summary>
        /// Initializes a new instance of the <see cref="GameLogic"/> class.
        /// </summary>
        /// <param name="model">model instance.</param>
        public GameLogic(IGameModel model)
        {
            this.model = model;
        }

        /// <summary>
        /// Gets or sets an enemyraw.
        /// </summary>
        public int EnemyInThisRow { get; set; }

        /// <summary>
        /// Gets or sets a score.
        /// </summary>
        public int Score { get; set; }

        /// <inheritdoc/>
        public IGameModel InitModel()
        {
            this.model = new GameModel(new Player(GameModel.GameWidth / 2, GameModel.GameHeight - 150, 100, 100, 3), new List<Enemy>(), new List<Bullet>(), new List<Bullet>(), 1);
            this.space = GameModel.GameWidth / 8;
            this.model.Enemiesinthiswave = this.model.Wave * 10;
            this.model.Enemies = this.EnemyPlacer();
            this.model.Score = this.Score;
            return this.model;
        }

        /// <inheritdoc/>
        public void PlayerMove(int dx)
        {
            int newx = (int)(this.model.Player.X + dx);

            if (newx < 0)
            {
                newx = (int)GameModel.GameWidth - 50;
            }
            else if (newx > GameModel.GameWidth - 50)
            {
                newx = 0;
            }

            this.model.Player.X = newx;
            this.model.Player.hitbox.X = newx;
        }

        /// <inheritdoc/>
        public void EnemyMove()
        {
            foreach (var item in this.model.Enemies)
            {
                item.Y += GameModel.GameHeight / 14;
                item.hitbox.Y = (int)item.Y;
            }

            double[] xplace = new double[8];

            for (int i = 0; i < xplace.Length; i++)
            {
                xplace[i] = i * (GameModel.GameWidth / 8);
            }

            if (this.model.Enemiesinthiswave < 8)
            {
                  this.EnemyInThisRow = this.rnd.Next(0, this.model.Enemiesinthiswave + 1);
            }
            else
            {
                this.EnemyInThisRow = this.rnd.Next(0, 8);
            }

            double[] enemyplacer = new double[this.EnemyInThisRow];
            for (int i = 0; i < this.EnemyInThisRow; i++)
            {
                enemyplacer[i] = xplace[this.rnd.Next(0, 8)];
            }

            foreach (var item in enemyplacer)
            {
                this.model.Enemies.Add(new Enemy(item, 10, 50, 50));
                this.model.Enemiesinthiswave--;
            }

            foreach (var item in this.model.Enemies)
            {
                if (item.Y > GameModel.GameHeight - 200)
                {
                    this.PlayerDmg();
                }
            }
        }

        /// <inheritdoc/>
        public Bullet Shoot()
        {
            Bullet bullet = new Bullet(this.model.Player.X + 15, this.model.Player.Y - 1, 8, 60);

            return bullet;
        }

        /// <inheritdoc/>
        public void BulletMove(ref Bullet bullet)
        {
            bullet.Y -= 5;
            bullet.hitbox.Y = (int)bullet.Y;
            foreach (var item in this.model.Enemies.ToList())
            {
                if (bullet.Collide(item))
                {
                    this.EnemyDies(item);
                    bullet.Timer.Stop();
                    this.model.PlayerBullets.Remove(bullet);
                    this.model.Score += 100;
                }
            }
        }

        /// <inheritdoc/>
        public void Enemyshoot()
        {
            var q1 = (from x in this.model.Enemies
                      where x == this.ClosestEnemy()
                      select x).FirstOrDefault();

            Bullet bullet = new Bullet(q1.X, q1.Y - 1);
            this.model.EnemyBullets.Add(bullet);
        }// DELETABLE?

        /// <inheritdoc/>
        public Bullet Enemyshoot2()
        {
            var q1 = (from x in this.model.Enemies
                      where x == this.ClosestEnemy()
                      select x).FirstOrDefault();

            Bullet bullet = new Bullet(q1.X + 45, q1.Y + 90, 8, 40);
            return bullet;
        }

        /// <inheritdoc/>
        public void EnemyBulletMove()
        {
            foreach (var item in this.model.EnemyBullets)
            {
                if (item.Y < GameModel.GameHeight)
                {
                    item.Y += 1;
                    item.hitbox.Y = (int)item.Y;
                    if (item.Collide(this.model.Player))
                    {
                        this.PlayerDmg();
                    }
                }
                else
                {
                    this.model.EnemyBullets.Remove(item);
                }
            }
        }// DELETABLE?

        /// <inheritdoc/>
        public void EnemyBulletMove2(ref Bullet bullet)
        {
            if (bullet.Y < GameModel.GameHeight)
            {
                bullet.Y += 1;
                bullet.hitbox.Y = (int)bullet.Y;
                if (bullet.Collide(this.model.Player))
                {
                    this.PlayerDmg();
                    bullet.Timer.Stop();
                    this.model.EnemyBullets.Remove(bullet);
                }
            }
            else
            {
                this.model.EnemyBullets.Remove(bullet);
            }
        }

        /// <inheritdoc/>
        public void EnemyDies(Enemy enemy)
        {
            this.model.Enemies.Remove(enemy);
        }

        /// <inheritdoc/>
        public void PlayerDmg()
        {
            if (this.model.Player.Life >= 1)
            {
                this.model.Player.Life -= 1;
            }
        }

        /// <summary>
        /// saddly this thing happend.
        /// </summary>
        public void PlayerDies()
        {
            if (this.model.Player.Life == 0)
            {
                // PLACEHOLDER FOR DEAD PLAYER
            }
        }

        /// <summary>
        /// sets the next wave.
        /// </summary>
        public void NextWave()
        {
            this.model.Wave++;
            this.model.Enemiesinthiswave = this.model.Wave * 10;
            this.model.Player.Life = 3;
        }

        private Enemy ClosestEnemy()
        {
            List<Enemy> enemies = this.FrontRowEnemies();
            double min = double.PositiveInfinity;
            Enemy closestEnemy = null;
            foreach (var item in enemies)
            {
                if (min > Math.Sqrt(Math.Abs(this.model.Player.X - item.X) + Math.Abs(this.model.Player.Y - item.Y)))
                {
                    min = Math.Sqrt(Math.Abs(this.model.Player.X - item.X) + Math.Abs(this.model.Player.Y - item.Y));
                    closestEnemy = item;
                }
            }

            return closestEnemy;
        }

        private List<Enemy> FrontRowEnemies()
        {
            var q1 = (from x in this.model.Enemies
                      orderby x.Y descending
                      select x.Y).FirstOrDefault();

            return (from x in this.model.Enemies
                    where x.Y == q1
                    select x).ToList();
        }

        private List<Enemy> EnemyPlacer()
        {
            double[] xplace = new double[8];

            for (int i = 0; i < xplace.Length; i++)
            {
                xplace[i] = i * (GameModel.GameWidth / 8);
            }

            this.EnemyInThisRow = this.rnd.Next(0, 6);
            double[] enemyplacer = new double[this.EnemyInThisRow];
            for (int i = 0; i < this.EnemyInThisRow; i++)
            {
                enemyplacer[i] = xplace[this.rnd.Next(0, 8)];
            }

            List<Enemy> enemies = new List<Enemy>();
            foreach (var item in enemyplacer)
            {
                enemies.Add(new Enemy(item, 10, 50, 50));
                this.model.Enemiesinthiswave--;
            }

            return enemies;
        }
    }
}