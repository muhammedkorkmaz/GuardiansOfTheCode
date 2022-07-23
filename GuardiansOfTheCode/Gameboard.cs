using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Common;
using Newtonsoft.Json;

namespace GuardiansOfTheCode
{
    public class Gameboard
    {
        private PrimaryPlayer _player;
        private EnemyFactory _enemyFactory;

        public Gameboard()
        {
            _player = PrimaryPlayer.Instance;
            _player.Weapon = new Sword(12, 8);
        }

        public async Task PlayArea(int lvl)
        {
            _enemyFactory = new EnemyFactory(lvl);

            if(lvl == 1)
            {
                _player.Cards = (await FetchCards()).ToArray();

                Console.WriteLine("Ready to play level 1");
                Console.ReadKey();
                PlayFirstLevel();
            }
        }

        public void PlayFirstLevel()
        {
            const int currentLvl = 1;
            EnemyFactory factory = new EnemyFactory(currentLvl);
            List<IEnemy> enemies = new List<IEnemy>();
            for(int i=0; i<10; i++)
            {
                enemies.Add(factory.SpawnZombie(currentLvl));
            }

            for(int i=0; i< 3; i++)
            {
                enemies.Add(factory.SpawnWerewolf(currentLvl));
            }

            foreach (var enemy in enemies)
            {
                while(enemy.Health > 0 || _player.Health > 0)
                {
                    _player.Weapon.Use(enemy);
                    enemy.Attack(_player);
                }
            }
        }

        private async Task<IEnumerable<Card>> FetchCards()
        {
            using (var http=new HttpClient())
            {
                var cardsJson= await http.GetStringAsync("https://localhost:42296/api/Cards");

                return JsonConvert.DeserializeObject<IEnumerable<Card>>(cardsJson);
            }
        } 
    }
}
