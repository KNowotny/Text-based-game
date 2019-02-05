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
    public class PlayerService : IPlayerService
    {
        private readonly IPlayerRepo _playerRepo;
        private readonly IEnemyRepo _enemyRepo;
        private readonly IArmorRepo _armorRepo;
        private readonly IWeaponRepo _weaponRepo;
        private readonly IItemRepo _itemRepo;

        public PlayerService(IPlayerRepo playerRepo, IEnemyRepo enemyRepo, IArmorRepo armorRepo, IWeaponRepo weaponRepo, IItemRepo itemRepo)
        {
            _playerRepo = playerRepo;
            _enemyRepo = enemyRepo;
            _armorRepo = armorRepo;
            _weaponRepo = weaponRepo;
            _itemRepo = itemRepo;
        }

        public bool CheckPlayerHealth(Player player)
        {
            if (player.PlayerAcctualHealth == 0 )
            {
                return false;
            }
            if (player.PlayerAcctualHealth > 0)
            {
                return true;
            }
            return false;
        }

        public string FindFile(string playerName)
        {
            int number = 1;
            string saveFile;
            do
            {
                saveFile = $@"{Directory.GetCurrentDirectory()}\Files\{playerName}_{number}.txt";
                if (File.Exists(saveFile))
                {
                    number++;
                }
                else
                {
                    number = 0;
                }

            } while (number != 0);

            return saveFile;
        }

        public int AddPlayer(string name, int power, int health)
        {
            var allArmors = _armorRepo.GetAllArmors();
            var allWeapon = _weaponRepo.GetAllWeapons();
            Player newPlayer = new Player();
            newPlayer.PlayerName = name;
            newPlayer.PlayerPower = power;
            newPlayer.PlayerMaxHealth = health;
            newPlayer.PlayerAcctualHealth = health;
            newPlayer.Score = 0;
            newPlayer.Date = DateTime.Now;
            newPlayer.Flie = FindFile(name);
            newPlayer.ArmorId = allArmors[0].Id;
            newPlayer.WeaponId = allWeapon[0].Id;
            _playerRepo.Create(newPlayer);
            return newPlayer.Id;
        }

        public bool AddEnemy(string name, int power, int health)
        {
            Enemy newEnemy = new Enemy();
            newEnemy.Name = name;
            newEnemy.Power = power;
            newEnemy.Health = health;
            newEnemy.AcctuallHealth = health;
            try
            {
                _enemyRepo.Create(newEnemy);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public void AddFolder()
        {
            string fileFolder = $@"{Directory.GetCurrentDirectory()}\Files";

            if (!Directory.Exists(fileFolder))
            {
                DirectoryInfo di = Directory.CreateDirectory(fileFolder);
            }
        }

        public void AddEnemies()
        {
            var enemyList = _enemyRepo.GetAllEnemies();
            var enemy = _enemyRepo.GetAllEnemies().FirstOrDefault(m => m.Name == "Wilk");

            if (enemy == null)
            {
                Enemy newEnemy = new Enemy()
                {
                    Name = "Wilk",
                    Health = 10,
                    Power = 5
                };
                _enemyRepo.Create(newEnemy);

                newEnemy = new Enemy()
                {
                    Name = "Ogr",
                    Health = 40,
                    Power = 15
                };
                _enemyRepo.Create(newEnemy);

                newEnemy = new Enemy()
                {
                    Name = "Goblin",
                    Health = 15,
                    Power = 15
                };
                _enemyRepo.Create(newEnemy);

                newEnemy = new Enemy()
                {
                    Name = "Minotaur",
                    Health = 30,
                    Power = 30
                };
                _enemyRepo.Create(newEnemy);

                newEnemy = new Enemy()
                {
                    Name = "Smok",
                    Health = 100,
                    Power = 50
                };
                _enemyRepo.Create(newEnemy);

                newEnemy = new Enemy()
                {
                    Name = "Elf",
                    Health = 25,
                    Power = 25
                };
                _enemyRepo.Create(newEnemy);

                newEnemy = new Enemy()
                {
                    Name = "Golem",
                    Health = 100,
                    Power = 10
                };
                _enemyRepo.Create(newEnemy);

                newEnemy = new Enemy()
                {
                    Name = "Chłop",
                    Health = 5,
                    Power = 5
                };
                _enemyRepo.Create(newEnemy);

                newEnemy = new Enemy()
                {
                    Name = "Gryf",
                    Health = 35,
                    Power = 50
                };
                _enemyRepo.Create(newEnemy);

                newEnemy = new Enemy()
                {
                    Name = "Wampir",
                    Health = 40,
                    Power = 25
                };
                _enemyRepo.Create(newEnemy);
            }
        }

        public void AddArmors()
        {
            var armorsList = _armorRepo.GetAllArmors();

            if (armorsList.Count < 6)
            {
                var newArmor = new Armor();
                newArmor = new Armor()
                {
                    ArmorName = "Zwyczajna koszula",
                    ArmorBuff = 1
                };
                _armorRepo.Create(newArmor);
                newArmor = new Armor()
                {
                    ArmorName = "Porządne ubranie",
                    ArmorBuff = 3
                };
                _armorRepo.Create(newArmor);
                newArmor = new Armor()
                {
                    ArmorName = "Lekka zbroja",
                    ArmorBuff = 5
                };
                _armorRepo.Create(newArmor);
                newArmor = new Armor()
                {
                    ArmorName = "Ludzka zbroja",
                    ArmorBuff = 7
                };
                _armorRepo.Create(newArmor);
                newArmor = new Armor()
                {
                    ArmorName = "Cięzka zbroja",
                    ArmorBuff = 10
                };
                _armorRepo.Create(newArmor);
                newArmor = new Armor()
                {
                    ArmorName = "Krasnoludzka zbroja",
                    ArmorBuff = 13
                };
                _armorRepo.Create(newArmor);
                newArmor = new Armor()
                {
                    ArmorName = "Elficka zbroja",
                    ArmorBuff = 15
                };
                _armorRepo.Create(newArmor);
            }
        }

        public void AddItems()
        {
            var itemList = _itemRepo.GetAllItems();

            if (itemList.Count < 5)
            {
                var newItem = new Item();
                newItem = new Item()
                {
                    ItemName = "Pierścień potęgi",
                    ItemArmorBuff = 3,
                    ItemPowerBuff = 3
                };
                _itemRepo.Create(newItem);
                newItem = new Item()
                {
                    ItemName = "Przeklęty pierścień",
                    ItemArmorBuff = -3,
                    ItemPowerBuff = -3
                };
                _itemRepo.Create(newItem);
                newItem = new Item()
                {
                    ItemName = "Ukryte ostrze",
                    ItemArmorBuff = 0,
                    ItemPowerBuff = 2
                };
                _itemRepo.Create(newItem);
                newItem = new Item()
                {
                    ItemName = "Kolczuga",
                    ItemArmorBuff = 3,
                    ItemPowerBuff = 0
                };
                _itemRepo.Create(newItem);
                newItem = new Item()
                {
                    ItemName = "Amulet aury ochronnej",
                    ItemArmorBuff = 2,
                    ItemPowerBuff = 0
                };
                _itemRepo.Create(newItem);
                newItem = new Item()
                {
                    ItemName = "Amulet siły",
                    ItemArmorBuff = 0,
                    ItemPowerBuff = 3
                };
                _itemRepo.Create(newItem);

            }
        }

        public void AddWeapons()
        {
            var weaponsList = _weaponRepo.GetAllWeapons();

            if (weaponsList.Count < 5)
            {
                var newWeapon = new Weapon();
                newWeapon = new Weapon()
                {
                    WeaponName = "Pięści",
                    WeaponBuff = 1
                };
                newWeapon = new Weapon()
                {
                    WeaponName = "Sztylet",
                    WeaponBuff = 3
                };
                _weaponRepo.Create(newWeapon);
                newWeapon = new Weapon()
                {
                    WeaponName = "Drewniany mieczyk",
                    WeaponBuff = 2
                };
                _weaponRepo.Create(newWeapon);
                newWeapon = new Weapon()
                {
                    WeaponName = "Krótki miecz",
                    WeaponBuff = 5
                };
                _weaponRepo.Create(newWeapon);
                newWeapon = new Weapon()
                {
                    WeaponName = "Długi miecz",
                    WeaponBuff = 7
                };
                _weaponRepo.Create(newWeapon);
                newWeapon = new Weapon()
                {
                    WeaponName = "Krasnoludzki topór",
                    WeaponBuff = 10
                };
                _weaponRepo.Create(newWeapon);
                newWeapon = new Weapon()
                {
                    WeaponName = "Elficka szabla",
                    WeaponBuff = 15
                };
                _weaponRepo.Create(newWeapon);
            }
        }

        public bool CheckEnemyExist(string enemyName)
        {
            var enemy = _enemyRepo.GetAllEnemies().FirstOrDefault(m => m.Name == enemyName);
            if (enemy == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
