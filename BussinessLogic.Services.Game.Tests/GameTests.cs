using DataAccess.Entities;
using DataAccess.Repos;
using GameBusiness.Services;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLogic.Services.Game.Tests
{
    [TestFixture]
    public class GameTests
    {

        [Test]
        public void Return_text_when_player_meets_enemy()
        {
            Player player = new Player();
            var playerRepo = new Mock<IPlayerRepo>();
            Enemy enemy = new Enemy();
            List<Enemy> enemies = new List<Enemy>();
            var enemyRepo = new Mock<IEnemyRepo>();

            enemies.Add(enemy);
            enemyRepo.Setup(x => x.GetAllEnemies()).Returns(enemies);
            enemyRepo.Setup(x => x.GetEnemyById(It.IsAny<int>())).Returns(enemy);
            
            var gameService = new GameService(playerRepo.Object, enemyRepo.Object);

            string expectedText = $"Spotkałeś {enemy.Name}! Moc:{enemy.Power}" +
                             $" Życie:{enemy.Health} " +
                            $"\nCo robisz? \n1.Walczę! \n2.Uciekam... \nWybór: ";
            string resultText = gameService.PlayerChoiceFightOrEscape(player);

            Assert.AreEqual(resultText, expectedText);
        }

        [Test]
        public void Return_text_when_cheek_player_status()
        {
            Weapon weapon = new Weapon()
            {
                WeaponBuff = 3,
            };
            Armor armor = new Armor()
            {
                ArmorBuff = 3,
            };
            Player player = new Player()
            {
                PlayerName = "ads",
                PlayerMaxHealth = 3,
                PlayerAcctualHealth = 3,
                PlayerPower = 3,
                Score = 3,
                Weapon = weapon,
                Armor = armor
            };
            var playerRepo = new Mock<IPlayerRepo>();

            var gameService = new GameService(playerRepo.Object);

            string expectedText = $"\n{player.PlayerName} Moc:{player.PlayerPower} Uzbrojenie:{player.Weapon.WeaponBuff} Pancerz:{player.Armor.ArmorBuff} HP:" +
                       $"{player.PlayerAcctualHealth}/{player.PlayerMaxHealth} Wynik:{player.Score}\n";
            string resultText = gameService.PlayerStatus(player);

            Assert.AreEqual(expectedText, resultText);
        }

        [Test]
        public void Return_text_when_player_escaped_fight()
        {
            Armor armor = new Armor()
            {
                ArmorBuff = 3
            };
            Player player = new Player()
            {
                Armor = armor
            };
            var playerRepo = new Mock<IPlayerRepo>();
            Enemy enemy = new Enemy()
            {
                Power = 3
            };
            Random rnd = new Random();
            int dmgTaken = rnd.Next(1, enemy.Power + 1);
            dmgTaken -= player.Armor.ArmorBuff;
            if (dmgTaken <= 0)
            {
                dmgTaken = 1;
            }

            var gameService = new GameService(playerRepo.Object);

            string expectedText = $"\nUciekasz! Dostajesz {dmgTaken} obrażeń. ";
            string resultText = gameService.Escape(player);

            Assert.AreEqual(expectedText, resultText);
        }

        [Test]
        public void Return_text_when_player_killed_enemy()
        {
            Weapon weapon = new Weapon()
            {
                WeaponBuff = 0
            };
            Armor armor = new Armor()
            {
                ArmorBuff = 0
            };
            Player player = new Player()
            {
                PlayerName = "player",
                PlayerMaxHealth = 5,
                PlayerAcctualHealth = 5,
                PlayerPower = 1,
                Weapon = weapon,
                Armor = armor
            };
            var playerRepo = new Mock<IPlayerRepo>();
            var enemyRepo = new Mock<IEnemyRepo>();
            List<Enemy> enemies = new List<Enemy>();
            Enemy enemy = new Enemy()
            {
                Name = "enemy",
                AcctuallHealth = 1,
                Health = 1,
                Power = 3
            };
            enemies.Add(enemy);

            enemyRepo.Setup(x => x.GetAllEnemies()).Returns(enemies);
            GameService gameService = new GameService(playerRepo.Object, enemyRepo.Object);
            Random rnd = new Random();
            int dmgGiven = rnd.Next(1, player.PlayerPower + player.Weapon.WeaponBuff);

            string expectedText = $"\nAtakujesz za {dmgGiven}!   {enemy.Name}:" +
                    $" {enemy.AcctuallHealth - dmgGiven}/{enemy.Health} HP" +
                 $"     {player.PlayerName}: {player.PlayerAcctualHealth}/{player.PlayerMaxHealth}HP" +
                 $"\n{enemy.Name} nie żyje!. Zdobyłeś" +
                    $" {enemy.Power + enemy.Health} punktów!";

            string resultText = gameService.Fight(player, enemy);

            Assert.AreEqual(expectedText, resultText);
        }

        [Test]
        public void Return_text_when_enemy_killed_player()
        {
            Weapon weapon = new Weapon()
            {
                WeaponBuff = 0
            };
            Armor armor = new Armor()
            {
                ArmorBuff = 0
            };
            Player player = new Player()
            {
                PlayerName = "player",
                PlayerMaxHealth = 1,
                PlayerAcctualHealth = 1,
                PlayerPower = 1,
                Weapon = weapon,
                Armor = armor
            };
            var playerRepo = new Mock<IPlayerRepo>();
            var enemyRepo = new Mock<IEnemyRepo>();
            List<Enemy> enemies = new List<Enemy>();
            Enemy enemy = new Enemy()
            {
                Name = "enemy",
                AcctuallHealth = 10,
                Health = 10,
                Power = 1
            };
            enemies.Add(enemy);

            enemyRepo.Setup(x => x.GetAllEnemies()).Returns(enemies);
            GameService gameService = new GameService(playerRepo.Object, enemyRepo.Object);
            Random rnd = new Random();
            int dmgGiven = 1;
            int dmgTakenWithoutArmor = 1;
            int dmgTakenWithArmor = 1;

            string expectedText = $"\nAtakujesz za {dmgGiven}!   {enemy.Name}:" +
                    $" {enemy.AcctuallHealth - dmgGiven}/{enemy.Health} HP" +
                 $"     {player.PlayerName}: {player.PlayerAcctualHealth}/{player.PlayerMaxHealth}HP" +
                 $"\nObrywasz za  {dmgTakenWithoutArmor}, ale dzięki zbroi dostajesz za {dmgTakenWithArmor}!   {enemy.Name}:" +
                        $" {enemy.AcctuallHealth - dmgGiven}/{enemy.Health} HP" +
                    $"     {player.PlayerName}: {player.PlayerAcctualHealth - dmgTakenWithArmor}/{player.PlayerMaxHealth}HP" +
                $"Zabił Cię {enemy.Name}";

            string resultText = gameService.Fight(player, enemy);

            Assert.AreEqual(expectedText, resultText);
        }

        [Test]
        public void Return_text_when_player_is_dead()
        {
            Player player = new Player()
            {
                Score = 5
            };
            var playerRepo = new Mock<IPlayerRepo>();

            GameService gameService = new GameService(playerRepo.Object);

            string expectedText = $"Umarłeś. Koniec gry. Zdobyłeś {player.Score} punktów.";
            string resultText = gameService.PlayerDeath(player);

            Assert.AreEqual(expectedText, resultText);
        }
    }
}
