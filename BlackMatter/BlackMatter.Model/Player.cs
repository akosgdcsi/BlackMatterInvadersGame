using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackMatter.Model
{
    public class Player : GameObject
    {
        public int Life { get; set; }

        public Player(double x, double y, int life) :base( x,y)
        {
            Life = life;
        }
        public Player(double x, double y, int width, int height, int life) : base(x, y, width, height)
        {
            Life = life;
        }
    }
}
