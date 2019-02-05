using System.Collections.Generic;
using DataAccess.Entities;

namespace DataAccess.Repos
{
    public interface IPlayerRepo
    {
        void Create(Player player);
        List<Player> GetAllPlayers();
        Player GetPlayerById(int id);
        void Update(Player player);
    }
}