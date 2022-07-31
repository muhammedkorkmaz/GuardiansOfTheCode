using System;
using System.Collections.Generic;
using Common;

namespace Api.Services
{
    public class CardsService : ICardsService
    {
        public CardsService()
        {
        }

        public IEnumerable<Card> FetchCards()
        {
            return new List<Card>()
            {
                new Card("Ultimate Shadow Wraith",90,80),
                new Card("Puppet of Doom" , 65,88),
                new Card("Lost Soul"  , 65,88),
                new Card("Plague Droid" , 65,88),
                new Card("Rage Dragon" , 65,88)
            };
        }
    }
}

