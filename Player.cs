using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Media;
using System.Threading;


namespace Asst1
{
    public class Player : IHand
    {
       private string name;                                //Player Name
       private List<Card> hand = new List<Card>();         //cards in hand
       private int handVal = 0;                            //current value of hand
       private int numAces = 0;                            //number of aces in hand
       private int playerNum;                              //numbers palyers
       private float bet;                                  //player's current bet
       private float account;                              //player's money
       private bool lucky = false;                         //True if player gets blackjack

        public Player(int playerNum)                       //single arg constructor ( for quick game ), only assigns a player number
        {
            this.account = 100;
            this.playerNum = playerNum;
            if (playerNum == 0)
                this.name = "Dealer";
            else
                this.name = "Player" + Convert.ToString(playerNum);
        }

        public Player(int playerNum, string name, float account)       // 3 arg constructor ( for custom game ) allows player to set
        {                                                              // name and starting account
            this.account = account;
            this.playerNum = playerNum;
            if (playerNum == 0)
                this.name = "Dealer";
            else
                this.name = name;
        }

        //Properties for bet, account, name, lucky and hand

        public float Bet
        {
            get
            {
                return bet;
            }
            set
            {
                bet = value;
                account = account - bet;                          // removes bet amount from account
            }
        }

        public float Account
        {
            get
            {
                return account;
            }
            set
            {
                account = value;
            }
        }

        public string Name
        {
            get { return name; }
        }

        public bool Lucky                                       // true if hand was a blackjack
        {
            get { return lucky; }
            set { lucky = value; }
        }

        public void SetHandVal( int ace , int value )       // adds value of new deal to hand, keeps track of how many aces are 
        {                                                       // in the hand for calculation
            handVal = handVal + value;                
            numAces = numAces + ace;

            if (numAces > 0)
            {
                if (handVal > 21)
                {
                    handVal = handVal - 10;
                    numAces = numAces - 1;
                }
            }
         }

        public int ReadHandVal()
        {
            return handVal;
        }

        public void ClearHandVal()
        {
            handVal = 0;
        }

        public void AddHand(Card card)                            // Adds card to hand         
        {
            int v = card.Value();
            hand.Add(card);
            if (card.Rank==Ranks.Ace)
                this.SetHandVal(1, v);
            else
                this.SetHandVal(0, v);
        }

        public Card CheckHand(int i)                              //shows suite and rank of card in hand by index
        {
            return hand[i];
        }

        public void ClearHand()                                   // deletes all cards from hand
        {
            hand.Clear();
            ClearHandVal();
        }

        public void PrintPlayer()                                 // prints all palyer stats (for debugging), left in for reference
        {                                                         // when viewing test document
            Console.WriteLine("{0} : ", name);
            Console.WriteLine(" hand value : {0}  ", handVal);
            Console.WriteLine(" aces : {0} ", numAces);
            Console.WriteLine(" Bet : {0:C} ", bet);
            Console.WriteLine(" account : {0:C} ", account);
            Console.WriteLine();
        }

        public void PrintHand()                                   // prints rank and suite of all cards in hand
        {
            int n = hand.Count();
            for (int k = n - 1; k >= 0; --k)
            {
                Console.WriteLine("{0}", hand[k].ToString());
                Console.Beep(200, 500); //Sound and delay to make "dealing" more realistic
                Thread.Sleep(500);
            }
        }
    }
}
