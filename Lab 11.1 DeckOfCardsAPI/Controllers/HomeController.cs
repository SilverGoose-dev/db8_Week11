using Lab_11._1_DeckOfCardsAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Lab_11._1_DeckOfCardsAPI.Controllers
{
    public class HomeController : Controller
    {

        private readonly ILogger<HomeController> _logger;
        //static public string DeckId = "";       This worked at first but you only can have one deckID for all users 
        //Let's give each user their own deck. Instead of keeping a single static deck ID, we will pass it around through 
        //views and links
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        async public Task<IActionResult> Index(string deckid)
        {
            // Old code 
            //HttpClient web = new HttpClient();
            //web.BaseAddress = new Uri("https://deckofcardsapi.com/api/deck/");
            //var connection = await web.GetAsync($"new/shuffle/?deck_count=1");
            //CardResponse deck = await connection.Content.ReadAsAsync<CardResponse>();
            ////DeckId = deck.deck_id;  // We're not doing this after all 

            //connection = await web.GetAsync($"{deck.deck_id}/draw/?count=5");
            //deck = await connection.Content.ReadAsAsync<CardResponse>();
            ////Now grab the deck ID 

            //// Then do another API call
            //// connection = await web.GetAsync(  other URL )

            string deck_id = await CardAPI.GetNewDeck();
            CardResponse deck = await CardAPI.GetCards(deck_id, 5);

            return View(deck);
        }

        async public Task<IActionResult> DrawFive(string deck_id)
        {
            // Old Code
            //HttpClient web = new HttpClient();
            //web.BaseAddress = new Uri("https://deckofcardsapi.com/api/deck/");
            //var connection = await web.GetAsync($"{deck_id}/draw/?count=5");
            //CardResponse deck = await connection.Content.ReadAsAsync<CardResponse>();

            CardResponse deck = await CardAPI.GetCards(deck_id, 5);

            return View("index", deck);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}