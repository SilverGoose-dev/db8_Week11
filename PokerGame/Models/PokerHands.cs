namespace PokerGame.Models
{
    public class PokerHands
    {
        public Hand Player1 { get; set; }
        public Hand Player2 { get; set; } 
        public string DeckId { get; set; }
        public Hand Winner
        {
            get
            {
                if (Player1.Value > Player2.Value)
                {
                    return Player1;
                }
                else
                {
                    return Player2;
                }
            }
        }
    }


    public class Hand
    {
        public string Username { get; set; }
        public List<Card> Cards { get; set; } 
        public int Value
        {
            get 
            { 
               int max = 0;
                foreach (Card card in Cards)
                {
                    if (card.Value > max)
                    {
                        max = card.Value;
                    }
                }
                return max;
            }
        }

        public Hand()
        {
            Cards = new List<Card>();
        }

    }

    /*
     * Rankings
     *      Suits: Hearst = 4, Spades = 3, Diamonds = 2, Clubs = 1
     *      Cards will just have their 2 through 13 rank
     *      We'll multiply suit by 13 and add on the card 
     *      
     *      */

    public class Card
    {
        public string Suit { get; set; } // H, S, D, C
        public int Rank { get; set; } // 2,3,...10, J=10, Q=11, K=11, A=13
        public string Image { get; set; }
        public int Value
        {
            get
            {
                int suitValue = 0;
                if(Suit == "H")
                {
                    suitValue = 4;
                }
                else if(Suit == "S")
                {
                    suitValue=3;
                }
                else if(Suit == "D")
                {
                    suitValue=2;
                }
                else if(Suit == "C")
                {
                    suitValue=1;
                }
                return suitValue * 14 + Rank;
            }
        }


    }
}
