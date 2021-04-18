using BlackMatter.Model.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackMatter.Model
{
    public class GameModel : IGameModel
    {
        public Player player { get; set; }
        public List<Enemy> enemies { get; set; }
        public List<Bullet> PlayerBullets { get; set; }
        public List<Bullet> EnemyBullets { get; set; }
        public static double GameWidth { get { return 1000; } }
        public static double GameHeight { get { return 1000; } }
        public int Wave { get; set; }
        public int Enemiesinthiswave { get; set; }

        public GameModel(Player player, List<Enemy> enemies, List<Bullet> playerBullets, List<Bullet> enemyBullets, int Wave)
        {
            this.player = player;
            this.enemies = enemies;
            this.PlayerBullets = playerBullets;
            this.EnemyBullets = enemyBullets;            
            this.Wave = Wave;
            
        }
        
    }
}
