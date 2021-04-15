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
        public double GameWidth { get; set; }
        public double GameHeight { get; set; }

        public GameModel(Player player, List<Enemy> enemies, double gameWidth, double gameHeight)
        {
            this.player = player;
            this.enemies = enemies;
            GameWidth = gameWidth;
            GameHeight = gameHeight;
        }
    }
}
