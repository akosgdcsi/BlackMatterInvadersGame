using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackMatter.Model.Interfaces
{
    public interface IGameModel
    {

        Player player { get; set; }
        List<Enemy> enemies { get; set; }
        List<Bullet> PlayerBullets { get; set; }
        List<Bullet> EnemyBullets { get; set; }
        static double GameWidth { get { return 1000; } }
        public static double GameHeight { get { return 1000; } }
        public int Wave { get; set; }
        public int Enemiesinthiswave { get; set; }
        public int Score { get; set; }
    }
}
