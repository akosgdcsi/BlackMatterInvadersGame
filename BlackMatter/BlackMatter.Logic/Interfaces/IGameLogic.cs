using BlackMatter.Model;
using BlackMatter.Model.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackMatter.Logic.Interfaces
{
    public interface IGameLogic
    {
        IGameModel InitModel();
        void PlayerMove(int dx);
        void EnemyMove();
        void Shoot();
        void BulletMove();
        void Enemyshoot();
        void EnemyBulletMove();
        void EnemyDies(Enemy enemy);
        void PlayerDmg();
    }
}
