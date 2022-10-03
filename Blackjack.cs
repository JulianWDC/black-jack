using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Media;
using System.Threading;

namespace Asst1
{
    class Blackjack : IGame
    {
        private PlayerNode firstPlayer; //Points to the first player
        private Player dealer; //Points to the dealer
        private CardDeck mainDeck; //The deck being used
        private int numPlayers; //The number of players
        private Exception bad = new Exception(); //to be thrown when needed

        public Blackjack(int num) //Creates a class for testing
        {
            numPlayers = num;
        }

        public Blackjack() //Interactive start game
        {
            Console.WriteLine("Welcome to Blackjack!");
            //Opening music
            Console.Beep(300, 500);
            Console.Beep(400, 500);
            Console.Beep(500, 500);
            Console.WriteLine("Please turn on your speakers.");
            Thread.Sleep(500);
            bool flag = false;
            do
            {
                try
                {
                    Console.Write("How many players? "); // input number of players
                    numPlayers = Convert.ToInt32(Console.ReadLine());
                    if ((numPlayers < 1) || (numPlayers > 20))
                    {
                        throw bad;
                    }
                    flag = true;
                }

                catch
                {
                    Console.WriteLine("Error, please enter a number between 1 and 20");
                }
            } while (flag == false);

            string input; //initialize string to ensure while loop will start
            flag = false;
            do // Choose quick game or custom game
            {
                try
                {
                    Console.Write("Input Q for Quick Game or C for Custom Game: ");
                    input = Console.ReadLine().ToUpper();

                    if (input == "Q")
                    {
                        flag = true;
                        QuickGame();
                    }
                    else if (input == "C")
                    {
                        flag = true;
                        CustomGame();
                    }
                    else
                    {
                        throw bad;
                    }
                }
                catch
                {
                    Console.WriteLine("Error, invalid input.");
                }
            } while (flag == false);
        }

        public void QuickGame() //Quickly initializes games
        {
            QuickPlayers(numPlayers);
            QuickDecks();
            PlayGame();
        }

        public void QuickDecks() //Creates a good number of decks for the number of players (minimum 1)
        {
            int numDecks = (numPlayers * 6) / 52 + 1;
            mainDeck = new CardDeck(numDecks);
        }

        public void QuickPlayers(int numPlay) //Quickly initializes players
        {
            Player start = new Player(1);
            dealer = new Player(0);
            PlayerNode last = new PlayerNode(start, null);

            for (int k = 2; k <= numPlay; ++k) //Initializes each player with name Player#
            {
                Player current = new Player(k);
                PlayerNode node = new PlayerNode(current, last);
                last = node;
            }

            firstPlayer = last;
        }

        public void CustomPlayers(int numPlay) //Allows customization of players
        {
            Console.WriteLine();
            float accountStart = 0;
            bool format = false;

            do
            {
                try
                {
                    do
                    {
                        Console.WriteLine("Please Enter Desired Starting Account Balance: "); //user enter starting account balances
                        accountStart = float.Parse(Console.ReadLine());
                        Console.WriteLine();
                        if (accountStart > 0)
                        {
                            if (accountStart > 100000)
                                Console.WriteLine("Please start with an account balance less than $10 000"); //Sets upper limit on account
                        }
                        else
                            Console.WriteLine("Please enter an account balance that is greater than zero");  //does not allow negative accounts
                    }
                    while (accountStart <= 0 || accountStart > 10000);
                    format = true;
                }
                catch (FormatException)
                {
                    Console.WriteLine("Please enter a number for account");
                }
            } while (format == false);

            Console.WriteLine();
            Console.WriteLine("Player 1 enter name: ");                            //player 1 choose name
            string name1 = Console.ReadLine();
            Console.WriteLine();

            Player start = new Player(1, name1, accountStart);                     //initalize player 1
            dealer = new Player(0, "Dealer", accountStart);                 //initialize dealer
            PlayerNode last = new PlayerNode(start, null);                         //set first node to player 1                                                      //keeps track of start of list

            for (int k = 2; k <= numPlay; ++k)
            {
                Console.WriteLine("Player {0} enter name: ", k);                  //next palyer on list enters name
                string name = Console.ReadLine();
                Console.WriteLine();
                Player current = new Player(k, name, accountStart);                //initializes player
                PlayerNode node = new PlayerNode(current, last);                   //sets node
                last = node;
            }

            firstPlayer = last;                                                          //changes first pointer
        }

        public void CustomGame() //Customizes game
        {
            int numDecks = 0;
            bool format = false;
            do
            {
                try
                {
                    CustomPlayers(numPlayers); //Customizes players
                    Console.Write("How many decks? (Input 0 for default) ");
                    numDecks = Convert.ToInt32(Console.ReadLine());
                    while (numDecks > 50) //Upper limit on decks
                    {
                        Console.Write("That's ridiculous. Please choose a reasonable number of decks: ");
                        Console.ReadLine();
                    }
                    format = true;
                }

                catch (FormatException)
                {
                    Console.WriteLine("Please enter a number for Decks");
                }
            } while (format == false);

            if (numDecks == 0) //Uses optimal number of decks
                QuickDecks();
            else
                mainDeck = new CardDeck(numDecks);

            PlayGame();//Starts playing of game
        }

        public void PlayGame() //Main gameplay algorithm
        {
            mainDeck.Shuffle(); //includes regathering cards
            PlayerNode nowPlaying = firstPlayer;
            while (nowPlaying != null) //Each player places a bet
            {
                Bet(nowPlaying.Current);
                nowPlaying = nowPlaying.Next;
            }

            nowPlaying = firstPlayer; //Reset to first player
            Console.WriteLine("Dealing initial hands.");
            SystemSounds.Asterisk.Play();
            Console.WriteLine();
            while (nowPlaying != null) //Deal initial hand to each player
            {
                DealHand(nowPlaying.Current); //Deals hand
                nowPlaying = nowPlaying.Next; //Cycles to next player
            }
            DealerHand(dealer); //Deals hand for dealer

            nowPlaying = firstPlayer; //Reset to first player
            while (nowPlaying != null) //Let each player play hand
            {
                PlayHand(nowPlaying.Current); //Play hand
                nowPlaying = nowPlaying.Next; //Cycle to next player
            }

            PlayDealer(); //Auto-plays dealer
            Console.WriteLine();

            nowPlaying = firstPlayer; //Reset to first player
            while (nowPlaying != null) //Calculates winnings
            {
                Winnings(nowPlaying.Current); //Adds winnings to account
                nowPlaying = nowPlaying.Next; //cycles to next player
            }

            firstPlayer = Eliminate(firstPlayer); //Sets first non-eliminated player to first player
            nowPlaying = firstPlayer;
            while (nowPlaying != null) //Cycles through and sets first non-eliminated player to each next player slot
            {
                nowPlaying.Next = Eliminate(nowPlaying.Next);
                nowPlaying = nowPlaying.Next;
            }
            if (firstPlayer != null)
            {
                Console.Write("Do you want to play again? Y/N "); //Checks for replay
                string replay = Console.ReadLine().ToUpper();
                while ((replay != "Y") && (replay != "N")) //Checks for correct input
                {
                    Console.Write("Invalid response. Do you want to play again? Y/N ");
                    replay = Console.ReadLine().ToUpper();
                }

                if (replay == "Y") //Replays game if requested
                {
                    Console.WriteLine();
                    PlayGame();
                }
                else
                {
                    Console.WriteLine("Game Over!"); // If no replay requested, game is over

                    Console.Beep(500, 500); //Game over music
                    Console.Beep(300, 500);
                    Console.Beep(400, 500);
                }
            }

            else
            {
                Console.WriteLine("Game Over!"); // If all players eliminated, game is over

                Console.Beep(500, 500); //Game over music
                Console.Beep(300, 500);
                Console.Beep(400, 500);
            }
        }

        public void Bet(Player p) //Lets player place a bet
        {
            Console.Write("{0}, please input your bet: ", p.Name); //Input bet
            bool flag = false;
            float currentBet = 0;
            do
            {
                try
                {
                    currentBet = float.Parse(Console.ReadLine());
                    if ((currentBet > p.Account) || (currentBet < 0)) //Check bet is valid
                        throw bad;
                    flag = true;
                }
                catch
                {
                    Console.Write("Sorry, invalid bet. You have ${0} in your account. Please try again: ", p.Account);
                }
            } while (flag == false);
            p.Bet = currentBet;
        }

        public void DealHand(Player p) //Clears hand then deals first hand to player
        {
            p.ClearHand(); //Clear hand
            p.AddHand(mainDeck.DealCard()); //Deals two cards
            p.AddHand(mainDeck.DealCard());
            if (p.ReadHandVal() == 21) //Checks for blackjack
                p.Lucky = true;
            else
                p.Lucky = false;
            Console.WriteLine(p.Name); //Prints name
            p.PrintHand(); //Prints hand
            Console.WriteLine();
        }

        public void DealerHand(Player p) //Deals initial hand for dealer
        {
            p.ClearHand(); //Clear hand
            Console.WriteLine(p.Name); //Write Dealer's name
            p.AddHand(mainDeck.DealCard()); //Add first card
            Console.Beep(200, 500); //Sound of card dealing
            Thread.Sleep(500); //Card dealing delay
            Console.WriteLine("Face Down"); //Print notice that first card is face down
            Card c = mainDeck.DealCard(); //Deal second card
            Console.Beep(200, 500); //Sound of card dealing
            Thread.Sleep(500); //Card dealing delay
            p.AddHand(c); //Add second card to hand
            Console.WriteLine(c.ToString()); //Print second card
            if (p.ReadHandVal() == 21) //Checks for blackjack
                p.Lucky = true;
            else
                p.Lucky = false;
            Console.WriteLine();
        }

        public void PlayHand(Player p) //Player can request cards as desired until bust.
        {
            Console.WriteLine("{0}'s Turn", p.Name); //Print hand
            p.PrintHand();
            string input;
            do //Continues to deal cards while user requests and not busted
            {
                do
                {
                    Console.Write("Do you want another card? Y/N "); //Check for user request
                    input = Console.ReadLine().ToUpper();
                } while ((input != "Y") && (input != "N"));
                if (input == "Y")
                {
                    p.Lucky = false; //Getting any extra cards invalidates a blackjack
                    p.AddHand(mainDeck.DealCard()); //Deals card
                    p.PrintHand(); // Prints new hand
                    if (p.ReadHandVal() > 21) //Checks for bust
                    {
                        Console.WriteLine("Bust!");
                        SystemSounds.Hand.Play(); //Plays unhappy sound when you bust
                    }
                   
                }

            } while ((input == "Y") && (p.ReadHandVal() <= 21));
            Console.WriteLine();
        }

        public void PlayDealer() //Auto-plays dealer
        {
            while (dealer.ReadHandVal() < 17) //Checks that dealer's hand value is less than 17
                dealer.AddHand(mainDeck.DealCard()); //Deals card
            if (dealer.ReadHandVal() > 21) //Checks for bust
                Console.WriteLine("Bust!");
            Console.WriteLine("Dealer's Hand: "); //Prints dealer's final hand
            dealer.PrintHand();
        }

        public void Winnings(Player p) //Adds Player's winnings to account - note that bet has already been subtracted from account
        {
            float win = 0;

            Console.WriteLine("Ok {0}, calculating your winnings...", p.Name);
            Thread.Sleep(500);
            if (p.ReadHandVal() > 21) //If player busts, auto-lose, regardless of blackjack
            {
                Console.WriteLine("Sorry! You lost this hand. Remaining balance: {0:C}", p.Account);

                SystemSounds.Hand.Play(); //Losing sound
                Thread.Sleep(500);
            }
            else if (dealer.Lucky == true) //If dealer has a blackjack, auto-lose
            {
                Console.WriteLine("Sorry! You lost this hand. Remaining balance: {0:C}", p.Account);

                SystemSounds.Hand.Play(); //Losing sound
                Thread.Sleep(500);
            }
            else if (p.Lucky == true) //Otherwise, if player has a blackjack, auto-win bet + bet*1.5
            {
                win = (p.Bet * (float)2.5);
                p.Account += win;
                Console.WriteLine("BlackJack! Congratulations! You win {0:C}. Account balance: {1:C}", (win - p.Bet), p.Account);

                SystemSounds.Exclamation.Play(); //Winning sound
                Thread.Sleep(500);
            }
            else if (p.ReadHandVal() > 21) //If player busts, auto-lose
            {
                Console.WriteLine("Sorry! You lost this hand. Remaining balance: {0:C}", p.Account);
            }
            else if (p.ReadHandVal() == dealer.ReadHandVal()) //Otherwise, if player and dealer are tied, return bet
            {
                p.Account += p.Bet;
                Console.WriteLine("It's a tie! You get your bet back. Account balance: {0:C}", p.Account);

                SystemSounds.Exclamation.Play(); //Winning sound
                Thread.Sleep(500);
            }
            else if (dealer.ReadHandVal() > 21) //If dealer busts, auto-win bet + bet*1
            {
                win = p.Bet * 2;
                p.Account += win;
                Console.WriteLine("The dealer bust! You win {0:C}. Account balance: {1:C}", (win - p.Bet), p.Account);

                SystemSounds.Exclamation.Play(); //Winning sound
                Thread.Sleep(500);
            }
            else if (p.ReadHandVal() > dealer.ReadHandVal()) //Otherwise, compare hands - if player's is greater win
            {
                win = p.Bet * 2;
                p.Account += win;
                Console.WriteLine("Congratulations! You won {0:C}! Account balance: {1:C}", win - p.Bet, p.Account);

                SystemSounds.Exclamation.Play(); //Winning sound
                Thread.Sleep(500);
            }
            else //Otherwise dealer's is greater - lose.
            {
                Console.WriteLine("Sorry! You lost this hand. Remaining balance: {0:C}", p.Account);

                SystemSounds.Hand.Play(); //Losing sound
                Thread.Sleep(500);
            }
            Console.WriteLine();
        }


        public PlayerNode Eliminate(PlayerNode p) //Returns first non-eliminated playernode
        {
            if (p == null) //stops at end of list if all players eliminated
                return null;
            else if(p.Current.Account < 0.01) //To eliminate people with less than a cent left, due to rounding
            {
                Console.WriteLine(p.Current.Name + " is eliminated from the game. Better luck next time!");
                return Eliminate(p.Next); //If eliminated, checks to see if next player is also eliminated. Returns first non-eliminated.
            }
            else
                return p; //if player is not eliminated, returns entered playernode.
        }
    }

}
