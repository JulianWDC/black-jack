/* public void QuickPlayers( int numPlay )
           {
                Player start = new Player(1);
                Player dealer = new Player(0);
                PlayerNode last = new PlayerNode(start, null);
                PlayerNode first;

                for (int k = 2; k <= numPlay; ++k)
                {
                    Player current = new Player(k);
                    PlayerNode node = new PlayerNode(current, last);
                    last = node;
                }

                first = last;
           }

        public void CustomPlayers( int numPlay )
           {
                Console.WriteLine("Please Enter Desired Starting Account Balance: "); //user enter starting account balances
                float accountStart = Convert.ToInt32(Console.ReadLine());

                Console.WriteLine("Player 1 enter name: ");                            //player 1 choose name
                string name1 = Console.ReadLine();

                Player start = new Player(1, name1, accountStart);                     //initalize player 1
                Player dealer = new Player(0, "Dealer", accountStart);                 //initialize dealer
                PlayerNode last = new PlayerNode(start, null);                         //set first node to player 1
                PlayerNode first;                                                      //keeps track of start of list

                for (int k = 2; k <= numPlay; ++k)
                {
                    Console.WriteLine("Player {0} enter name: ", k );                  //next palyer on list enters name
                    string name = Console.ReadLine();                                  
                    Player current = new Player(k, name, accountStart);                //initializes player
                    PlayerNode node = new PlayerNode(current, last);                   //sets node
                    last = node;                                                       
                }

                first = last;                */                                          //changes first pointer