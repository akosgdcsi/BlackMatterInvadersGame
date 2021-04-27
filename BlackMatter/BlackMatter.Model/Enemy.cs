using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackMatter.Model
{
    public class Enemy : GameObject
    {
        public bool IsShooted { get; set; }

        public Enemy(double x ,double y, bool isShooted = false):base(x,y)
        {
            IsShooted = IsShooted;
        }
        public Enemy(double x, double y, int width, int height) :base(x,y,width,height)
        {

        }
    }
}
