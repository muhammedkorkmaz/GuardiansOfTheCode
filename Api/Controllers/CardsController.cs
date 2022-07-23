using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Services;
using Common;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Api.Controllers
{
    [Produces("application/json")]
    [Route("api/Cards")]
    public class CardsController : Controller
    {
        private ICardsService _cardsService;

        public CardsController(ICardsService service)
        {
            _cardsService = service;
        }

        [HttpGet("")]
        public IEnumerable<Card> GetAll()
        {
            return _cardsService.FetchCards();
        }

    }
}

