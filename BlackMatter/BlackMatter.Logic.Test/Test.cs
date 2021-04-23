using BlackMatter.Logic.Interfaces;
using BlackMatter.Model;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System;

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
            ModelMock.Object.Wave = 1;
            ModelMock.Object.Enemiesinthiswave = 50;


            gameLogic = new GameLogic(ModelMock.Object);
        }
        [Test]
        public void PlayerMove()
        {

            
        } 
    }
}
