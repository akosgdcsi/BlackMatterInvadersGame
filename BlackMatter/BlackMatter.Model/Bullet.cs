using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace BlackMatter.Model
{
    public class Bullet : GameObject
    {
        public bool IsCollided { get; set; }
        public DispatcherTimer Timer { get; set; }
        public Bullet(double x, double y, bool isCollided = false) : base(x, y)
        {
            IsCollided = isCollided;
        }
        public Bullet(double x, double y, int width, int height) : base(x, y, width, height)
        {

        }
    }
}
