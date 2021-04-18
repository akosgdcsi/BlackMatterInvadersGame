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
        double Margin;
        double Space;
        int enemyrow { get; set; }
        
        public GameLogic(IGameModel model)
        {
            this.model = model;
        }

        public IGameModel InitModel()
        {
            model = new GameModel(new Player(GameModel.GameWidth / 2, GameModel.GameHeight - 200,3),new List<Enemy>(),new List<Bullet>(),new List<Bullet>(),1);
            Margin = GameModel.GameWidth / 8;
            Space=GameModel.GameWidth/5-2*Margin;
            model.enemies = EnemyPlacer();
            model.Enemiesinthiswave = model.Wave * 50;
            return model;
        }

        private List<Enemy> EnemyPlacer()
        {
            
            List<Enemy> enemies = new List<Enemy>();
            //Wavenkent 50 enemy
            Enemy enemy = new Enemy(Margin + Space / 5,0);
            enemies.Add(enemy);
                                   
            enemyrow = rnd.Next(1,3);
            for (int y = 1; y < enemyrow; y++)
            {
                Enemy enemy1 = new Enemy(Space / 5 * y,0);
                enemies.Add(enemy1);                
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
            }
            Random rnd = new Random();
            if (model.Enemiesinthiswave > 0)
            {
                Enemy enemy = new Enemy(Margin + Space / 5,0);
                model.enemies.Add(enemy);

                enemyrow = rnd.Next(1, 3);
                for (int y = 1; y < enemyrow; y++)
                {
                    Enemy enemy1 = new Enemy(Space / 5 * y,0);
                    model.enemies.Add(enemy1);
                }

                
            }

        }

        public void Shoot()
        {
            Bullet bullet = new Bullet(model.player.X, model.player.Y - 1);

            model.PlayerBullets.Add(bullet);
        }

        public void BulletMove()
        {
            foreach (var item in model.PlayerBullets)
            {
                if (item.Y>0)
                {
                    item.Y -= 0.5;
                }
                else
                {
                    model.PlayerBullets.Remove(item);
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

    }
}
