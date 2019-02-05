using DataAccess.Entities;
using System.Collections.Generic;

namespace GameBusiness.Services
{
    public interface IDisplay
    {
        List<Player> DisplayAlivePlayer();
        List<Player> GetPlayerList();
        void DisplayGame(int playerId);
        Player DisplayGameHistory(int choice);
        Player GetPlayer(int playerId);
        List<Player> DisplayPlayerWithoutFile();
        int LoadPlayer(int choice);
    }
}