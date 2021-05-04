// <copyright file="LogicTest.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace BlackMatter.Logic.Test
{
    using System.Collections.Generic;
    using BlackMatter.Logic;
    using BlackMatter.Logic.Interfaces;
    using BlackMatter.Model;
    using Moq;
    using NUnit.Framework;

    /// <summary>
    /// Documentation of the public LogicTest class.
    /// </summary>
    [TestFixture]
    public class LogicTest
    {
        private static IGameLogic gameLogic;
        private static Mock<GameModel> modelMock;

        /// <summary>
        /// init test objects.
        /// </summary>
        [SetUp]
        public void Init()
        {
            modelMock = new Mock<GameModel>();
            modelMock.Object.Player = new Player(400, 700, 3);
            List<Enemy> enemies = new List<Enemy>();
            enemies.Add(new Enemy(50, 10));
            enemies.Add(new Enemy(150, 10));
            enemies.Add(new Enemy(250, 10));
            modelMock.Object.Enemies = enemies;
            modelMock.Object.EnemyBullets = new List<Bullet>();
            modelMock.Object.PlayerBullets = new List<Bullet>();

            modelMock.Object.PlayerBullets.Add(new Bullet(10, 900));
            modelMock.Object.PlayerBullets.Add(new Bullet(25, 900));
            modelMock.Object.PlayerBullets.Add(new Bullet(60, 900));

            modelMock.Object.EnemyBullets.Add(new Bullet(50, 400));
            modelMock.Object.EnemyBullets.Add(new Bullet(100, 500));
            modelMock.Object.EnemyBullets.Add(new Bullet(150, 700));
            modelMock.Object.Wave = 1;
            modelMock.Object.Enemiesinthiswave = 50;

            gameLogic = new GameLogic(modelMock.Object);
        }

        /// <summary>
        /// tests player movemnet.
        /// </summary>
        [Test]
        public void PlayerMove()
        {
            double expectedPlayerX = 410;

            gameLogic.PlayerMove(10);

            Assert.That(modelMock.Object.Player.X, Is.EqualTo(expectedPlayerX));
        }

        /// <summary>
        /// tests enemy movement.
        /// </summary>
        [Test]
        public void EnemyMove()
        {
            double expectedposition = modelMock.Object.Enemies[0].Y + GameModel.GameHeight / 14;

            gameLogic.EnemyMove();

            Assert.That(modelMock.Object.Enemies[0].Y, Is.EqualTo(expectedposition));
        }

        /// <summary>
        /// tests player shooting.
        /// </summary>
        [Test]
        public void Shoot()
        {
            double expectedbulletpositionX = modelMock.Object.Player.X + 15;
            double expectedbulletpositionY = modelMock.Object.Player.Y - 1;

            Bullet b = gameLogic.Shoot();

            Assert.That(b.X, Is.EqualTo(expectedbulletpositionX));
            Assert.That(b.Y, Is.EqualTo(expectedbulletpositionY));
        }

        /// <summary>
        /// tests player bullet movement.
        /// </summary>
        [Test]
        public void BulletMove()
        {
            Bullet b = new Bullet(10, 900);

            double expectedbulletposition = b.Y - 5;

            gameLogic.BulletMove(ref b);

            Assert.That(b.Y, Is.EqualTo(expectedbulletposition));
        }

        /// <summary>
        /// tests player bullet movement.
        /// </summary>
        [Test]
        public void EnemyBulletMove()
        {
            double expectedenemybulletmoveY0 = modelMock.Object.EnemyBullets[0].Y + 1;
            double expectedenemybulletmoveY1 = modelMock.Object.EnemyBullets[1].Y + 1;
            double expectedenemybulletmoveY2 = modelMock.Object.EnemyBullets[2].Y + 1;

            gameLogic.EnemyBulletMove();

            Assert.That(modelMock.Object.EnemyBullets[0].Y, Is.EqualTo(expectedenemybulletmoveY0));
            Assert.That(modelMock.Object.EnemyBullets[1].Y, Is.EqualTo(expectedenemybulletmoveY1));
            Assert.That(modelMock.Object.EnemyBullets[2].Y, Is.EqualTo(expectedenemybulletmoveY2));
        }
    }
}