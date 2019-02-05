using DataAccess.Entities;

namespace GameBusiness.Services
{
    public interface IPlayerService
    {
        void AddItems();
        void AddWeapons();
        void AddEnemies();
        void AddArmors();
        bool AddEnemy(string name, int power, int health);
        void AddFolder();
        int AddPlayer(string name, int power, int health);
        bool CheckEnemyExist(string enemyName);
        bool CheckPlayerHealth(Player player);
        string FindFile(string playerName);
    }
}