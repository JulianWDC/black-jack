/*using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Asst1
{
    class Test
    {
        public static void Main()
        {
           
                int numPlay = 3;

                /*Console.WriteLine("Enter Starting Account Value: ");
                float accountStart = float.Parse(Console.ReadLine());
                Console.WriteLine("Player 1 enter Name");
                string firstPlayer = Console.ReadLine();
                Player start = new Player(1, firstPlayer, accountStart );
                Player dealer = new Player(0);
                PlayerNode last = new PlayerNode(start, null);
                PlayerNode first;
                CardDeck deck = new CardDeck();

              for (int k = 2; k <= numPlay; ++k)
                {
                    Player current = new Player(k);
                    PlayerNode node = new PlayerNode(current, last);
                    last = node;
                }

                first = last;
        
            last.Current.Bet = 50;
            last.Current.Account = last.Current.Account + last.Current.Bet;

                CustomPlayers(3);

            for (int l = numPlay; l > 0; --l)
            {
                
                Console.WriteLine("{0} : ", last.Current.Name);
                last.Current.PrintPlayer();
                last.Current.PrintHand();
                Console.WriteLine();
            }

            last = first;

           PlayerNode now = firstPlayer;

            for (int l = numPlayers; l > 0; --l)
            {

                //Console.WriteLine("{0} : ", firstPlayer.Current.Name);
               
                Card card = mainDeck.DealCard();
                now.Current.AddHand(card);
                card = mainDeck.DealCard();
                now.Current.AddHand(card);
                now.Current.PrintPlayer();
                now.Current.PrintHand();
                Console.WriteLine();
                now = now.Next;
            for (int l = numPlay; l > 0; --l)
            {
                Current.ClearHand();
                
                Console.WriteLine("{0} : ", last.Current.Name);
                last.Current.PrintPlayer();
                last.Current.PrintHand();
                Console.WriteLine();
            }

            last = first;
            Console.ReadKey();
        }

            public static void CustomPlayers( int numPlay )
           {
                Console.WriteLine("Please Enter Desired Starting Account Balance: ");
                float accountStart = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Player 1 enter name: ");
                string name1 = Console.ReadLine();
                Player start = new Player(1, name1, accountStart);
                Player dealer = new Player(0, "Dealer", accountStart);
                PlayerNode last = new PlayerNode(start, null);
                PlayerNode first;
                CardDeck deck = new CardDeck();

                for (int k = 2; k <= numPlay; ++k)
                {
                    Console.WriteLine("Player {0} enter name: ", k );
                    string name = Console.ReadLine();
                    Player current = new Player(k, name, accountStart);
                    PlayerNode node = new PlayerNode(current, last);
                    last = node;
                }

                first = last;
           }
        }         
    }*/


