using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackMatter.Logic.Interfaces
{
    interface IGameLogic
    {
        void InitModel();
        void PlayerMove();
        void EnemyMove();
        void PlayerFire();
        void EnemyFire();    
    }
}
