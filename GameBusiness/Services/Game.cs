using DataAccess.Entities;
using DataAccess.Repos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameBusiness.Services
{
    public class GameService : IGame
    {
        private readonly IPlayerRepo _playerRepo;
        private readonly IEnemyRepo _enemyRepo;
        private readonly IArmorRepo _armorRepo;
        private readonly IWeaponRepo _weaponRepo;
        private readonly IItemRepo _itemRepo;
        private Enemy enemy = new Enemy();
        public GameService(IPlayerRepo playerRepo, IEnemyRepo enemyRepo, IArmorRepo armorRepo, IWeaponRepo weaponRepo, IItemRepo itemRepo)
        {
            _playerRepo = playerRepo;
            _enemyRepo = enemyRepo;
            _armorRepo = armorRepo;
            _weaponRepo = weaponRepo;
            _itemRepo = itemRepo;
        }

        public GameService(IPlayerRepo playerRepo)
        {
            _playerRepo = playerRepo;
        }

        public GameService(IPlayerRepo playerRepo, IEnemyRepo enemyRepo)
        {
            _playerRepo = playerRepo;
            _enemyRepo = enemyRepo;
        }

        public Player AcctualPlayer(int playerId)
        {
            var player = _playerRepo.GetAllPlayers().FirstOrDefault(m => m.Id == playerId);
            return player;
        }

        public string PlayerStatus(Player player)
        {
            string text = $"\n{player.PlayerName} Moc:{player.PlayerPower} Uzbrojenie:{player.Weapon.WeaponBuff} Pancerz:{player.Armor.ArmorBuff} HP:" +
                         $"{player.PlayerAcctualHealth}/{player.PlayerMaxHealth} Wynik:{player.Score}\n";

            return text;
        }

        public int FightOrIncident()
        {
            Random rnd = new Random();
            int randomEvent = rnd.Next(1, 11);
            return randomEvent;
        }

        public string PlayerChoiceFightOrEscape(Player player)
        {
            var allEnemies = _enemyRepo.GetAllEnemies();
            Random rnd = new Random();
            int randomEnemy = rnd.Next(0, allEnemies.Count);
            enemy = allEnemies[randomEnemy];
            enemy.AcctuallHealth = enemy.Health;
            string text = $"Spotkałeś {enemy.Name}! Moc:{enemy.Power}" +
                $" Życie:{enemy.Health} " +
                $"\nCo robisz? \n1.Walczę! \n2.Uciekam... \nWybór: ";
            return text;
        }

        public Enemy GetEnemy()
        {
            return enemy;
        }

        public void SaveToTxtFile(string text, string playerFile)
        {
            System.IO.File.AppendAllText($"{playerFile}", Environment.NewLine + text);
        }

        public string Fight(Player player, Enemy enemy)
        {
            var allEnemies = _enemyRepo.GetAllEnemies();
            string save = player.Flie, text = null;
            Random rnd = new Random();
            while (player.PlayerAcctualHealth > 0 && enemy.AcctuallHealth > 0)
            {
                int dmgGiven = rnd.Next(1, player.PlayerPower + player.Weapon.WeaponBuff);
                enemy.AcctuallHealth -= dmgGiven;
                text += $"\nAtakujesz za {dmgGiven}!   {enemy.Name}:" +
                    $" {enemy.AcctuallHealth}/{enemy.Health} HP" +
                 $"     {player.PlayerName}: {player.PlayerAcctualHealth}/{player.PlayerMaxHealth}HP";
                _playerRepo.Update(player);

                if (enemy.AcctuallHealth > 0)
                {
                    int dmgTakenWithoutArmor = rnd.Next(1, enemy.Power + 1);
                    int dmgTakenWithArmor = dmgTakenWithoutArmor - player.Armor.ArmorBuff;
                    if (dmgTakenWithArmor <= 0)
                    {
                        dmgTakenWithArmor = 1;
                        player.PlayerAcctualHealth -= dmgTakenWithArmor;
                        text += $"\nObrywasz za  {dmgTakenWithoutArmor}, ale dzięki zbroi dostjesz tylko 1 punkt obrażeń!   {enemy.Name}:" +
                                 $" {enemy.AcctuallHealth}/{enemy.Health} HP" +
                                $"     {player.PlayerName}: {player.PlayerAcctualHealth}/{player.PlayerMaxHealth}HP";
                    }
                    else
                    {
                        player.PlayerAcctualHealth -= dmgTakenWithArmor;
                        text += $"\nObrywasz za  {dmgTakenWithoutArmor}, ale dzięki zbroi dostajesz za {dmgTakenWithArmor}!   {enemy.Name}:" +
                            $" {enemy.AcctuallHealth}/{enemy.Health} HP" +
                        $"     {player.PlayerName}: {player.PlayerAcctualHealth}/{player.PlayerMaxHealth}HP";
                    }
                    _playerRepo.Update(player);
                }
            }
            if (player.PlayerAcctualHealth > 0)
            {
                text += $"\n{enemy.Name} nie żyje!. Zdobyłeś" +
                    $" {enemy.Power + enemy.Health} punktów!";
                player.Score += enemy.Power + enemy.Health;
                _playerRepo.Update(player);
            }
            if (player.PlayerAcctualHealth <= 0)
            {
                text += $"Zabił Cię {enemy.Name}";
            }

            return text;
        }

        public string Escape(Player player)
        {
            Random rnd = new Random();
            int dmgTaken = rnd.Next(1, enemy.Power + 1);
            dmgTaken -= player.Armor.ArmorBuff;
            if (dmgTaken <= 0)
            {
                dmgTaken = 1;
            }
            string text = $"\nUciekasz! Dostajesz {dmgTaken} obrażeń. ";
            player.PlayerAcctualHealth -= dmgTaken;
            _playerRepo.Update(player);

            return text;
        }

        public string PlayerDeath(Player player)
        {
            Console.Clear();
            string text = $"Umarłeś. Koniec gry. Zdobyłeś {player.Score} punktów.";
            _playerRepo.Update(player);

            return text;
        }

        public string Incident(Player player)
        {
            var allArmors = _armorRepo.GetAllArmors();
            var allItems = _itemRepo.GetAllItems();
            var allWeapon = _weaponRepo.GetAllWeapons();
            string text;

            Random rnd = new Random();
            int randomIncident = rnd.Next(3, 4);
            switch (randomIncident)
            {
                case 1:
                    int randomArmor = rnd.Next(1, allArmors.Count);
                    if (player.Armor.ArmorBuff >= allArmors[randomArmor].ArmorBuff)
                    {
                        text = "Znalazłeś zbroję, ale jest ona słabasza od Twojej. Zostawiasz ją.";
                        return text;
                    }
                    else
                    {
                        player.ArmorId = allArmors[randomArmor].Id;
                        _playerRepo.Update(player);
                        text = $"Znalazłeś zbroję: {allArmors[randomArmor].ArmorName}! Twój pancerz wynosi teraz: {player.Armor.ArmorBuff}.";
                        return text;
                    }
                case 2:
                    int randomWeapon = rnd.Next(1, allWeapon.Count);
                    if (player.Weapon.WeaponBuff >= allWeapon[randomWeapon].WeaponBuff)
                    {
                        text = "Znalazłeś broń, ale jest ona słabasza od Twojej. Zostawiasz ją.";
                        return text;
                    }
                    else
                    {
                        player.WeaponId = allWeapon[randomWeapon].Id;
                        _playerRepo.Update(player);
                        text = $"Znalazłeś broń: {allWeapon[randomWeapon].WeaponName}! Twoje uzbrojenie wynosi teraz: {player.Weapon.WeaponBuff}.";
                        return text;
                    }
                case 3:
                    int randomItem = rnd.Next(0, allItems.Count);
                    text = $"Znalazłeś: {allItems[randomItem].ItemName}! Zmienia Twoją moc o {allItems[randomItem].ItemPowerBuff}," +
                        $"a życie o {allItems[randomItem].ItemArmorBuff}!";
                    player.PlayerMaxHealth += allItems[randomItem].ItemArmorBuff;
                    if (player.PlayerMaxHealth < player.PlayerAcctualHealth)
                    {
                        player.PlayerAcctualHealth = player.PlayerMaxHealth;
                    }
                    player.PlayerPower += allItems[randomItem].ItemPowerBuff;
                    if (player.PlayerPower < 0)
                    {
                        player.PlayerPower = 0;
                    }
                    _playerRepo.Update(player);
                    return text;
            }

            return null;
        }
    }
}
