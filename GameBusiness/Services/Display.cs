using DataAccess;
using DataAccess.Entities;
using DataAccess.Repos;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameBusiness.Services
{
    public class Display : IDisplay
    {
        private readonly IPlayerRepo _playerRepo;

        public Display(IPlayerRepo playerRepo)
        {
            _playerRepo = playerRepo;
        }

        public void DisplayGame(int playerId)
        {
            if (playerId != 0)
            {
                Console.Clear();
                var player = _playerRepo.GetAllPlayers().FirstOrDefault(m => m.Id == playerId);

                using (StreamReader history = new StreamReader($@"{player.Flie}"))
                {
                    String line = history.ReadToEnd();
                    Console.WriteLine(line);
                }
                Console.ReadKey();
            }
        }

        public List<Player> DisplayPlayerWithoutFile()
        {
            Console.Clear();
            var list = _playerRepo.GetAllPlayers().OrderBy(x => x.Id).ToList();

            return list;
        }

        public Player DisplayGameHistory(int choice)
        {
            var list = _playerRepo.GetAllPlayers().OrderBy(x => x.Id).ToList();

            return list[choice - 1];
        }

        public List<Player> GetPlayerList()
        {
            Console.Clear();
            var list = _playerRepo.GetAllPlayers().OrderByDescending(x => x.Score).ToList();

            return list;
        }

        public Player GetPlayer(int playerId)
        {     
            Console.Clear();
            var player = _playerRepo.GetAllPlayers().FirstOrDefault(m => m.Id == playerId);
            return player;
        }

        public List<Player> DisplayAlivePlayer()
        {
            Console.Clear();
            var players = _playerRepo.GetAllPlayers().Where(m => m.PlayerAcctualHealth > 0).ToList();

            return players;
        }

        public int LoadPlayer(int choice)
        {
            if (choice == 0)
            {
                return 0;
            }
            Console.Clear();
            int number = 1, playerId = 0;
            var players = _playerRepo.GetAllPlayers().Where(m => m.PlayerAcctualHealth > 0).ToList();
            foreach (var item in players)
            {
                if (choice == number)
                {
                    playerId = item.Id;
                }
                number++;
            }

            return playerId;
        }
    }
}
