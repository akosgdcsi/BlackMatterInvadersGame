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
            Margin = model.GameWidth / 8;
            Space=model.GameWidth/5-2*Margin;
            model.enemies = EnemyPlacer();
        }

        private List<Enemy> EnemyPlacer()
        {
            Random rnd = new Random();
            List<Enemy> enemies = new List<Enemy>();
            //Wavenkent 50 enemy
            Enemy enemy = new Enemy();
            enemy.X = Margin + Space/5;
            enemy.Y = 0;
            enemies.Add(enemy);
                                   
            enemyrow = rnd.Next(1,3);
            for (int y = 1; y < enemyrow; y++)
            {
                Enemy enemy1 = new Enemy();
                enemy1.X = Space / 5 * y;
                enemy1.Y = 0;
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
            
        }
    }
}
