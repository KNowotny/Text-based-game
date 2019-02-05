using DataAccess.Entities;

namespace GameBusiness.Services
{
    public interface IGame
    {
        Player AcctualPlayer(int playerId);
        Enemy GetEnemy();
        string Escape(Player player);
        string Fight(Player player, Enemy enemy);
        string PlayerChoiceFightOrEscape(Player player);
        string PlayerDeath(Player player);
        string PlayerStatus(Player player);
    }
}