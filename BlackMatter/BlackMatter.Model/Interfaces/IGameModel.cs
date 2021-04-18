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
        Point Player { get; set; }
        List<Point> Enemies { get; set; }
        double GameWidth { get; set; }
        double GameHeight { get; set; }
        int Life { get; set; } // under review!!!!
        int Score { get; set; }
        int Level { get; set; }
    }
}
