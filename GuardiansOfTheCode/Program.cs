using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace GuardiansOfTheCode
{
    class Program
    {
        static void Main(string[] args)
        {
            //PrimaryPlayer player = PrimaryPlayer.Instance;
            //Console.WriteLine($"{player.Name} - lvl {player.Level}");

            try
            {

                TestApiConnection().Wait();
                Gameboard board = new Gameboard();
                board.PlayArea(1);

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
    }
}
