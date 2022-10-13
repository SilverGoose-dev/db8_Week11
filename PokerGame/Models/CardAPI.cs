namespace PokerGame.Models
{
	public class CardResponse
	{
		public bool success { get; set; }
		public string deck_id { get; set; }
		// WE copied this class over but
		// changed the second class to APICard.
		// So change it in this list as well!
		public List<APICard> cards { get; set; }
		public int remaining { get; set; }
	}


	public class APICard
	{
		public string code { get; set; }
		public string image { get; set; }
		public string value { get; set; }
		public string suit { get; set; }
	}

	public class CardAPI
    {

		//
		// CardAPI Functionality
		// GetNewDeck() - return a Deck ID
		// GetHand(deck_id, count) - return a single Hand instance
		//	with the Card list all populated






		public static HttpClient _web = null;

		public static HttpClient GetHttpClient()
		{
			if (_web == null)
			{
				_web = new HttpClient();
				_web.BaseAddress = new Uri("https://deckofcardsapi.com/api/deck/");
			}
			return _web;
		}

		async public static Task<string> GetNewDeck()
		{
			HttpClient web = GetHttpClient();

			var connection = await web.GetAsync($"new/shuffle/?deck_count=1");
			CardResponse deck = await connection.Content.ReadAsAsync<CardResponse>();

			return deck.deck_id;
		}

		async public static Task<Hand> GetHand(string deck_id, int count)
        {
			HttpClient web = GetHttpClient();
			var connection = await web.GetAsync($"{deck_id}/draw/?count={count}");
			CardResponse deck = await connection.Content.ReadAsAsync<CardResponse>();

			Hand newHand = new Hand();
			foreach (APICard apicard in deck.cards)
            {
				Card newCard = new Card();
				newCard.Image = apicard.image;
				newCard.Suit = apicard.suit.Substring(0, 1);
				int cardValue = 0;
				bool worked = int.TryParse(apicard.value, out cardValue);
				if(!worked)
                {
					if(apicard.value == "JACK")
                    {
						cardValue = 11;
                    }
					else if(apicard.value == "QUEEN")
                    {
						cardValue = 12;
                    }
					else if(apicard.value == "KING")
                    {
						cardValue = 13;
                    }
					else if(apicard.value == "ACE")
                    {
						cardValue = 14;
                    }
                }
				newCard.Rank = cardValue;
				newHand.Cards.Add(newCard);
            }
			return newHand;

		}
	}
}
