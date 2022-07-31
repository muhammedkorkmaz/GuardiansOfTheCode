using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Common;
using GuardiansOfTheCode.Observer;
using GuardiansOfTheCode.Proxies;
using GuardiansOfTheCode.Strategies;
using Newtonsoft.Json;

namespace GuardiansOfTheCode.Facades
{
    public class GameboardFacade
    {

        PrimaryPlayer _player;
        private int _areaLEvel;
        private HttpClient _http;
        private EnemyFactory _factory;
        private List<IEnemy> _enemies = new List<IEnemy>();

        private CardsProxy _cardsProxy;

        public GameboardFacade()
        {
            _cardsProxy = new CardsProxy();
        }

        public async Task Play(PrimaryPlayer player, int areaLevel)
        {
            _player = player;
            _areaLEvel = areaLevel;

            ConfigurePlayerWeapon();
            await AddPlayerCards();
            InitializeEnemyFactory(areaLevel);
            LoadZombies(areaLevel);
            LoadWerevolves(areaLevel);
            LoadGiants(areaLevel);
            //Begin playing logic
            StartTurns();
        }

        private void ConfigurePlayerWeapon()
        {
            string input;
            int choice;
            IWeapon weapon;

            while (true)
            {
                Console.WriteLine("Picka weapon");
                Console.WriteLine("1. Sword");
                Console.WriteLine("2. Fire Staff");
                Console.WriteLine("3. Ice Staff");

                input = Console.ReadLine();

                if (int.TryParse(input, out choice))
                {
                    if (choice == 1)
                    {
                        _player.Weapon = new Sword(15, 7);
                        break;
                    }
                    else if (choice == 2)
                    {
                        _player.Weapon = new FireStaff(12, 14);
                        break;
                    }
                    else if (choice == 3)
                    {
                        _player.Weapon = new IceStaff(5, 1);
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Invalid input");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid input");
                }
            }
        }

        private async Task AddPlayerCards()
        {

            _player.Cards = (await _cardsProxy.GetCards()).ToArray();
        }

        private void InitializeEnemyFactory(int areaLevel)
        {
            _factory = new EnemyFactory(areaLevel);
        }

        private void LoadZombies(int areaLevel)
        {
            int count;
            if (areaLevel < 3)
            {
                count = 10;
            }
            else if (areaLevel < 10)
            {
                count = 20;
            }
            else
            {
                count = 30;
            }

            for (int i = 0; i < count; i++)
            {
                _enemies.Add(_factory.SpawnZombie(areaLevel));
            }
        }

        private void LoadWerevolves(int areaLevel)
        {
            int count;
            if (areaLevel < 5)
            {
                count = 3;
            }
            else
            {
                count = 10;
            }

            for (int i = 0; i < count; i++)
            {
                _enemies.Add(_factory.SpawnWerewolf(areaLevel));
            }
        }

        private void LoadGiants(int areaLevel)
        {
            int count;
            if (areaLevel < 8)
            {
                count = 1;
            }
            else
            {
                count = 3;
            }

            for (int i = 0; i < count; i++)
            {
                _enemies.Add(_factory.SpawnGiant(areaLevel));
            }
        }

        private void StartTurns()
        {
            IEnemy currentEnemy = null;
            var regularObserver = new HealthChangedObserver(new RegularDamageIndicator());
            var criticalObserver = new HealthChangedObserver(new CriticalHealthIndicator());
            regularObserver.WatchPlayerHealth(_player);
            criticalObserver.WatchPlayerHealth(_player);

            while (true)
            {
                if (currentEnemy == null)
                {
                    if (_enemies.Count > 0)
                    {
                        currentEnemy = _enemies[0];
                        _enemies.RemoveAt(0);
                    }
                    else
                    {
                        Console.WriteLine("You won this level");
                        break;
                    }
                }

                //// Your turn
                //_player.Weapon.Use(currentEnemy);
                //// Enemy's turn
                //currentEnemy.Attack(_player);

                int damage = currentEnemy.Attack(_player);
                _player.Hit(damage);

                Thread.Sleep(500);
            }
        }
    }
}

