using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Common;
using Newtonsoft.Json;

namespace GuardiansOfTheCode.Proxies
{
    public class CardsProxy
    {
        private HttpClient _http;
        private IEnumerable<Card> _cards;

        public CardsProxy()
        {
            _http = new HttpClient();
        }

        public async Task<IEnumerable<Card>> GetCards()
        {
            if (_cards!=null)
            {
                return _cards;
            }
            else
            {
                await FetchCards();
                return _cards;
            }
        }

        private async Task FetchCards()
        {
            var cardsJson = await _http.GetStringAsync("https://localhost:42296/api/Cards");

            _cards = JsonConvert.DeserializeObject<IEnumerable<Card>>(cardsJson).ToArray();
        }
    }
}

