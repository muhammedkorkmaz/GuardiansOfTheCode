using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Common;
using GuardiansOfTheCode.Facades;

namespace GuardiansOfTheCode
{
    class Program
    {
        static void Main(string[] args)
        {
            PrimaryPlayer player = PrimaryPlayer.Instance;
            Console.WriteLine($"{player.Name} - lvl {player.Level}");

            try
            {

                TestApiConnection().Wait();
                GameboardFacade gameBoard = new GameboardFacade();
                gameBoard.Play(player,1).Wait();


                Console.ReadKey();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Failed to initialize game");
                Console.ReadKey();
            }
        }

        private static async Task TestApiConnection()
        {
            int maxAttempt = 20;

            //Interval in milisecond
            int attempInterval = 2000;

            using (var httpClient = new HttpClient())
            {
                for (int i = 0; i < maxAttempt; i++)
                {
                    try
                    {
                        var response = await httpClient.GetAsync("https://localhost:42296/api/Cards");

                        if (response.StatusCode == HttpStatusCode.OK)
                        {
                            return;
                        }
                    }
                    catch (Exception ex)
                    {

                    }
                    finally
                    {
                        Console.WriteLine("Lost connection to server. Attempting to re-connect");
                        Thread.Sleep(attempInterval);
                    }

                }

                throw new Exception("Failed to connect to server");
            }
        }

        private static void TestDecorator()
        {
            Card soldier = new Card("Soldier", 25, 20);

            soldier = new AttackDecorator(soldier, "Sword", 15);
            soldier = new AttackDecorator(soldier, "Amulet", 5);
            soldier = new DefenseDecorator(soldier, "Helmet", 10);
            soldier = new DefenseDecorator(soldier, "Heavy Armor", 45);

            Console.WriteLine($"Final Stats: {soldier.Name} - {soldier.Attack} / {soldier.Defense}");
        }

        private static void TestComposite()
        {
            CardDeck deck = new CardDeck();
            CardDeck attackDeck = new CardDeck();
            CardDeck defenseDeck = new CardDeck();

            attackDeck.Add(new Card("Basic Infantry Unit", 12, 15));
            attackDeck.Add(new Card("Advanced Infantry Unit", 25, 18));
            attackDeck.Add(new Card("Cavarly Unit", 32, 24));

            defenseDeck.Add(new Card("Wooden Shield", 0, 6));
            defenseDeck.Add(new Card("Iron Shield", 0, 9));
            defenseDeck.Add(new Card("Shining Royal Armor", 0, 40));

            deck.Add(attackDeck);
            deck.Add(new Card("Small Beast", 16, 3));
            deck.Add(new Card("High Elf Rogue", 22, 7));
            deck.Add(defenseDeck);

            Console.WriteLine(deck.Display());

        }
    }
}
