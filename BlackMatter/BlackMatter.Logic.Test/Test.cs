using BlackMatter.Logic.Interfaces;
using BlackMatter.Model;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System;
using BlackMatter.Model.Interfaces;

namespace BlackMatter.Logic.Test
{
    [TestFixture]
    public class Test
    {
        private static IGameLogic gameLogic;
        private static Mock<GameModel> ModelMock;

        [SetUp]
        public void Init()
        {
            ModelMock = new Mock<GameModel>();            
            ModelMock.Object.player = new Player(400, 700, 3);
            List<Enemy> enemies = new List<Enemy>();
            enemies.Add(new Enemy(50, 10));
            enemies.Add(new Enemy(150, 10));
            enemies.Add(new Enemy(250, 10));
            ModelMock.Object.enemies = enemies;
            ModelMock.Object.EnemyBullets = new List<Bullet>();
            ModelMock.Object.PlayerBullets = new List<Bullet>();

            ModelMock.Object.PlayerBullets.Add(new Bullet(10, 900));
            ModelMock.Object.PlayerBullets.Add(new Bullet(25, 900));
            ModelMock.Object.PlayerBullets.Add(new Bullet(60, 900));

            ModelMock.Object.EnemyBullets.Add(new Bullet(50, 400));
            ModelMock.Object.EnemyBullets.Add(new Bullet(100, 500));
            ModelMock.Object.EnemyBullets.Add(new Bullet(150, 700));
            ModelMock.Object.Wave = 1;
            ModelMock.Object.Enemiesinthiswave = 50;


            gameLogic = new GameLogic(ModelMock.Object);
        }
        [Test]
        public void PlayerMove()
        {
            double ExpectedPlayerX = 410;

            gameLogic.PlayerMove(10);

            Assert.That(ModelMock.Object.player.X, Is.EqualTo(ExpectedPlayerX));
        }

        [Test]
        public void EnemyMove()
        {
            double expectedposition = ModelMock.Object.enemies[0].Y + GameModel.GameHeight / 14;

            gameLogic.EnemyMove();

            Assert.That(ModelMock.Object.enemies[0].Y, Is.EqualTo(expectedposition));
        }
        [Test]
        public void Shoot()
        {
            double expectedbulletpositionX = ModelMock.Object.player.X + 15;
            double expectedbulletpositionY = ModelMock.Object.player.Y -1;

            Bullet b = gameLogic.Shoot();

            Assert.That(b.X, Is.EqualTo(expectedbulletpositionX));
            Assert.That(b.Y, Is.EqualTo(expectedbulletpositionY));
        }

        [Test]
        public void BulletMove()
        {
            Bullet b = new Bullet(10, 900);

            double expectedbulletposition = b.Y - 5;

            gameLogic.BulletMove(ref b);

            Assert.That(b.Y, Is.EqualTo(expectedbulletposition));
        }

        [Test]
        public void EnemyBulletMove()
        {
            double expectedenemybulletmoveY0 = ModelMock.Object.EnemyBullets[0].Y + 0.5;
            double expectedenemybulletmoveY1 = ModelMock.Object.EnemyBullets[1].Y + 0.5;
            double expectedenemybulletmoveY2 = ModelMock.Object.EnemyBullets[2].Y + 0.5;

            gameLogic.EnemyBulletMove();

            Assert.That(ModelMock.Object.EnemyBullets[0].Y, Is.EqualTo(expectedenemybulletmoveY0));
            Assert.That(ModelMock.Object.EnemyBullets[1].Y, Is.EqualTo(expectedenemybulletmoveY1));
            Assert.That(ModelMock.Object.EnemyBullets[2].Y, Is.EqualTo(expectedenemybulletmoveY2));
        }
        
    }
}
