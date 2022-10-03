using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Asst1
{
    public interface IContainer
    {
        void MakeEmpty();
        bool Empty();
        int Size();
    }

    public interface IDeck: IContainer
    {
        void Shuffle(); //Shuffles the deck of cards
        void Sort(); //Sorts the deck into ascending order, according to the cards' IComparable
        Card DealCard(); //Deals the top card of the deck
        void GatherCards(); //Returns all cards that have been dealt out to the deck
    }

    public interface IValue : IComparable
    {
        int Value(); //Returns the value of the item
    }

    public interface IGame
    {
        void QuickGame(); //Initializes a game with minimal customization
        void CustomGame(); //Initializes a game with maximum customization
        void PlayGame(); //Plays the game
    }

    public interface IHand
    {
        void AddHand(Card card); // adds cards to hand
        Card CheckHand(int i); //returns contents of hand by index
        void ClearHand(); //clears cards from hand
        void SetHandVal(int ace, int value); //adds new card value to hand value
        int ReadHandVal(); // returns hand value
        void ClearHandVal(); //resets hand value
    }

}
