using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardGameUIR
{
    internal class Program
    {
        static void Main(string[] args)
        {
            BlackJackDeck deck = new BlackJackDeck();

            var hand = deck.DealCard();

            foreach (var card in hand)
            {
                Console.WriteLine($"{card.Suite.ToString()} {card.Value.ToString()}");
            }
            
            Console.ReadLine();
        }
    }

    public abstract class Deck
    {
        protected List<PlayingCardModel> fullDeck = new List<PlayingCardModel>();
        protected List<PlayingCardModel> drawPile = new List<PlayingCardModel>();
        protected List<PlayingCardModel> discardPile = new List<PlayingCardModel>();


        protected void CreateDeck()
        {
            fullDeck.Clear();

            for (int suit = 0; suit < 4; suit++)
            {
                for (int val = 0; val < 13; val++)
                {
                    fullDeck.Add(new PlayingCardModel { Suite = (CardSuit)suit, Value = (CardValue)val });
                }
            }
        }

        public virtual void ShuffleDeck()
        {
            var rand = new Random();
            drawPile = fullDeck.OrderBy(x => rand.Next()).ToList();
        }


        public abstract List<PlayingCardModel> DealCard();
        protected virtual PlayingCardModel DrawOneCard()
        {
            PlayingCardModel output = drawPile.Take(1).First();
            drawPile.Remove(output);
            return output;
        }
    }

    public class PokerDeck : Deck
    {
        public PokerDeck()
        {
            CreateDeck();
            ShuffleDeck();
        }
        public override List<PlayingCardModel> DealCard()
        {
            List<PlayingCardModel> output = new List<PlayingCardModel>();
            for (int i = 0; i < 5; i++)
            {
                output.Add(DrawOneCard());
            }

            return output;
        }

        public List<PlayingCardModel> RequestCards(List<PlayingCardModel> cardsToDiscard)
        {
            List<PlayingCardModel> output = new List<PlayingCardModel>();

            foreach (var card in cardsToDiscard)
            {
                output.Add(DrawOneCard());
                discardPile.Add(card);
            }
            return output;
        }
    }

    public class BlackJackDeck : Deck
    {
        public BlackJackDeck()
        {
            CreateDeck();
            ShuffleDeck();
        }
        public override List<PlayingCardModel> DealCard()
        {
            List<PlayingCardModel> output = new List<PlayingCardModel>();
            for (int i = 0; i < 2; i++)
            {
                output.Add(DrawOneCard());
            }

            return output;
        }

        public PlayingCardModel RequestCard()
        {
            return DrawOneCard();
        }
    }
}
