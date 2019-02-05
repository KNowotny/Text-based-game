using DataAccess.Entities;
using System.Collections.Generic;

namespace DataAccess.Repos
{
    public interface IEnemyRepo
    {
        void Create(Enemy enemy);
        List<Enemy> GetAllEnemies();
        Enemy GetEnemyById(int id);
    }
}