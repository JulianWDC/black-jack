using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Asst1
{
    class PlayerNode
    {
        private Player current;
        private PlayerNode next;

        public Player Current
        {
            get { return current; }
            set { current = value; }
        }

        public PlayerNode Next
        {
            get { return next; }
            set { next = value; }
        }

        public PlayerNode(Player current, PlayerNode next)
        {
            Current = current;
            Next = next;
        }
    }
}
