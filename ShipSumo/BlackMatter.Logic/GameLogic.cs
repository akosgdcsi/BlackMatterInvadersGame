using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlackMatter.Model;


namespace BlackMatter.Logic
{
    class GameLogic
    {
        Random rnd = new Random();
        GameModel model;
        double Margin;
        double Space;
        int enemyrow { get; set; }
        
        public GameLogic(GameModel model)
        {
            this.model = model;
            InitModel();
        }

        private void InitModel()
        {
            model.player.X = model.GameWidth / 2;
            model.player.Y = model.GameHeight - 10;
            model.player.Life = 3;
            Margin = model.GameWidth / 8;
            Space=model.GameWidth/5-2*Margin;
            model.enemies = EnemyPlacer();
            model.Wave = 1;
            model.Enemiesinthiswave = model.Wave * 50;
        }

        private List<Enemy> EnemyPlacer()
        {
            
            List<Enemy> enemies = new List<Enemy>();
            //Wavenkent 50 enemy
            Enemy enemy = new Enemy
            {
                X = Margin + Space / 5,
                Y = 0
            };
            enemies.Add(enemy);
                                   
            enemyrow = rnd.Next(1,3);
            for (int y = 1; y < enemyrow; y++)
            {
                Enemy enemy1 = new Enemy
                {
                    X = Space / 5 * y,
                    Y = 0
                };
                enemies.Add(enemy1);                
            }

            return enemies;
        }

        public void PlayerMove(int dx)
        {
            int newx = (int)(model.player.X + dx);
            if  (newx<0)
            {
                newx = (int)model.GameWidth;
            }
            else if (newx>model.GameWidth)
            {
                newx = 0;
            }

        }

        public void EnemyMove()
        {
            foreach (var item in model.enemies)
            {
                item.Y += model.GameHeight / 14;
            }
            Random rnd = new Random();
            if (model.Enemiesinthiswave > 0)
            {
                Enemy enemy = new Enemy
                {
                    X = Margin + Space / 5,
                    Y = 0
                };
                model.enemies.Add(enemy);

                enemyrow = rnd.Next(1, 3);
                for (int y = 1; y < enemyrow; y++)
                {
                    Enemy enemy1 = new Enemy
                    {
                        X = Space / 5 * y,
                        Y = 0
                    };
                    model.enemies.Add(enemy1);
                }

                
            }

        }

        public void Shoot()
        {
            Bullet bullet = new Bullet
            {
                X = model.player.X,
                Y = model.player.Y - 1,
                IsCollided = false,
                
            };

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
            var q1 = from x in model.enemies
                 where x == ClosestEnemy()
                 select x;

            Bullet bullet = new Bullet
            {
                X = model.player.X,
                Y = model.player.Y - 1,
                IsCollided = false,

            };
            model.EnemyBullets.Add(bullet);
        }
        public void EnemyBulletMove()
        {
            foreach (var item in model.EnemyBullets)
            {
                if (item.Y < model.GameHeight)
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
