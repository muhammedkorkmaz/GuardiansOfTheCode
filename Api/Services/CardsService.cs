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
                new Card(){Attack=90, Defense=80, Name="Ultimate Shadow Wraith" },
                new Card(){Attack=65, Defense=88, Name="Puppet of Doom" },
                new Card(){Attack=77, Defense=77, Name="Lost Soul" },
                new Card(){Attack=55, Defense=66, Name="Plague Droid" },
                new Card(){Attack=90, Defense=88, Name="Rage Dragon" }
            };
        }
    }
}

