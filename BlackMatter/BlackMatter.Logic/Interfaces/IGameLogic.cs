﻿using BlackMatter.Model;
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
        Bullet Shoot();
        void BulletMove(ref Bullet bullet);
        void Enemyshoot();
        Bullet Enemyshoot2();
        void EnemyBulletMove();
        void EnemyBulletMove2(ref Bullet bullet);
        void EnemyDies(Enemy enemy);
        void PlayerDmg();
    }
}
