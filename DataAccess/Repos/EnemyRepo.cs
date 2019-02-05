using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repos
{
    public class EnemyRepo : IEnemyRepo
    {
        private readonly IEnemyRepo _enemyRepo;

        private Homework04DbContext _dbContext;
        public EnemyRepo(Homework04DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Create(Enemy enemy)
        {
            _dbContext.Enemies.Add(enemy);
            _dbContext.SaveChanges();
        }

        public List<Enemy> GetAllEnemies()
        {
            return _dbContext.Enemies.ToList();
        }

        public Enemy GetEnemyById(int id)
        {
            var enemy = _enemyRepo.GetAllEnemies().FirstOrDefault(m => m.Id == id);
            return enemy;
        }
    }
}
