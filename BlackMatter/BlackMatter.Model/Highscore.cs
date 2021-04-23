using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackMatter.Model
{
    public class Highscore
    {
        public string Name { get; set; }
        public int Score { get; set; }

        public Highscore(string name, int score)
        {
            Name = name;
            Score = score;
        }
        public Highscore()
        {

        }
    }
}
