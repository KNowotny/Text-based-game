using DataAccess;
using DataAccess.Entities;
using GameBusiness;
using GameBusiness.Services;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            IKernel kernel = new StandardKernel(new DataAccessModule());
            kernel.Bind<IPlayerService>().To<PlayerService>();
            kernel.Bind<IDisplay>().To<Display>();
            kernel.Bind<IGame>().To<GameService>();
            var service = kernel.Get<PlayerService>();

            int playerId = 0, choice = 0, newPlayerChoice = 1, number = 0;
            bool checkHealth = false;
            Player player = kernel.Get<Player>();
            List<Player> playersList;
            string text;
            service.AddFolder();
            service.AddEnemies();
            service.AddArmors();
            service.AddWeapons();
            service.AddItems();
            do
            {
                var display = kernel.Get<Display>();
                var game = kernel.Get<GameService>();
                Console.Clear();
                player = display.GetPlayer(playerId);
                if (playerId != 0 && player.PlayerAcctualHealth > 0)
                {
                    Console.WriteLine($"{player.PlayerName}   Moc:{player.PlayerPower} Uzbrojenie:{player.Weapon.WeaponBuff} Pancerz:{player.Armor.ArmorBuff}  Życie:{player.PlayerAcctualHealth}/{player.PlayerMaxHealth} \n");
                }
                do
                {
                    Console.Write("1. Nowa gra. \n2. Wczytaj grę. \n3. Stwórz nową postać. \n4. Dodaj wroga. " +
                             "\n5. Top 10 wyników. \n6. Zobacz historię wybranej gry. \n7. Informacje. \n8. Wyjdź. \nWybór: ");
                    choice = CheckNumberTemplate.CheckIntNumber(Console.ReadLine(), 1, 8);
                } while (choice < 1 && choice > 8);

                switch (choice)
                {
                    case 1:
                        Console.Clear();
                        player = game.AcctualPlayer(playerId);
                        if (playerId == 0 || player.PlayerAcctualHealth <= 0)
                        {
                            Console.WriteLine("Musisz najpierw założyć postać!");
                            Console.ReadKey();
                        }
                        else
                        {
                            do
                            {
                                player = game.AcctualPlayer(playerId);
                                text = game.PlayerStatus(player);
                                Console.WriteLine(text);
                                game.SaveToTxtFile(text, player.Flie);
                                int whatsHappening = game.FightOrIncident();
                                if (whatsHappening <= 7)
                                {
                                    text = game.PlayerChoiceFightOrEscape(player);
                                    Console.Write(text);
                                    game.SaveToTxtFile(text, player.Flie);
                                    choice = CheckNumberTemplate.CheckIntNumber(Console.ReadLine(), 1, 2);
                                    switch (choice)
                                    {
                                        case 1:
                                            Enemy enemy = game.GetEnemy();
                                            text = game.Fight(player, enemy);
                                            Console.WriteLine(text);
                                            game.SaveToTxtFile(text, player.Flie);
                                            Console.ReadKey();
                                            break;
                                        case 2:
                                            text = game.Escape(player);
                                            Console.WriteLine(text);
                                            game.SaveToTxtFile(text, player.Flie);
                                            Console.ReadKey();
                                            break;
                                    }
                                }
                                else
                                {
                                    text = game.Incident(player);
                                    Console.WriteLine(text);
                                    game.SaveToTxtFile(text, player.Flie);
                                    Console.ReadKey();
                                }

                            } while (service.CheckPlayerHealth(player));
                            text = game.PlayerDeath(player);
                            Console.WriteLine(text);
                            game.SaveToTxtFile(text, player.Flie);
                            Console.ReadKey();
                        }
                        break;
                    case 2:
                        playersList = display.DisplayAlivePlayer();
                        number = 1;
                        foreach (var anyPlayer in playersList)
                        {
                            Console.WriteLine($"{number}. {anyPlayer.PlayerName} Życie: {anyPlayer.PlayerAcctualHealth}/{anyPlayer.PlayerMaxHealth} Wynik: {anyPlayer.Score}");
                            number++;
                        }
                        Console.Write("Wczytaj bohatera nr (0 - wróć do menu): ");
                        playerId = display.LoadPlayer(CheckNumberTemplate.CheckIntNumber(Console.ReadLine(), 0, playersList.Count));
                        Console.Clear();
                        break;
                    case 3:
                        Console.Clear();
                        player = game.AcctualPlayer(playerId);
                        if (playerId == 0)
                        {
                            Console.Write("Podaj swoje imię śmiałku: ");
                            string playerName = Console.ReadLine();
                            Console.WriteLine("Masz do wykorzystania 50 punktów na życie i moc.");
                            Console.Write("Ile masz mocy: ");
                            int playerPower = CheckNumberTemplate.CheckIntNumber(Console.ReadLine(), 1, 49);
                            Console.Write($"Ile masz życia (maksymalnie {50 - playerPower}): ");
                            int playerHealth = CheckNumberTemplate.CheckIntNumber(Console.ReadLine(), 1, 50 - playerPower);
                            playerId = service.AddPlayer(playerName, playerPower, playerHealth);
                            Console.WriteLine("Dodano gracza.");
                        }
                        else
                        {
                            checkHealth = service.CheckPlayerHealth(player);
                            if (checkHealth)
                            {
                                Console.Write("Twój bohater jeszcze żyje. Czy chcesz utworzyć nowego? (obecny zginie) \n1.Nowy! \n2.Stary! \nWybór: ");
                                newPlayerChoice = CheckNumberTemplate.CheckIntNumber(Console.ReadLine(), 1, 2);
                                Console.Clear();
                            }
                            if (newPlayerChoice == 1)
                            {
                                Console.Write("Podaj swoje imię śmiałku: ");
                                string playerName = Console.ReadLine();
                                Console.WriteLine("Masz do wykorzystania 50 punktów na życie i moc.");
                                Console.Write("Ile masz mocy: ");
                                int playerPower = CheckNumberTemplate.CheckIntNumber(Console.ReadLine(), 1, 49);
                                Console.Write($"Ile masz życia (maksymalnie {50 - playerPower}): ");
                                int playerHealth = CheckNumberTemplate.CheckIntNumber(Console.ReadLine(), 1, 50 - playerPower);
                                playerId = service.AddPlayer(playerName, playerPower, playerHealth);
                                Console.WriteLine("Dodano gracza.");
                            }
                        }                   
                        break;
                    case 4:
                        Console.Clear();
                        Console.Write("Podaj nazwę wroga: ");
                        string enemyName = Console.ReadLine();
                        bool alreadyExist = service.CheckEnemyExist(enemyName);
                        if (!alreadyExist)
                        {
                            Console.Write("Podaj moc wroga: ");
                            int enemyPower = CheckNumberTemplate.CheckIntNumber(Console.ReadLine(), 1, int.MaxValue);
                            Console.Write("Podaj życie wroga: ");
                            int enemyHealth = CheckNumberTemplate.CheckIntNumber(Console.ReadLine(), 1, int.MaxValue);
                            bool addedNewEnemy = service.AddEnemy(enemyName, enemyPower, enemyHealth);
                            if (addedNewEnemy == true)
                            {
                                Console.WriteLine("\nDodano nowego wroga");
                            }
                            else
                            {
                                Console.WriteLine("Błąd przy dodawaniu.");
                            }
                            Console.ReadKey();
                        }
                        else
                        {
                            Console.WriteLine("Wróg o takiej nazwie już istnieje.");
                            Console.ReadKey();
                        }
                        Console.Clear();
                        break;
                    case 5:
                        number = 1;
                        playersList = display.GetPlayerList();
                        foreach (var bestPlayer in playersList)
                        {
                            if (number <= 10)
                            {
                                Console.WriteLine($"{number}. {bestPlayer.PlayerName} - {bestPlayer.Score} punktów - {bestPlayer.Date}");
                                number++;
                            }
                            else
                            {
                                break;
                            }
                        }
                        if (number == 1)
                        {
                            Console.WriteLine("Brak historii gier.");
                        }
                        Console.Write("\n\nWciśnij dowolny przycisk aby wrócić do menu.");
                        Console.ReadKey();
                        break;
                    case 6:
                        Console.Clear();
                        number = 0;
                        playersList = display.DisplayPlayerWithoutFile();
                        foreach (var anyPlayer in playersList)
                        {
                            Console.WriteLine($"{anyPlayer.Id}. {anyPlayer.PlayerName} - {anyPlayer.Score} punktów - {anyPlayer.Date}");
                            number++;
                        }
                        if (number == 0)
                        {
                            Console.WriteLine("Brak historii gier. Wciśnij dowolny przycisk aby wrócić do menu");
                            Console.ReadLine();
                        }
                        else
                        {
                            Console.Write("\n\nTwój wybór (0 - powrót do menu): ");
                            playerId = CheckNumberTemplate.CheckIntNumber(Console.ReadLine(), 0, number + 1);
                            display.DisplayGame(playerId);
                        }
                        break;
                    case 7:
                        Console.Clear();
                        Console.WriteLine("1. Nowa gra - zaczynasz nową grę. Musisz mieć wybraną postać. \n" +
                            "2. Wczytanie gry - kontynuujesz grę po wyborze postaci z listy żyjących. \n" +
                            "3. Tworzenie postaci - podajesz imię, moc oraz życie postaci. Suma mocy i życia nie może przekraczać 50. \n" +
                            "4. Dodaj wroga - podajesz nazwę, moc oraz zycie wroga. Nazwa musi być unikatowa.\n" +
                            "5. Top 10 wyników - pokazuje 10 najlepszych wyników.\n" +
                            "6. Historia gry - wybierz spośród listy wszytskich gier taką, której przebieg chcesz zobaczyć w konsoli.\n" +
                            "7. Zasady walki - \n\t Atak - Gracz atakuje za minimum 1, a maksymalnie za tyle, ile wynosi jego moc. Jeśli wróg przeżył," +
                            "następuje jego atak za minimum 1, a maksymalnie za tyle, ile wynosi jego moc. Walka trwa dopóki jedna ze stron nie polegnie." +
                            "\n\t Ucieczka - gracz otrzymuje minimum 1 punkt obrażeń, a maksymalnie za tyle, ile wynosi moc wroga.");
                        Console.ReadKey();
                        break;
                    default:
                        break;
                }
            } while (choice != 8);
        }
    }
}
