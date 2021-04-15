using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackMatter.Model
{
    public class GameModel
    {
        Player player;
        List<Enemy> enemies;

        public double GameWidth;
        public double GameHeight;

        public GameModel(Player player, List<Enemy> enemies, double gameWidth, double gameHeight)
        {
            this.player = player;
            this.enemies = enemies;
            GameWidth = gameWidth;
            GameHeight = gameHeight;
        }
    }
}
