using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Asst1
{
    public enum Suits { Club, Diamond, Heart, Spade }
    public enum Ranks 
    {
        Ace, Two, Three, Four, Five, Six, Seven, Eight, Nine, Ten,
        Jack, Queen, King
    }
    public class Card : IValue
    {
        private Suits suit;
        private Ranks rank;

        public Suits Suit //Returns suit; card's suit cannot be changed after construction, therefore read-only
        {
            get { return suit; }
        }

        public Ranks Rank //Returns rank; card's rank cannot be changed after construction, therefore read-only
        {
            get { return rank; }
        }

        public Card(Suits newsuit, Ranks newrank) //Only one constructor, since rank and suit can't be added after construction
        {
            suit = newsuit;
            rank = newrank;
        }

        public int Value() //Returns value of card. Note that Ace's value is set to 11.
        {
            if (Rank == Ranks.Ace)
            {
                return 11;
            }

            else if ((Rank == Ranks.Jack)|(Rank==Ranks.Queen)|(Rank==Ranks.King))
            {
                return 10;
            }

            else return (int)Rank + 1;
        }

        public int CompareTo(object obj) // To compare, first compare rank, then if equal, compare suit
        {
            Card that = (Card)obj;
            if (this.Rank < that.Rank) return -1;
            else if (this.Rank > that.Rank) return 1;
            else if (this.Suit < that.Suit) return -1;
            else if (this.Suit > that.Suit) return 1;
            else return 0;
        }

        public override string ToString() // Returns suit symbol, followed by rank
        {
            if (Suit == Suits.Club) return "\u2663:" + Rank;
            else if (Suit == Suits.Diamond) return "\u2666:" + Rank;
            else if (Suit == Suits.Heart) return "\u2665:" + Rank;
            else return "\u2660:" + Rank;
        }
    }


}
