using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Asst1
{
    public class CardDeck : IDeck
    {
        public Card[] deck;
        private int topCard=0;
        private int maxSize;
        private int numDecks;

        public int TopCard //Read-only
        {
            get { return topCard; }
        }

        public int NumDecks //Setting automatically uses MultiDeck method to change the number of decks
        {
            get { return numDecks; }
            set { MultiDeck(value); }
        }

        public int MaxSize //Read-only
        {
            get { return maxSize; }
        }

        private void Construct(int n) //Constructs n decks
        {
            numDecks = n;
            maxSize = 52 * n;
            topCard = 0;

            deck = new Card[maxSize];
            for (int i = 0; i<n*52; i++)
            {
                deck[i] = new Card((Suits)(i%4), (Ranks)(i%13)); 
                //For each cycle through 52, it will pass through each combination of rank and suit
            }
        }

        public int Size()
        {
            return MaxSize-TopCard;
        }

        public bool Empty()
        {
            if (topCard == maxSize) return true;
            else return false;
        }

        public void MakeEmpty()
        {
            topCard = maxSize;
        }

        public CardDeck(int n) //To construct n decks
        {
            Construct(n);
        }

        public CardDeck()
        {
            Construct(1);
        }

        private void MultiDeck(int n) //Re-construts CardDeck with a new number of decks
        {
            Construct(n);
        }

        public void Sort() //Sorts cards in deck into order by rank, followed by suit
        {
            Array.Sort(deck);
        }

        public void GatherCards() //Gathers cards that have been dealt out
        {
            topCard=0;
        }

        public Card DealCard() //Returns top card, then increases topCard index
        {
            if (Empty()==true)//Reshuffles deck if deck is empty, after printing warning message
            {
                Console.WriteLine("Error: Deck Empty - Shuffling new deck");
                GatherCards();
                Shuffle();
            }

            return deck[topCard++];
        }

        public void Shuffle() //Randomizes card placement
        {
            Card[] tempDeck = new Card[maxSize]; //Temporary deck for storing cards in new places
            bool[] full = new bool[maxSize]; //Stores information about whether each position in the new deck is full
            Random num = new Random(); //Random number generator

            GatherCards(); //ensures the deck is "full" (re-initializes topCard)

            for (int i = 0; i < maxSize; i++ ) //initializes full markers as false
            {
                full[i] = false;
            }


            for (int i = 0; i < maxSize; i++)
            {
                //For each card, assigns a random number. If that place is empty, assigns card to that new place.
                //If that place is full, adds 7 (which is coprime to 52, and will therefore cycle through all values)
                //to the place until it finds one which is empty. Adding 7 rather than one avoids visible clustering 
                //while still allowing a much higher speed than continuous random number selection.
                bool complete = false;
                int place = num.Next(maxSize - 1);
                do
                {
                    if (full[place] == false)
                    {
                        tempDeck[place] = deck[i];
                        full[place] = true;
                        complete = true;
                    }
                    place=(place+7)%maxSize; 
                } while (complete == false);
            }
            //Once all cards have been assigned to the temporary deck, overwrite original deck with temporary deck
            deck = tempDeck;
        }
    }
}
