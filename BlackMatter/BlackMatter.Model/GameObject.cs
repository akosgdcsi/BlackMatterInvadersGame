using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace BlackMatter.Model
{
    public abstract class GameObject
    {
        public Rect hitbox;
        public double X { get; set; }
        public double Y { get; set; }

        public GameObject(double x, double y)
        {
            X = x;
            Y = y;

        }
        public GameObject(double x , double y , int width , int height)
        {
            hitbox = new Rect((int)x, (int)y, width, height);
            X = x;
            Y = y;
        }
        public bool Collide(GameObject other)
        {
            return hitbox.IntersectsWith(other.hitbox);
        }

    }
}
