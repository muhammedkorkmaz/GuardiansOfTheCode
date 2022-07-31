using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Common;
using GuardiansOfTheCode.Adapters;
using GuardiansOfTheCode.Facades;
using MilkyWayponLib;
using Newtonsoft.Json;

namespace GuardiansOfTheCode
{
    public class Gameboard
    {
        private GameboardFacade _gameboardFacade;

        private PrimaryPlayer _player;

        public Gameboard()
        {
            _player = PrimaryPlayer.Instance;
            _gameboardFacade = new GameboardFacade();
        }

        public async Task PlayArea(int lvl)
        {
            if (lvl == -1)
            {
                Console.WriteLine("Play special level");
                Console.ReadKey();
                PlaySpecialLevel();
            }
            else
            {
                await _gameboardFacade.Play(_player, lvl);
            }
        }

        private void PlaySpecialLevel()
        {
            _player.Weapon = new WeaponAdapter(new Blaster(20, 15, 15));

        }
    }
}
