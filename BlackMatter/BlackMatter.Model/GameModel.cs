using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackMatter.Model
{
    public class GameModel
    {
        public Player player { get; set; }
        public List<Enemy> enemies { get; set; }
        public List<Bullet> PlayerBullets { get; set; }
        public List<Bullet> EnemyBullets { get; set; }
        public double GameWidth { get; set; }
        public double GameHeight { get; set; }
        public int Wave { get; set; }
        public int Enemiesinthiswave { get; set; }

        public GameModel(Player player, List<Enemy> enemies, List<Bullet> playerBullets, List<Bullet> enemyBullets, double gameWidth, double gameHeight, int Wave, int Enemiesinthiswave)
        {
            this.player = player;
            this.enemies = enemies;
            this.PlayerBullets = playerBullets;
            this.EnemyBullets = enemyBullets;
            GameWidth = gameWidth;
            GameHeight = gameHeight;
            this.Wave = Wave;
            this.Enemiesinthiswave = Enemiesinthiswave;
        }
    }
}
