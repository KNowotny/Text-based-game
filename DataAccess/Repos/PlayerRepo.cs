using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repos
{
    public class PlayerRepo : IPlayerRepo
    {
        private readonly IPlayerRepo _playerRepo;

        private Homework04DbContext _dbContext;
        public PlayerRepo(Homework04DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Create(Player player)
        {
            _dbContext.Players.Add(player);
            _dbContext.SaveChanges();
        }

        public List<Player> GetAllPlayers()
        {
            return _dbContext.Players.ToList();
        }

        public Player GetPlayerById(int id)
        {
            var player = _playerRepo.GetAllPlayers().FirstOrDefault(m => m.Id == id);
            return player;
        }

        public void Update(Player player)
        {
            var dbPlayer = _dbContext.Players.Find(player.Id);
            dbPlayer = player;
            _dbContext.SaveChanges();
        }
    }
}
