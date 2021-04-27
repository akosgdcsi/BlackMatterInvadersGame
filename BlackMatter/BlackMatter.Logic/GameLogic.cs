using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlackMatter.Logic.Interfaces;
using BlackMatter.Model;
using BlackMatter.Model.Interfaces;

namespace BlackMatter.Logic
{
    public class GameLogic : IGameLogic
    {
        Random rnd = new Random();
        IGameModel model;
        double Space;
        public int EnemyInThisRow { get; set; }
        public GameLogic(IGameModel model)
        {
            this.model = model;
        }

        public IGameModel InitModel()
        {
            model = new GameModel(new Player(GameModel.GameWidth / 2, GameModel.GameHeight - 200,3),new List<Enemy>(),new List<Bullet>(),new List<Bullet>(),1);
            Space=GameModel.GameWidth/8;
            model.Enemiesinthiswave = model.Wave * 50;
            model.enemies = EnemyPlacer();            
            return model;
        }


        private List<Enemy> EnemyPlacer()
        {
            double[] xplace = new double[8];
            
            for (int i = 0; i < xplace.Length; i++)
            {
                xplace[i] =(i * (GameModel.GameWidth / 8));
            }
            EnemyInThisRow = rnd.Next(0, 6);
            double[] enemyplacer = new double[EnemyInThisRow];
            for (int i = 0; i < EnemyInThisRow; i++)
            {
                enemyplacer[i] = xplace[rnd.Next(0, 8)];
            }
            List<Enemy> enemies = new List<Enemy>();
            foreach (var item in enemyplacer)
            {
                enemies.Add(new Enemy(item, 10,140,140));
                model.Enemiesinthiswave--;
            }

            return enemies;
        }

        public void PlayerMove(int dx)
        {
            int newx = (int)(model.player.X + dx);
            if  (newx<0)
            {
                newx = (int)GameModel.GameWidth-50;
            }
            else if (newx>GameModel.GameWidth-50)
            {
                newx = 0;
            }
            model.player.X = newx;
        }

        public void EnemyMove()
        {
            foreach (var item in model.enemies)
            {
                item.Y += GameModel.GameHeight / 14;
                item.hitbox.Y = (int)item.Y;
            }
            double[] xplace = new double[8];

            for (int i = 0; i < xplace.Length; i++)
            {
                xplace[i] =(i * (GameModel.GameWidth / 8));
            }
            if (model.Enemiesinthiswave < 8)
            {
                EnemyInThisRow = rnd.Next(0, model.Enemiesinthiswave);
            }
            else
            {
                EnemyInThisRow = rnd.Next(0, 8);
            }
            
            double[] enemyplacer = new double[EnemyInThisRow];
            for (int i = 0; i < EnemyInThisRow; i++)
            {
                enemyplacer[i] = xplace[rnd.Next(0, 8)];
            }
            
            foreach (var item in enemyplacer)
            {
                model.enemies.Add(new Enemy(item, 10,140,140));
                model.Enemiesinthiswave--;
            }
            foreach (var item in model.enemies)
            {
                if (item.Y>GameModel.GameHeight-200)
                {
                    PlayerDmg();
                }
            }            
        }

        public Bullet Shoot()
        {
            Bullet bullet = new Bullet(model.player.X + 15, model.player.Y - 1,50,50);

            return bullet;
        }

        public void BulletMove(ref Bullet bullet)
        {
            bullet.Y -= 5;
            bullet.hitbox.Y = (int)bullet.Y;
            foreach (var item in model.enemies.ToList())
            {
                if (bullet.Collide(item))
                {
                    EnemyDies(item);
                }
            }
        }
        public void Enemyshoot()
        {
            var q1 = (from x in model.enemies
                 where x == ClosestEnemy()
                 select x).FirstOrDefault();

            Bullet bullet = new Bullet(q1.X, q1.Y - 1);
            model.EnemyBullets.Add(bullet);
        }
        public void EnemyBulletMove()
        {
            foreach (var item in model.EnemyBullets)
            {
                if (item.Y < GameModel.GameHeight)
                {
                    item.Y += 0.5;
                }
                else
                {
                    model.EnemyBullets.Remove(item);
                }
            }
        }
        private Enemy ClosestEnemy()
        {
            List<Enemy> enemies = FrontRowEnemies();
            double min = double.PositiveInfinity;
            Enemy closestEnemy = null;
            foreach (var item in enemies)
            {
                if (min > Math.Sqrt(Math.Abs(model.player.X - item.X) + Math.Abs(model.player.Y - item.Y)))
                {
                    min = Math.Sqrt(Math.Abs(model.player.X - item.X) + Math.Abs(model.player.Y - item.Y));
                    closestEnemy = item;
                }
            }
            return closestEnemy;
        }
        private List<Enemy> FrontRowEnemies()
        {
            var q1 = (from x in model.enemies
                      orderby x.Y descending
                      select x.Y).FirstOrDefault();
             
            return (from x in model.enemies
                     where x.Y == q1
                     select x).ToList();
        }

        public void EnemyDies(Enemy enemy)
        {
            model.enemies.Remove(enemy);
        }

        public void PlayerDmg()
        {
            if (model.player.Life>=1)
            {
                model.player.Life -= 1;
            }
            
        }
        public void PlayerDies()
        {

        }

    }
}
